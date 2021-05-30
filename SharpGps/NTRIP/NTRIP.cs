// Copyright 2007 - Morten Nielsen
//
// This file is part of SharpGps.
// SharpGps is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpGps is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpGps; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using SharpGPS;
using System.Threading;

namespace SharpGPS.NTRIP
{
    public class NTRIPClient : IDisposable
    {
        private Socket sckt;
        private string _username;
        private string _password;
        private IPEndPoint _broadcaster;
        byte[] rtcmDataBuffer = new byte[1024];
        public AsyncCallback pfnCallBack;

        private string recentGGA = "";
        public bool NTRIPIsConnected = false;

        public string mMountPoint;

        public delegate void OnDataReceivedEvent(byte[] data);
        public OnDataReceivedEvent dataReceivedEvent;

        public delegate void OnMessageEvent(string msg);
        public OnMessageEvent messageEvent;

        public delegate void OnErrorEvent(string msg);
        public OnErrorEvent errorEvent;

        System.Timers.Timer timer = new System.Timers.Timer();


        /// <summary>
        /// 初始化时把需要回调的函数传入
        /// </summary>
        /// <param name="func"></param>
        public void InitDataReceivedCallbackFunction(OnDataReceivedEvent func)
        {
            dataReceivedEvent = func;
        }

        /// <summary>
        /// 初始化时把需要回调的函数传入
        /// </summary>
        /// <param name="func"></param>
        public void InitMessageCallbackFunction(OnMessageEvent func)
        {
            messageEvent = func;
        }

        public string MostRecentGGA
        {
            get { return recentGGA; }
            set { recentGGA = value; }
        }

        /// <summary>
        /// NTRIP server Username
        /// </summary>
        public string UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// NTRIP server password
        /// </summary>
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// NTRIP Server
        /// </summary>
        public IPEndPoint BroadCaster
        {
            get { return _broadcaster; }
            set { _broadcaster = value; }
        }

        private GPSHandler gps;

        public NTRIPClient(IPEndPoint Server, GPSHandler gpsHandler)
        {
            //Initialization...
            gps = gpsHandler;
            BroadCaster = Server;
            //InitializeSocket();sourceTable.DataStreams[i].MountPoint
        }

        public NTRIPClient(IPEndPoint Server, string strUserName, string strPassword, GPSHandler gpsHandler) : this(Server, gpsHandler)
        {
            _username = strUserName;
            _password = strPassword;
            timer.Interval = 1000;
            timer.Elapsed += delegate
            {
                showMessage("发送GGA启动");
                this.sendGGA();
            };
            //InitializeSocket();
        }

        private void InitializeSocket()
        {
            sckt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }



        /// <summary>
        /// Creates request to NTRIP server
        /// </summary>
        /// <param name="strRequest">Resource to request. Leave blank to get NTRIP service data</param>
        private byte[] CreateRequest(string strRequest)
        {
            //Build request message
            string msg = "GET /" + strRequest + " HTTP/1.1\r\n";
            msg += "User-Agent: SharpGps iter.dk\r\n";
            //If password/username is specified, send login details
            if (_username != null && _username != "")
            {
                string auth = ToBase64(_username + ":" + _password);
                msg += "Authorization: Basic " + auth + "\r\n";
            }
            msg += "Accept: */*\r\nConnection: close\r\n";
            msg += "\r\n";

            return System.Text.Encoding.ASCII.GetBytes(msg);
        }

        private void Connect()
        {
            //Connect to server	   
            try {
                if (!sckt.Connected)
                    sckt.Connect(BroadCaster);
            }
            catch
            {
               

            }
            
        }

        private void Close()
        {
            sckt.Shutdown(SocketShutdown.Both);
            sckt.Close();
        }

        public SourceTable GetSourceTable()
        {
            this.InitializeSocket();
            sckt.Blocking = true;
            this.Connect();
            sckt.Send(CreateRequest(""));
            string responseData = "";
            System.Threading.Thread.Sleep(1000); //Wait for response
            while (sckt.Available > 0)
            {
                byte[] returndata = new byte[sckt.Available];
                sckt.Receive(returndata); //Get response
                responseData += System.Text.Encoding.ASCII.GetString(returndata, 0, returndata.Length);
                System.Threading.Thread.Sleep(100); //Wait for response
            }
            this.Close();

            if (!responseData.StartsWith("SOURCETABLE 200 OK"))
                return null;

            SourceTable result = new SourceTable();
            foreach (string line in responseData.Split('\n'))
            {
                if (line.StartsWith("STR"))
                    result.DataStreams.Add(NTRIP.SourceTable.NTRIPDataStream.ParseFromString(line));
                else if (line.StartsWith("CAS"))
                    result.Casters.Add(NTRIP.SourceTable.NTRIPCaster.ParseFromString(line));
                else if (line.StartsWith("NET"))
                    result.Networks.Add(NTRIP.SourceTable.NTRIPNetwork.ParseFromString(line));
            }
            return result;
        }

        /// <summary>
        /// Opens the connection to the NTRIP server and starts receiving
        /// </summary>
        /// <param name="MountPoint">ID of Stream</param>
        public void StartNTRIP(string MountPoint,OnDataReceivedEvent onDataReceivedEvent,OnMessageEvent onMessageEvent,OnErrorEvent onErrorEvent)
        {
            this.dataReceivedEvent = onDataReceivedEvent;
            this.messageEvent = onMessageEvent;
            this.errorEvent = onErrorEvent;
            this.InitializeSocket();
            sckt.Blocking = true;
            this.Connect();
            sckt.Send(CreateRequest(MountPoint));
            string responseData = "";
            System.Threading.Thread.Sleep(1000); //Wait for response
            while (sckt.Available > 0)
            {
                byte[] returndata = new byte[sckt.Available];
                sckt.Receive(returndata); //Get response
                responseData += System.Text.Encoding.ASCII.GetString(returndata, 0, returndata.Length);
                System.Threading.Thread.Sleep(100); //Wait for response
            }

            showMessage(responseData);
            if (responseData.Contains("401 Unauthorized"))
            {
                showError("用户名或密码错误，授权失败");  
                this.Close();
            }
            else if (responseData.Contains("ICY 200 OK"))
            {
                showMessage("连接基准站"+ BroadCaster.Address.ToString()+":"+ BroadCaster.Port+"成功！");
                NTRIPIsConnected = true;
                sckt.Blocking = false;
                WaitForData(sckt);
                timer.Start();
            }
        }

        private void showMessage(string msg) {
            if (messageEvent != null)
            {
                IAsyncResult result = messageEvent.BeginInvoke(msg, delegate (IAsyncResult ar)
                {
                    messageEvent.EndInvoke(ar);
                }, "");
            }
        }

        private void showError(string msg)
        {
            if (errorEvent != null)
            {
                IAsyncResult result = errorEvent.BeginInvoke(msg, delegate (IAsyncResult ar)
                {
                    errorEvent.EndInvoke(ar);
                }, "");
            }
        }

        public void WaitForData(Socket sock)
        {

            //if (pfnCallBack == null)
            //	pfnCallBack = new AsyncCallback(OnDataReceived);
            AsyncCallback recieveData = new AsyncCallback(OnDataReceived);
            sock.BeginReceive(rtcmDataBuffer, 0, rtcmDataBuffer.Length, SocketFlags.None,
                recieveData, sock);
            //m_asynResult = sckt.BeginReceive(rtcmDataBuffer, 0, rtcmDataBuffer.Length, SocketFlags.None, pfnCallBack, null);
        }

        private void OnDataReceived(IAsyncResult ar)
        {
            if (!NTRIPIsConnected) {
                return;
            }
            //byte[] nmeadata = System.Text.Encoding.ASCII.GetBytes(MostRecentGGA + "\r\n");
            //try
            //{
            //    sckt.Send(nmeadata);
            //    showMessage("发送模拟位置：" + MostRecentGGA);
            //}
            //catch (Exception ex)
            //{
            //    showMessage("发送模拟GGA报错" );
            //}
            Socket sock = (Socket)ar.AsyncState;
            if (!sock.Connected) {
                return;
            }
            int iRx = sock.EndReceive(ar);
            if (iRx > 0) //if we received at least one byte
            {
                try
                {
                    if (dataReceivedEvent != null)
                    {
                        IAsyncResult result = dataReceivedEvent.BeginInvoke(rtcmDataBuffer, delegate (IAsyncResult arr)
                        {
                            dataReceivedEvent.EndInvoke(arr);
                        }, "");
                    }
                }
                catch (System.Exception ex)
                {
                    this.Close();
                    throw (new System.Exception("Error sending RTCM data to device:" + ex.Message));
                }
            }
            WaitForData(sock);
        }

        /// <summary>
        /// 发送GGA模拟数据
        /// </summary>
        public void sendGGA() {
            if (!NTRIPIsConnected)
            {
                return;
            }
            byte[] nmeadata = System.Text.Encoding.ASCII.GetBytes(MostRecentGGA + "\r\n");
            try
            {
                sckt.Send(nmeadata);
                showMessage("发送模拟位置：" + MostRecentGGA);
            }
            catch (Exception ex)
            {
                showMessage("发送模拟GGA报错");
            }
        }

        /// <summary>
        /// Stops receiving data from the NTRIP server
        /// </summary>
        public void StopNTRIP()
        {
            NTRIPIsConnected = false;
            timer.Stop();
            this.Close();
        }

        /// <summary>
        /// Apply AsciiEncoding to user name and password to obtain it as an array of bytes
        /// </summary>
        /// <param name="str">username:password</param>
        /// <returns>Base64 encoded username/password</returns>
        private string ToBase64(string str)
        {
            Encoding asciiEncoding = Encoding.ASCII;
            byte[] byteArray = new byte[asciiEncoding.GetByteCount(str)];
            byteArray = asciiEncoding.GetBytes(str);
            return Convert.ToBase64String(byteArray, 0, byteArray.Length);
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Close();
        }

        #endregion
    }
}
