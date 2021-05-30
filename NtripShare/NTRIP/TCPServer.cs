using NtripShare.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NtripShare
{
    public class TCPServer
    {
        public bool stopFlag { get; set; } = true; //Boolean used to indicate server stopping
        private Thread listenerThread;
        private ArrayList clients = new ArrayList(); //List containing client contexts
        private object stopSyncObj = new object(); //Sync object used with StopFlag
        Socket socket;
        //public int port { get; set; } = 3000;
        //public string pointName { get; set; } = "RTCM32";
       public TCP TCP {
            get;set;
        }
        private TcpListener listener;
        System.Net.IPAddress ip = System.Net.IPAddress.Any;
        public double total { get; set; } = 0;

        public TCPServer(TCP tcp) {
            TCP = tcp;
        }


        public void StartServer() //This gets called by a button on another form.
        {
            if (!stopFlag)
            {
                StopServer();
            }

            try
            {
                listener = new TcpListener(ip, TCP.Port);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            stopFlag = false;

            listenerThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoListen));
            listenerThread.Start();

            Thread processThread = new Thread(new System.Threading.ThreadStart(ProcessQueues));
            processThread.Start();
        }
        public void StopServer() //This gets called by a button on another form, and on form close.
        {
            stopFlag = true;
            if (listener != null)
            {
                try
                {
                    listener.Stop(); //Causes DoListen() to abort
                }
                catch
                {
                }
            }

            try
            {
                if (socket!= null && socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(true);
                }

                if (!(listenerThread == null))
                {
                    //listenerThread.Abort();
                }
            }
            catch (Exception)
            {
                MainForm.Default.LogToUIThread(0, "TCP Server停止监听" + TCP.Port + "端口");
            }
        }
        private void DoListen()
        {
            ClientContext context = default(ClientContext);
            Socket socket = default(Socket);

            listener.Start(); //Start listening

            MainForm.Default.LogToUIThread( 0, "TCP Server正在监听" + TCP.Port + "端口");
            //LogEvent("TCP Server正在监听" + listener.LocalEndpoint.ToString()+"端口");

            try //The try block is merely to exit gracefully when you stop listening
            {
                do //Loop to handle multiple connections
                {
                    if (stopFlag) {
                        break;
                    }
                    socket = listener.AcceptSocket(); //AcceptSocket blocks until a connection is established
                    if (!(socket == null)) //can't hurt to double-check
                    {
                        int ConnectionCount = -1;
                        lock (clients) //lock the list and add the ClientContext
                        {
                            context = new ClientContext(socket); //create a new context
                            clients.Add(context); //Add it to the list
                            ConnectionCount = clients.Count;
                        }
                        if (ConnectionCount > -1)
                        {
                            MainForm.Default.LogToUIThread( 0, "TCP客户端已连接" + TCP.Port + "端口");
                        }
                    }
                }
                while (true);
            }
            catch(Exception e)
            {
                int ss = 1;
            }
        }
        private void ProcessQueues()
        {
            byte[] buffer = new byte[1024];
            int len = 0;
            do
            {
                //-- Get a lock on the entire collection
                lock (clients)
                {
                    foreach (ClientContext client in clients)
                    {
                        if (client.IsConnected() == false) //Is the socket connected
                        {
                            client.RemoveFlag = true;
                        }
                        else
                        {
                            //-- Process outgoing messages.
                            if (client.SendQueue.Count > 0) //Is there an item to send
                            {
                                lock (client.SendQueue.Peek()) //Get a lock on the outgoing byte array
                                {
                                    byte[] bytes = (byte[])(client.SendQueue.Dequeue()); //Retrieve the byte array
                                    try //Try sending
                                    {
                                        client.Socket.Send(bytes, bytes.Length, SocketFlags.None);
                                    }
                                    catch (System.Net.Sockets.SocketException)
                                    {
                                        client.RemoveFlag = true; //The socket has disconnected. Mark it for death.
                                    }
                                }
                            }
                            else
                            {
                                if (client.RemoveAfterSend) //Send Queue is now empty, kill connection
                                {
                                    client.RemoveFlag = true;
                                }
                            }
                            lock (stopSyncObj) //Are we stopped
                            {
                                if (stopFlag == true)
                                {
                                    goto endOfDoLoop;
                                }
                            }

                            //Process incoming messages
                            if (client.RemoveFlag == false)
                            {
                                if (client.Socket != null) //Do we have a socket
                                {
                                    if (client.FirstTime)
                                    {
                                        //SendStuffToUIThread(1, client.ConnID, client.Socket.RemoteEndPoint.ToString()); //Add ID to the users table on the UI thread
                                        client.FirstTime = false;
                                    }

                                    if (client.Socket.Poll(10, SelectMode.SelectRead) == true) //Did we receive data
                                    {
                                        try //Try reading from the socket
                                        {
                                            //InputStream input = new ByteArrayInputStream(buffer);
                                            //org.gogpsproject.producer.parser.rtcm3.RTCM3Client rTCM3Client = new org.gogpsproject.producer.parser.rtcm3.RTCM3Client(null);
                                            
                                            //len = client.Socket.Receive(buffer, buffer.Length, SocketFlags.None); //len returns the number of bytes received
                                            //total += (len/1024.0/1024.0);
                                            //if (len > 0) //Data was received.
                                            //{
                                            //    byte[] bytes = new byte[len - 1 + 1];
                                            //    Array.Copy(buffer, bytes, len);
                                            //    MainForm.Default.UpdateDataUIThread(1, TCP, bytes);
                                            //}
                                            if (client.Socket.Available <= 0) continue;
                                            //string recvStr = "";
                                            byte[] recvBytes = new byte[1024 * 100];
                                            //int len2;
                                            do
                                            {
                                                total += (len / 1024.0 / 1024.0);
                                                client.Socket.Blocking = true;
                                                len = client.Socket.Receive(recvBytes, recvBytes.Length, 0); //从客户端接受消息
                                                //recvStr += Encoding.ASCII.GetString(recvBytes, 0, bytes);
                                                byte[] bytes = new byte[len];
                                                Array.Copy(recvBytes, bytes, len);
                                                MainForm.Default.UpdateDataUIThread(1, TCP, bytes);
                                            } while (len == 1024 * 100);

                                        }
                                        catch (System.Net.Sockets.SocketException)
                                        {
                                            client.RemoveFlag = true; //The socket has disconnected. Mark it for death
                                        }
                                    }
                                }
                            }

                            lock (stopSyncObj)
                            {
                                if (stopFlag == true) //Are we stopped
                                {
                                    goto endOfDoLoop;
                                }
                            }
                        }

                        Thread.Sleep(10); //To prevent CPU overload
                    }
                }

                lock (clients) //Remove any dead sockets
                {
                    bool removed = false;
                    int curCount = clients.Count;
                    do
                    {
                        removed = false;
                        for (int i = 0; i <= clients.Count - 1; i++)
                        {
                            if (((ClientContext)(clients[i])).RemoveFlag == true)
                            {
                                //MainForm.Default.SendStuffToUIThread(2, ((ClientContext)(clients[i])).ConnID, null); //Remove ID from the connections table on the UI thread
                                ((ClientContext)(clients[i])).Socket.Shutdown(SocketShutdown.Both);
                                ((ClientContext)(clients[i])).Socket.Close();
                                clients.Remove(clients[i]);
                                removed = true;
                                break;
                            }
                        }
                    } while (!(removed == false));
                    if (clients.Count != curCount) //Client count changed
                    {
                        if (clients.Count == 0)
                        {
                            MainForm.Default.LogToUIThread( 0, "TCP客户端已断开连接"+TCP.Port + "端口");
                        }
                    }
                }

                lock (stopSyncObj)
                {
                    if (stopFlag == true) //Are we stopped
                    {
                        break;
                    }
                }

                Thread.Sleep(10);
            } while (true);
            endOfDoLoop:
            1.GetHashCode(); //VBConversions note: C# requires an executable line here, so a dummy line was added.
        }
    }
}
