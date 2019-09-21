using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;
using System;
//AUTHOR : 梁振东
//DATE : 09/15/2019 17:10:47
//DESC : ****
public class DefaultSocketConnection : MonoBehaviour
{
    public enum EventType
    {
        Disconnected,
        ReconnectInstantly,
        Connected,
        Login,
        SendCacheMessage
    }
    public class SocketEvent
    {
        public EventType type;
        public object param;
        public SocketEvent(EventType type)
        {
            this.type = type;
        }
        public SocketEvent(EventType type, object param)
        {
            this.type = type;
            this.param = param;
        }
    }
    public class SocketEventArgs : EventArgs
    {
        private string data;
        public string Data
        {
            get
            {
                return data;
            }
        }
        public SocketEventArgs(string data)
        {
            this.data = data;
        }
    }
    public class SocketMessageEventArgs : EventArgs
    {
        
    }
    TcpClient client;
    private byte[] buffer;
    private const int bufferSize = 1024;
    private NetworkStream streamToServer;
    private Action<string> callback;
    public static bool autoTryConnect = true;
    public event EventHandler<SocketEventArgs> OnSocketDataCallBack;
    Queue<string> dataQueue = new Queue<string>();
    private Queue<SocketEvent> socketsEventsQueue = new Queue<SocketEvent>();

    public static T Create<T>(Action<string> callback) where T : DefaultSocketConnection
    {
        GameObject obj = new GameObject("SocketConnection");
        DefaultSocketConnection instance = obj.AddComponent<DefaultSocketConnection>();
        instance.callback = callback;
        return instance as T;
    }
    byte[] zeroBytes = new byte[4]{0, 0, 0, 0};
    public Boolean ready
    {
        get
        {
            if(client == null)
              return false;
            if(client.Connected)
            {
                try
                {
                    client.GetStream().Write(zeroBytes, 0, 4);
                }
                catch(SocketException ex)
                {
                    Close(false);
                    Debug.LogError("DefaultSocket :: Ready" + ex.Message);
                    return false;

                }
                catch(IOException ex)
                {
                    Close(false);
                    Debug.LogError("DefaultSocket :: Ready" + ex.Message);
                    return false;
                }
            }
            return client.Connected;
        }
    }
    void Close(bool bForceReconnect = false)
    {
        if(client != null)
        {
            client.Close();
            client = null;
        }
        if(streamToServer != null)
        {
            streamToServer.Close();
            streamToServer = null;
        }
        if(bForceReconnect)
        {
            SocketEvent socketEvent = new SocketEvent(EventType.ReconnectInstantly);
            socketsEventsQueue.Enqueue(socketEvent);
        }
    }
    public virtual void Connect()
    {
        
    }
    public void Connect(string address, int port, string playerData)
    {
        Debug.LogWarning("DefaultSocketConnection Connect : " + port);
        try
        {
            IPHostEntry host = Dns.GetHostEntry(address);
            IPAddress ip = null;
            if(host != null)
            {
                ip = host.AddressList[0];
            }
            client = new TcpClient(ip != null ? ip.AddressFamily : AddressFamily.InterNetwork);
            client.BeginConnect(ip, port, new AsyncCallback(AfterConnected), playerData);

            //第二种写法
            // IPAddress ipAddress = IPAddress.Parse(address);
            // IPEndPoint endpoint = new IPEndPoint(ipAddress, port);
            // client = new TcpClient(endpoint);
        }
        catch(Exception ex)
        {
            Debug.LogError("DefaultSocketConnection :: Connect() - " + address + ":" + port);
            client = null;
        }
    }
    void AfterConnected(IAsyncResult result)
    {
        client.EndConnect(result);
        string playerData = (string)result.AsyncState;
        streamToServer = client.GetStream();
        SocketEvent e = new SocketEvent(EventType.Login, playerData);
        socketsEventsQueue.Enqueue(e);
        StartRead();
    }
    void StartRead()
    {

    }
    void Awake()
    {
        client = new TcpClient(); //创建client
        buffer = new byte[bufferSize];

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
