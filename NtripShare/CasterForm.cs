// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
// End of VB project level imports

using System.Net.Sockets;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

using NtripShare.Model;
using NtripShare.Util;
using GMap.NET.WindowsForms;
using GMapUtil;

namespace NtripShare
{
    public partial class CasterForm 
    {
        public CasterForm()
        {
            settingsfile = Application.StartupPath + "\\Settings.txt";
            InitializeComponent();

            if (defaultInstance == null)
                defaultInstance = this;
        }

        GMapPolygon GeoFence = null;

        #region Default Instance

        private static CasterForm defaultInstance;

        /// <summary>
        /// Added by the VB.Net to C# Converter to support default instance behavour in C#
        /// </summary>
        public static CasterForm Default
        {
            get
            {
                if (defaultInstance == null)
                {
                    defaultInstance = new CasterForm();
                    defaultInstance.FormClosed += new FormClosedEventHandler(defaultInstance_FormClosed);
                }

                return defaultInstance;
            }
            set
            {
                defaultInstance = value;
            }
        }

        static void defaultInstance_FormClosed(object sender, FormClosedEventArgs e)
        {
            defaultInstance = null;
        }

        #endregion
        string settingsfile; // VBConversions Note: Initial value cannot be assigned here since it is non-static.  Assignment has been moved to the class constructors.

        private TcpListener listener;
        private System.Threading.Thread listenerThread;
        System.Net.IPAddress ip = System.Net.IPAddress.Any;
        public int port { get; set; } = 5000;
        Socket socket;
        public bool stopFlag = true; //Boolean used to indicate server stopping
        private object stopSyncObj = new object(); //Sync object used with StopFlag
        private ArrayList clients = new ArrayList(); //List containing client contexts

        //public DataTable dtconnections = new DataTable();
        public List<UserAccount> connecedUserList = new List<UserAccount>();
        public List<MountPoint> mountpointList = new List<MountPoint>();
        //public DataTable dtmountpoints = new DataTable();
        //public DataTable dtusers = new DataTable();

        private int TimerTickCount = 0;
        public string MyNetworkName = "GG RTK";
        public bool WriteEventsToFile = false;

        public static int ConnIDCount = 0; //Incrementing count of unique connection IDs

        //private string MountPoistsList = "";
        string sourceHeader = "SOURCETABLE 200 OK\r\n" +
"Server: NtripShareBasic/180615 \r\n" +
"Date: 2018/07/17 15:09:53\r\n" +
"Content-Type: text/plain\r\n" +
"Content-Length:  27840\r\n";

        string sourceFoot = "ENDSOURCETABLE";


        public void MainForm_Load(System.Object sender, System.EventArgs e)
        {
            lblVersion.Text += " " + System.Convert.ToString((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.Version.Major) + "." + Strings.Format((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.Version.Minor, "00") + "." + Strings.Format((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.Version.Build, "00");
            if ((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.Version.Revision != 0)
            {
                lblVersion.Text += " (Rev " + System.Convert.ToString((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).Info.Version.Revision) + ")";
            }

            tbServerInfo.Text += "If there is a GPS base station connected to a serial port on this computer, you can use the built in NTRIP server to get that data stream out to other NTRIP Clients. Of course you can have other NTRIP Servers sending data in via the network. The local NTRIP Seriver is an option for those of you that want the Caster and Server on the same computer.";

            if (WriteEventsToFile)
            {
                boxDoLogging.SelectedIndex = 1;
            }
            else
            {
                boxDoLogging.SelectedIndex = 0;
            }


            clients = ArrayList.Synchronized(clients); //use a synchronized arraylist to store the clients
            Timer1.Start();
        }
        public void MainForm_FormClosing(System.Object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            ProjectData.EndApp();
        }

        /// <summary>
        /// 添加接入点
        /// </summary>
        /// <param name="data"></param>
        public void addMountPoint(string data)
        {
            if (data.Contains("STR"))
            {
                mountpointList.Add(new MountPoint(data));
            }
        }

        /// <summary>
        /// 获取接入点列表字符串
        /// </summary>
        /// <returns></returns>
        public string getMountPoistsList() {
            string str = "";
            foreach (MountPoint mountPoint in mountpointList) {
                str += mountPoint.getString() + "\r\n";
            }
            return str;
        }


        public void LogEvent(string Message)
        {
            MainForm.Default.LogToUIThread(0,Message);
          
        }
        public void boxDoLogging_SelectionChangeCommitted(System.Object sender, System.EventArgs e)
        {
        }

        public void StartServer() //This gets called by a button on another form.
        {
            if (listener != null)
            {
                StopServer();
            }

            try
            {
                listener = new TcpListener(ip, port);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            stopFlag = false;
            //SendStuffToUIThread(0, 0, "0"); //Report no connections

            listenerThread = new System.Threading.Thread(new System.Threading.ThreadStart(DoListen));
            listenerThread.Start();

            Thread processThread = new Thread(new System.Threading.ThreadStart(ProcessQueues));
            processThread.Start();
            LogEvent("Server启动成功，端口" + port + ",接入点" + DocumentSetting.Default.PoolName);
        }
        public void StopServer() //This gets called by a button on another form, and on form close.
        {
            //To reuse a socket, call Disconnect(true) instead of Close(). Close will release all the socket resources.
            //It's recommended to call Shutdown() before Disconnect to allow all data to be sent and received.
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
                if (socket != null && socket.Connected)
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Disconnect(true);
                }

                if (!(listenerThread == null))
                {
                    listenerThread.Abort();
                }
            }
            catch (Exception)
            {
            }
            connecedUserList.Clear();
            foreach (ClientContext client in clients)
            {
                if (client.IsConnected()) {
                    client.Socket.Shutdown(SocketShutdown.Both);
                    client.Socket.Disconnect(true);
                }
            }
            clients.Clear();

            SendStuffToUIThread(0, 0, "0"); //Report no connections
        }
        private void DoListen()
        {
            ClientContext context = default(ClientContext);
            Socket socket = default(Socket);

            listener.Start(); //Start listening

            SendStuffToUIThread(-1, 0, "Caster正在监听端口" + port); //Request Mountpoint

            try //The try block is merely to exit gracefully when you stop listening
            {
                do //Loop to handle multiple connections
                {
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
                            SendStuffToUIThread(0, 0, clients.Count.ToString());
                        }
                    }
                } while (true);
            }
            catch(Exception e)
            {
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


                            for (int ctr = 0; ctr < connecedUserList.Count; ctr++)
                            {
                                if (connecedUserList[ctr].CurrentLat == 0 || connecedUserList[ctr].CurrentLon == 0)
                                {
                                    if (connecedUserList[ctr].ConnectTime.AddMinutes(5) < DateTime.Now) {
                                        client.RemoveFlag = true;
                                        SendStuffToUIThread(-1, client.ConnID, "Connection ID " + client.ConnID + " - 用户 " + connecedUserList[ctr].Username + " 长时间未上传GGA数据，已强制下线！");
                                    }
                                }
                                else {
                                    ///地理围栏验证
                                    if (DocumentSetting.Default.StartGeoFence)
                                    {
                                        if (connecedUserList[ctr].ConnectionID == client.ConnID)
                                        {
                                            if (CoordUtil.isInChina(connecedUserList[ctr].CurrentLat, connecedUserList[ctr].CurrentLon))
                                            {
                                                if (GeoFence == null)
                                                {
                                                    GeoFence = GMapChinaRegion.ChinaMapRegion.GetRegionPolygon("geofence", DocumentSetting.Default.GeoFence);
                                                }
                                                if (GeoFence != null)
                                                {

                                                    if (!GMapUtils.IsPointInPolygon(GeoFence.Points, new GMap.NET.PointLatLng(connecedUserList[ctr].CurrentLat, connecedUserList[ctr].CurrentLon)))
                                                    {
                                                        client.RemoveFlag = true;
                                                        SendStuffToUIThread(-1, client.ConnID, "Connection ID " + client.ConnID + " - 用户 " + connecedUserList[ctr].Username + " 位置超出地理围栏范围，已强制下线！");
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
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

                            if (client.RemoveFlag == false)
                            {
                                if (client.Socket != null) //Do we have a socket
                                {
                                    if (client.FirstTime)
                                    {
                                        SendStuffToUIThread(1, client.ConnID, client.Socket.RemoteEndPoint.ToString()); //Add ID to the users table on the UI thread
                                        client.FirstTime = false;
                                    }

                                    if (client.Socket.Poll(10, SelectMode.SelectRead) == true) //Did we receive data
                                    {
                                        try //Try reading from the socket
                                        {
                                            len = client.Socket.Receive(buffer, buffer.Length, SocketFlags.None); //len returns the number of bytes received
                                            if (len > 0) //Data was received.
                                            {
                                                byte[] bytes = new byte[len - 1 + 1];
                                                Array.Copy(buffer, bytes, len); //Copy to another buffer

                                                if (client.ConnLevel == 2) //This is a server sending in data
                                                {
                                                    PropagateStreamData(null, client.MountPoint, bytes) ;
                                                }
                                                else if (client.ConnLevel == 1) //This is a client, and we don't care about the data.
                                                {
                                                    string data = Encoding.ASCII.GetString(bytes).Replace("\n", "");
                                                    ProcessMessages(client, data.Trim());
                                                }
                                                else //Not authenticated yet, process data
                                                {
                                                    //Decode data and append to incoming buffer
                                                    client.IncomingData += Encoding.ASCII.GetString(bytes).Replace("\n", "");
                                                    if (client.IncomingData.IndexOf(('\r').ToString()) >= 0) //Contains at least one carridge return
                                                    {
                                                        string[] lines = Strings.Split(client.IncomingData, System.Convert.ToString('\r'));
                                                        for (var i = 0; i <= (lines.Length - 1) - 1; i++)
                                                        {
                                                            ProcessMessages(client, lines[(int)i].Trim());
                                                        }
                                                        client.IncomingData = lines[lines.Length - 1];
                                                    }
                                                    else //Data doesn't contain any line breaks
                                                    {
                                                        if (client.IncomingData.Length > 4000)
                                                        {
                                                            client.IncomingData = ""; //Clean out stale data
                                                        }
                                                    }
                                                }

                                            }
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
                                try
                                {
                                    SendStuffToUIThread(2, ((ClientContext)(clients[i])).ConnID, null); //Remove ID from the connections table on the UI thread

                                    ((ClientContext)(clients[i])).Socket.Shutdown(SocketShutdown.Both);
                                    ((ClientContext)(clients[i])).Socket.Close();
                                    clients.Remove(clients[i]);
                                    removed = true;
                                }
                                catch (System.Net.Sockets.SocketException e)
                                {
                                    SendStuffToUIThread(-1, ((ClientContext)(clients[i])).ConnID, "Socket错误！");
                                }
                                break;
                            }
                        }
                    } while (!(removed == false));
                    if (clients.Count != curCount) //Client count changed
                    {
                        SendStuffToUIThread(0, 0, clients.Count.ToString());
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
        private void PropagateStreamData(string stationName, string MountPoint, byte[] bytes)
        {
       
            if (string.IsNullOrEmpty(stationName))
            {
                SendStuffToUIThread(99, bytes.Length, MountPoint);
                foreach (ClientContext client in clients)
                {
                    if (client.MountPoint == MountPoint && client.ConnLevel == 1)
                    {
                        client.SendQueue.Enqueue(bytes);
                    }
                }
            }
            else {
                for (int i = 0; i < connecedUserList.Count; i++) {
                    if (connecedUserList[i].ConnectStaion == stationName) {
                        connecedUserList[i].DataSize = (connecedUserList[i].DataSize + bytes.Length);
                        foreach (ClientContext client in clients)
                        {
                            if (client.MountPoint == MountPoint && client.ConnLevel == 1 && client.ConnID == connecedUserList[i].ConnectionID)
                            {
                                client.SendQueue.Enqueue(bytes);
                            }
                        }
                    }
                }
            }
           
        }


        private void SendMessage(string response, ClientContext sender)
        {
            sender.SendQueue.Enqueue(ASCIIEncoding.ASCII.GetBytes(response + "\r\n"));
        }
        private void ProcessMessages(ClientContext sender, string Message)
        {
            if (Message.StartsWith("GET "))
            {
                string mp = Message.Substring(4);
                mp = mp.Substring(0, mp.IndexOf(" "));
                if (mp.Substring(0, 1) == "/")
                {
                    mp = mp.Substring(1);
                }
                SendStuffToUIThread(3, sender.ConnID, mp); //Request Mountpoint
            }
            else if (Message.StartsWith("User-Agent: NTRIP "))
            {
                SendStuffToUIThread(4, sender.ConnID, Message.Replace("User-Agent: NTRIP ", "")); //Update User Agent
            }
            else if (Message.StartsWith("Authorization: Basic "))
            {
                SendStuffToUIThread(5, sender.ConnID, Message.Replace("Authorization: Basic ", "")); //Login
            }
            else if (Message.StartsWith("SOURCE ")) //Request ability to send stream
            {
                SendStuffToUIThread(6, sender.ConnID, Message.Replace("SOURCE ", ""));
            }
            else if (Message.StartsWith("Source-Agent: NTRIP "))
            {
                SendStuffToUIThread(4, sender.ConnID, Message.Replace("Source-Agent: NTRIP ", "")); //Update User Agent
            }
            else if (Message.StartsWith("$GPGGA") || Message.StartsWith("$GNGGA") || Message.StartsWith("BDGGA"))
            {
                SendStuffToUIThread(7, sender.ConnID, Message);
            }
            else
            {
                //Some other message that we don't care about
            }
        }


        //private void SendStuffToSocketsThread(int ConnID, string Message, bool Disconnect, int NewStatus)
        //{
        //    try
        //    {
        //        SendStuffToSocketsThreadDelegate uidel = new SendStuffToSocketsThreadDelegate(SendBackToClient);
        //        object[] o = new object[4];
        //        o[0] = ConnID;
        //        o[1] = Message;
        //        o[2] = Disconnect;
        //        o[3] = NewStatus;
        //        Invoke(uidel, o);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //delegate void SendStuffToSocketsThreadDelegate(int ConnID, string Message, bool Disconnect, int NewStatus);
        public void SendStuffToSocketsThread(int ConnID, string Message, bool Disconnect, int NewStatus)
        {
            //SyncLock clients
            string tempmp = "";
            foreach (ClientContext client in clients)
            {
                if (client.ConnID == ConnID)
                {
                    if (NewStatus == 3) //Means the message is the mountpoint
                    {
                        client.MountPoint = Message;
                    }
                    else
                    {
                        client.SendQueue.Enqueue(ASCIIEncoding.ASCII.GetBytes(Message + "\r\n"));
                        if (Disconnect)
                        {
                            client.RemoveAfterSend = true;
                        }
                        if (NewStatus > 0)
                        {
                            client.ConnLevel = NewStatus;
                            if (NewStatus == 2)
                            {
                                tempmp = client.MountPoint;
                            }
                        }
                    }
                }
            }
            if (NewStatus == 2 & tempmp.Length > 0) //this is a server sending data in, disconnect all other servers with same mountpoint
            {
                foreach (ClientContext client in clients)
                {
                    if (!(client.ConnID == ConnID))
                    {
                        if (client.MountPoint == tempmp && client.ConnLevel == 2)
                        {
                            client.RemoveFlag = true;
                        }
                    }
                }
            }
            //End SyncLock
        }

        //private void SendStuffToUIThread(int Action, int ConnID, string Message)
        //{
        //    try
        //    {
        //        SendStuffToUIThreadDelegate uidel = new SendStuffToUIThreadDelegate(ProcessOnUIThread);
        //        object[] o = new object[3];
        //        o[0] = Action;
        //        o[1] = ConnID;
        //        o[2] = Message;
        //        Invoke(uidel, o);
        //    }
        //    catch (Exception e)
        //    {
        //        int mm = 0;
        //    }
        //}
        //delegate void SendStuffToUIThreadDelegate(int Action, int ConnID, string Message);
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="Action"></param>
        /// <param name="ConnID"></param>
        /// <param name="Message"></param>
        public void SendStuffToUIThread(int Action, int ConnID, string Message)
        {
            switch (Action)
            {
                case -1: //On start up, list IP and port
                    LogEvent(Message);
                    break;
                case 0: //Connection count change
                    lblStatusBar.Text = "Connections: " + Message;
                    break;
                case 1: //Register user
                    UserAccount userAccount = new UserAccount();
                    userAccount.ConnectionID = ConnID;
                    userAccount.ConnectTime = DateTime.Now;
                    userAccount.UpdateTime = DateTime.Now;
                    connecedUserList.Add(userAccount);
                    break;
                case 2: //UnRegister user
                    for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                    {
                        if (connecedUserList[ctr].ConnectionID == ConnID)
                        {
                            connecedUserList.RemoveAt(ctr);
                        }
                    }
                    break;
                case 3: //Request Mountpoint
                    bool FoundMP_1 = false;
                    for (int ctr = mountpointList.Count - 1; ctr >= 0; ctr--)
                    {
                        if (mountpointList[ctr].JieRuDian == Message)
                        {
                            FoundMP_1 = true;
                        }
                    }

                    if (!FoundMP_1)
                    {
                        LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 接入点 " + Message +
                            " 不存在或未启动 ");
                        Message = "Sending SourceTable";
                    }

                    for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                    {
                        if (connecedUserList[ctr].ConnectionID == ConnID)
                        {
                            connecedUserList[ctr].MountPoint = Message;
                        }
                    }

                    if (FoundMP_1)
                    {
                        SendStuffToSocketsThread(ConnID, Message, false, 3);
                    }
                    else
                    {
                        GenerateAndSendSourceTable(ConnID);
                    }
                    break;

                case 4: //Update User Agent
                    for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                    {
                        if (connecedUserList[ctr].ConnectionID == ConnID)
                        {
                            connecedUserList[ctr].UA = Message;
                        }
                    }
                    break;

                case 5: //Login
                    for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                    {
                        if (connecedUserList[ctr].ConnectionID == ConnID)
                        {
                            if (connecedUserList[ctr].MountPoint.Length > 0) //A mountpoint was specified
                            {
                                DecryptLoginInfo(ConnID, Message);
                            }
                        }
                    }
                    break;

                case 6: //A server sending in the password and mount point
                    bool FoundMP = false;
                    bool GoodPassword = false;

                    string mountpoint = "";
                    if (Message.Contains(" "))
                    {
                        int firstspace = Message.IndexOf(" ");
                        if (Message.Length > firstspace + 2)
                        {
                            string password = Message.Substring(0, firstspace);
                            mountpoint = Message.Substring(firstspace + 2);
                            for (int ctr = mountpointList.Count - 1; ctr >= 0; ctr--)
                            {
                                if (mountpointList[ctr].JieRuDian == mountpoint)
                                {
                                    FoundMP = true;
                                    //if (mountpointList[ctr].JieRuDian,o == password)
                                    //{
                                    //    GoodPassword = true;
                                    //}
                                }
                            }
                        }
                    }
                    if (!FoundMP)
                    {
                        mountpoint = "NonExistant";
                    }
                    for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                    {
                        if (connecedUserList[ctr].ConnectionID == ConnID)
                        {
                            connecedUserList[ctr].MountPoint = mountpoint;
                        }
                    }
                    if (FoundMP)
                    {
                        if (GoodPassword)
                        {
                            for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                            {
                                if (connecedUserList[ctr].ConnectionID == ConnID)
                                {
                                    connecedUserList[ctr].ConnectTime = DateTime.Now;
                                    connecedUserList[ctr].Username = "--SERVER--";
                                    LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - Server " + connecedUserList[ctr].Address + " is streaming to mountpoint " + mountpoint + ".");
                                }
                            }
                            SendStuffToSocketsThread(ConnID, mountpoint, false, 3);
                            SendStuffToSocketsThread(ConnID, "ICY 200 OK", false, 2);
                        }
                        else
                        {
                            SendStuffToSocketsThread(ConnID, "ERROR - Bad Password", true, 0);
                        }
                    }
                    else
                    {
                        SendStuffToSocketsThread(ConnID, "ERROR - Invalid MountPoint", true, 0);
                    }
                    break;

                case 7:
                    string[] aryNMEAString = Message.Split(',');

                    if ((aryNMEAString.Length - 1) > 13) //we have at least 14 fields.
                    {
                        int InFixQuality = int.Parse(aryNMEAString[6]);
                        string gpstype = "";
                        string shorttype = "";
                        switch (InFixQuality)
                        {
                            case 1: //GPS fix (SPS)
                                gpstype = "GPS fix (No Differential Correction)";
                                shorttype = "单点解";
                                break;
                            case 2: //DGPS fix
                                gpstype = "DGPS";
                                shorttype = "差分解";
                                break;
                            case 3: //PPS fix
                                gpstype = "PPS Fix";
                                shorttype = "PPS";
                                break;
                            case 4: //Real Time Kinematic
                                gpstype = "RTK";
                                shorttype = "固定解";
                                break;
                            case 5: //Float RTK
                                gpstype = "Float RTK";
                                shorttype = "浮点解";
                                break;
                            case 6: //estimated (dead reckoning) (2.3 feature)
                                gpstype = "Estimated";
                                shorttype = "Estimated";
                                break;
                            case 7: //Manual input mode
                                gpstype = "Manual Input Mode";
                                shorttype = "Manual";
                                break;
                            case 8: //Simulation mode
                                gpstype = "Simulation";
                                shorttype = "Simulation";
                                break;
                            case 9: //WAAS
                                gpstype = "WAAS";
                                shorttype = "WAAS";
                                break;
                            default: //0 = invalid
                                gpstype = "Invalid";
                                shorttype = "Invalid";
                                break;
                        }
                        SharpGPS.NMEA.GPGGA gga = new SharpGPS.NMEA.GPGGA(Message);
                        for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                        {
                            if (connecedUserList[ctr].ConnectionID == ConnID)
                            {
                                connecedUserList[ctr].CurrentStatus = shorttype;
                                connecedUserList[ctr].CurrentLon = gga.Position.X;
                                connecedUserList[ctr].CurrentLat = gga.Position.Y;
                                connecedUserList[ctr].UpdateTime = DateTime.Now;
                                
                            }
                        }
                    }
                    break;
                case 99:
                    for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                    {
                        if (connecedUserList[ctr].MountPoint == Message && !(connecedUserList[ctr].Username == ""))
                        {
                            connecedUserList[ctr].DataSize = (connecedUserList[ctr].DataSize + ConnID);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// 向客户端发送接入点列表
        /// </summary>
        /// <param name="ConnID"></param>
        public void GenerateAndSendSourceTable(int ConnID)
        {
            string sourceTable = sourceHeader + getMountPoistsList() + sourceFoot;
            SendStuffToSocketsThread(ConnID, sourceTable, true, 0);

            //Remove client from the list
            for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
            {
                if (connecedUserList[ctr].ConnectionID == ConnID)
                {
                    if (connecedUserList[ctr].DataSize > 0)
                    {
                        LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + connecedUserList[ctr].Username + " 断开连接，已接收 " + connecedUserList[ctr].DataSize + " bytes.");
                    }
                    connecedUserList.RemoveAt(ctr);
                }
            }
        }

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <param name="ConnID"></param>
        /// <param name="Message"></param>
        public void DecryptLoginInfo(int ConnID, string Message)
        {
            bool LoginFound = false;
            string username = "";
            bool haveuser = false;
            int loginNum = 0;

            if (Message.Length > 1)
            {
                //Decrypt info
                byte[] originalbytes = Convert.FromBase64String(Message);
                string originaltext = System.Text.Encoding.ASCII.GetString(originalbytes);
                if (originaltext.IndexOf(":") >= 0)
                {
                    int colinlocation = originaltext.IndexOf(":");
                    if (originaltext.Length >= colinlocation + 1)
                    {
                        username = originaltext.Substring(0, colinlocation);
                        string password = originaltext.Substring(colinlocation + 1);
                        foreach (string key in DocumentSetting.Default.UserAccounts.Keys)
                        {
                            if (!DocumentSetting.Default.IsCheckPass) {
                                password = DocumentSetting.Default.UserAccounts[key].Password;
                            }
                            if (DocumentSetting.Default.UserAccounts[key].Username == username
                                && DocumentSetting.Default.UserAccounts[key].Password == password
                                && DocumentSetting.Default.UserAccounts[key].DeadLineTime> DateTime.Now)
                            {
                                LoginFound = true;
                                haveuser = true;
                            }
                            else
                            {
                                if (DocumentSetting.Default.UserAccounts[key].Username == username
                                && DocumentSetting.Default.UserAccounts[key].Password != password)
                                {
                                    haveuser = true;
                                    LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + username + " 断开连接,密码验证错误");
                                }
                                if (DocumentSetting.Default.UserAccounts[key].Username == username
                               && DocumentSetting.Default.UserAccounts[key].DeadLineTime < DateTime.Now)
                                {
                                    haveuser = true;
                                    LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + username + " 断开连接,用户已过期");
                                }
                            }
                        }
                        if (!haveuser)
                        {
                            LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + username + " 不存在");
                        }

                        for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                        {
                            DateTime conTime = connecedUserList[ctr].UpdateTime;
                            if (((connecedUserList[ctr].CurrentLat == 0 || connecedUserList[ctr].CurrentLon == 0) && conTime < DateTime.Now.AddMinutes(-5))//接入5分钟用未上传gga
                               || (string.IsNullOrEmpty(connecedUserList[ctr].Username) && connecedUserList[ctr].ConnectTime < DateTime.Now.AddSeconds(-1))//连接一分钟未上传用户名
                               )
                            {
                                for (int i = 0; i <= clients.Count - 1; i++)
                                {
                                    if (((ClientContext)(clients[i])).ConnID == connecedUserList[ctr].ConnectionID)
                                    {
                                        ((ClientContext)(clients[i])).RemoveFlag = true;
                                        LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + username + ",被强制下线！");
                                    }

                                }
                            }
                            if (connecedUserList[ctr].Username == username && !string.IsNullOrEmpty( username))
                            {
                                loginNum++;
                                //for (int i = 0; i <= clients.Count - 1; i++)
                                //{
                                //    if (((ClientContext)(clients[i])).ConnID.ToString() == connecedUserList[ctr].ConnectionID.ToString())
                                //    {
                                        //((ClientContext)(clients[i])).RemoveFlag = true;
                                        //LoginFound = false;
                                //        LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + username + " 已登录,被强制下线！");
                                //        break;
                                //    }

                                //}

                            }
                        }
                    }
                }
            }
            if (connecedUserList.Count >= DocumentSetting.Default.MaxConnectionCount) {
                LogEvent("系统已达最大连接数量 - 用户 " + username + " 登录失败");
                LoginFound = false;
            }
            if (loginNum >= DocumentSetting.Default.MaxUserCount)
            {
                LogEvent("用户登录数量已达最大连接数量 - 用户 " + username + " 登录失败");
                LoginFound = false;
            }

            if (LoginFound)
            {
                LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + username + " 登录成功");
                for (int ctr = connecedUserList.Count - 1; ctr >= 0; ctr--)
                {
                    if (connecedUserList[ctr].ConnectionID == ConnID)
                    {
                        connecedUserList[ctr].Username = username;
                    }
                }
                SendStuffToSocketsThread(ConnID, "ICY 200 OK", false, 1);
                LogEvent("Connection ID " + System.Convert.ToString(ConnID) + " - 用户 " + username + " 已连接.");
            }
            else 
            {
                SendStuffToSocketsThread(ConnID, "401 Unauthorized", true, 0);
            }
        }

        public void SendBytesToCasterThread(string name, string MountPoint, byte[] bytes)
        {
            PropagateStreamData(name,MountPoint, bytes);
        }

        /// <summary>
        /// 发送肠粉数据至客户端
        /// </summary>
        /// <param name="MountPoint">接入点</param>
        /// <param name="bytes">数据流</param>
        public void SendBytesToCasterThread(string MountPoint, byte[] bytes)
        {
            PropagateStreamData(null,MountPoint, bytes);
            //try
            //{
            //    SendBytesToCasterThreadDelegate uidel = new SendBytesToCasterThreadDelegate(PropagateStreamData);
            //    object[] o = new object[2];
            //    o[0] = MountPoint;
            //    o[1] = bytes;
            //    Invoke(uidel, o);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }
        delegate void SendBytesToCasterThreadDelegate(string MountPoint, byte[] bytes);
    }



    public class ClientContext
    {
        private Socket _socket;
        public bool FirstTime = true;
        public int ConnID = 0;
        public string IncomingData = "";
        public int ConnLevel = 0;
        public string MountPoint = "";
        public bool RemoveAfterSend = false;

        public ClientContext(Socket Socket)
        {
            _socket = Socket;
            FirstTime = true;
            CasterForm.ConnIDCount++;
            ConnID = CasterForm.ConnIDCount;
            IncomingData = "";
            ConnLevel = 0;
            MountPoint = "";
            RemoveAfterSend = false;
        }
        public Socket Socket
        {
            get
            {
                return _socket;
            }
        }


        private Queue _sendQueue;
        public Queue SendQueue
        {
            get
            {
                if (_sendQueue == null)
                {
                    _sendQueue = new Queue();
                }
                return _sendQueue;
            }
        }



        private bool _removeFlag;
        public bool RemoveFlag
        {
            get
            {
                return _removeFlag;
            }
            set
            {
                _removeFlag = value;
            }
        }

        public bool IsConnected()
        {
            bool connected = false;
            try
            {
                connected = !(Socket.Poll(1, SelectMode.SelectRead) && (Socket.Available == 0));
                bool blockingState = Socket.Blocking;
                try
                {
                    byte[] tmp = new byte[1];

                    Socket.Blocking = false;
                    Socket.Send(tmp, 0, 0);
                }
                catch (SocketException e)
                {
                    // 10035 == WSAEWOULDBLOCK
                    if (e.NativeErrorCode.Equals(10035))
                    {
                        //Console.WriteLine("Still Connected, but the Send would block");
                    }
                    else
                    {
                        //Console.WriteLine("Disconnected: error code {0}!", e.NativeErrorCode);
                        connected = false;
                    }
                }
                finally
                {
                    Socket.Blocking = blockingState;
                }
            }
            catch
            {
            }
            return connected;
        }

        public void SendNow(string message)
        {
            try
            {
                if (Socket != null)
                {
                    if (IsConnected())
                    {
                        byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(message);
                        Socket.Send(bytes, bytes.Length, SocketFlags.None);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
