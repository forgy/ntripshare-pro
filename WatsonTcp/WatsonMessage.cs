﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WatsonTcp
{
    internal class WatsonMessage
    {
        #region Public-Members

        /// <summary>
        /// Length of the data.
        /// </summary>
        public long ContentLength { get; set; }
         
        /// <summary>
        /// Preshared key for connection authentication.
        /// </summary>
        public byte[] PresharedKey
        {
            get
            {
                return _PresharedKey;
            }
            set
            {
                if (value == null)
                {
                    _PresharedKey = null; 
                }
                else
                {
                    if (value.Length != 16) throw new ArgumentException("PresharedKey must be 16 bytes.");

                    _PresharedKey = new byte[16];
                    Buffer.BlockCopy(value, 0, _PresharedKey, 0, 16); 
                }
            }
        }

        /// <summary>
        /// Status of the message.   
        /// </summary>
        public MessageStatus Status = MessageStatus.Normal;
          
        /// <summary>
        /// Metadata dictionary; contains user-supplied metadata.
        /// </summary>
        public Dictionary<object, object> Metadata
        {
            get
            {
                return _Metadata;
            }
            set
            {
                if (value == null || value.Count < 1)
                {
                    _Metadata = new Dictionary<object, object>(); 
                }
                else
                {
                    _Metadata = value; 
                }
            }
        }

        /// <summary>
        /// Indicates if the message is a synchronous request.
        /// </summary>
        public bool SyncRequest = false;

        /// <summary>
        /// Indicates if the message is a synchronous response.
        /// </summary>
        public bool SyncResponse = false;

        /// <summary>
        /// Indicates the current time as perceived by the sender; useful for determining expiration windows.
        /// </summary>
        public DateTime? SenderTimestamp = null;

        /// <summary>
        /// Indicates an expiration time in UTC; only applicable to synchronous requests.
        /// </summary>
        public DateTime? Expiration = null;

        /// <summary>
        /// Indicates the conversation GUID of the message. 
        /// </summary>
        public string ConversationGuid = null;

        /// <summary>
        /// The type of compression used in the message.
        /// </summary>
        public CompressionType Compression = CompressionType.None;
         
        /// <summary>
        /// Stream containing the message data.
        /// </summary>
        [JsonIgnore]
        public Stream DataStream
        {
            get
            {
                return _DataStream;
            }
        }

        /// <summary>
        /// Message headers in byte-array form ready to send.
        /// </summary>
        [JsonIgnore]
        public byte[] HeaderBytes
        {
            get
            {
                string jsonStr = SerializationHelper.SerializeJson(this, false);
                byte[] jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
                byte[] end = Encoding.UTF8.GetBytes("\r\n\r\n");
                byte[] final = WatsonCommon.AppendBytes(jsonBytes, end);
                return final;
            }
        }

        #endregion

        #region Internal-Members

        /// <summary>
        /// Size of buffer to use while reading message payload.  Default is 64KB.
        /// </summary>
        internal int ReadStreamBuffer
        {
            get
            {
                return _ReadStreamBuffer;
            }
            set
            {
                if (value < 1) throw new ArgumentException("ReadStreamBuffer must be greater than zero bytes.");
                _ReadStreamBuffer = value;
            }
        }
         
        #endregion

        #region Private-Members

        private Action<string> _Logger = null;
        private string _Header = "[WatsonMessage] ";
        //                                         1         2         3
        //                                12345678901234567890123456789012
        private string _DateTimeFormat = "yyyy-MM-dd HH:mm:ss.fffzzz"; // 32 bytes

        private int _ReadStreamBuffer = 65536; 
        private byte[] _PresharedKey; 
        private Dictionary<object, object> _Metadata = new Dictionary<object, object>(); 
        private Stream _DataStream = null;

        #endregion

        #region Constructors-and-Factories

        /// <summary>
        /// Do not use.
        /// </summary>
        internal WatsonMessage()
        { 
            Status = MessageStatus.Normal;
        }
         
        /// <summary>
        /// Construct a new message to send.
        /// </summary>
        /// <param name="metadata">Metadata to attach to the message.</param>
        /// <param name="contentLength">The number of bytes included in the stream.</param>
        /// <param name="stream">The stream containing the data.</param>
        /// <param name="syncRequest">Indicate if the message is a synchronous message request.</param>
        /// <param name="syncResponse">Indicate if the message is a synchronous message response.</param>
        /// <param name="expiration">The time at which the message should expire (only valid for synchronous message requests).</param>
        /// <param name="compression">The type of compression to use.</param>
        /// <param name="convGuid">Conversation GUID.</param>
        /// <param name="logger">Logger method.</param>
        internal WatsonMessage(
            Dictionary<object, object> metadata, 
            long contentLength, 
            Stream stream, 
            bool syncRequest, 
            bool syncResponse, 
            DateTime? expiration, 
            string convGuid, 
            CompressionType compression, 
            Action<string> logger)
        {
            if (contentLength < 0) throw new ArgumentException("Content length must be zero or greater.");
            if (contentLength > 0)
            {
                if (stream == null || !stream.CanRead)
                {
                    throw new ArgumentException("Cannot read from supplied stream.");
                }
            } 

            Status = MessageStatus.Normal; 
            ContentLength = contentLength;
            Metadata = metadata;
            SyncRequest = syncRequest;
            SyncResponse = syncResponse;
            Expiration = expiration;
            ConversationGuid = convGuid;
            Compression = compression;
            if (SyncRequest) SenderTimestamp = DateTime.Now;

            _DataStream = stream;
            _Logger = logger; 
        }

        /// <summary>
        /// Read from a stream and construct a message.  Call Build() to populate.
        /// </summary>
        /// <param name="stream">Stream.</param>
        /// <param name="logger">Logger method.</param>
        internal WatsonMessage(Stream stream, Action<string> logger)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (!stream.CanRead) throw new ArgumentException("Cannot read from stream.");
             
            Status = MessageStatus.Normal; 
            
            _DataStream = stream;
            _Logger = logger; 
        }
         
        #endregion
        
        #region Internal-Methods
         
        /// <summary>
        /// Build the Message object from data that awaits in a NetworkStream or SslStream.
        /// </summary>
        /// <returns>True if successful.</returns>
        internal async Task<bool> BuildFromStream()
        {
            try
            {
                #region Read-Headers

                    //byte[] buffer = new byte[0];
                    //byte[] data = await WatsonCommon.ReadFromStreamAsync(_DataStream, 0, _ReadStreamBuffer);
                    //if (data != null && data.Length == 1)
                    //{
                    //ContentLength = _DataStream.Length;
                    //}
                    //ContentLength = msg.ContentLength;
                    //while (true)
                    //{
                    //    byte[] data = await WatsonCommon.ReadFromStreamAsync(_DataStream, 1, _ReadStreamBuffer);
                    //    if (data != null && data.Length == 1)
                    //    {
                    //        buffer = WatsonCommon.AppendBytes(buffer, data);
                    //        if (buffer.Length >= 4)
                    //        {
                    //            byte[] endCheck = buffer.Skip(buffer.Length - 4).Take(4).ToArray();
                    //            if ((int)endCheck[3] == 10
                    //                && (int)endCheck[2] == 13
                    //                && (int)endCheck[1] == 10
                    //                && (int)endCheck[0] == 13)
                    //            {
                    //                _Logger?.Invoke(_Header + "ReadHeaders found header demarcation");
                    //                break;
                    //            }
                    //        }
                    //    }
                    //}

                    //WatsonMessage msg = SerializationHelper.DeserializeJson<WatsonMessage>(Encoding.UTF8.GetString(buffer));
                    //WatsonMessage msg = new WatsonMessage();
                    //ContentLength = msg.ContentLength;
                    //PresharedKey = msg.PresharedKey;
                    //Status = msg.Status;
                    //Metadata = msg.Metadata;
                    //SyncRequest = msg.SyncRequest;
                    //SyncResponse = msg.SyncResponse;
                    //SenderTimestamp = msg.SenderTimestamp;
                    //Expiration = msg.Expiration;
                    //ConversationGuid = msg.ConversationGuid;
                    //Compression = msg.Compression;

                    //_Logger?.Invoke(_Header + "BuildFromStream header processing complete" + Environment.NewLine + Encoding.UTF8.GetString(buffer).Trim()); 

                    #endregion

                    return true;
            }
            catch (IOException)
            {
                _Logger?.Invoke(_Header + "BuildStream IOexception, disconnect assumed");
                return false;
            }
            catch (SocketException)
            {
                _Logger?.Invoke(_Header + "BuildStream SocketException, disconnect assumed");
                return false;
            }
            catch (ObjectDisposedException)
            {
                _Logger?.Invoke(_Header + "BuildStream ObjectDisposedException, disconnect assumed");
                return false;
            }
            catch (Exception e)
            {
                _Logger?.Invoke(_Header + "BuildStream exception: " +
                    Environment.NewLine +
                    SerializationHelper.SerializeJson(e, true) +
                    Environment.NewLine);
                return false;
            }
        }
         
        /// <summary>
        /// Human-readable string version of the object.
        /// </summary>
        /// <returns>String.</returns>
        public override string ToString()
        {
            string ret = "---" + Environment.NewLine; 
            ret += "  Preshared key     : " + (PresharedKey != null ? WatsonCommon.ByteArrayToHex(PresharedKey) : "null") + Environment.NewLine;
            ret += "  Status            : " + Status.ToString() + Environment.NewLine;
            ret += "  SyncRequest       : " + SyncRequest.ToString() + Environment.NewLine;
            ret += "  SyncResponse      : " + SyncResponse.ToString() + Environment.NewLine;
            ret += "  ExpirationUtc     : " + (Expiration != null ? Expiration.Value.ToString(_DateTimeFormat) : "null") + Environment.NewLine;
            ret += "  Conversation GUID : " + ConversationGuid + Environment.NewLine;
            ret += "  Compression       : " + Compression.ToString() + Environment.NewLine;

            if (Metadata != null)
            {
                ret += "  Metadata          : " + Metadata.Count + " entries" + Environment.NewLine;
            }
             
            if (DataStream != null)
                ret += "  DataStream        : present, " + ContentLength + " bytes" + Environment.NewLine;

            return ret;
        }

        #endregion Public-Methods

        #region Private-Methods

        #endregion
    }
}