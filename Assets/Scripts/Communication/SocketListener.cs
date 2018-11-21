using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

using UnityEngine;

public class SocketListener : MonoBehaviour
{
    TcpClient client;
    TcpListener server;
    Byte[] bytes;
    String data;

    NetworkStream networkStream;
    StreamWriter writer;
    StreamReader reader;

    public string HostAdress = "127.0.0.1";
    public int HostPort = 5111;

	void Start ()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(HostAdress), HostPort);
        server = new TcpListener(endPoint);
        server.Start();

        Debug.Log("Server Launched");
        Debug.Log("Wait for client connection...");

        /** Set async client connection */
        server.BeginAcceptTcpClient(DoAcceptTcpClientCallback, server);
	}
	
	void Update ()
    {
        if(client != null && client.Connected)
        {
            /** Receive messages */
            string data = "";
            while(networkStream.DataAvailable)
            {
                char[] bytes = new char[1024];
                reader.Read(bytes, 0, 1024);
                data += new string(bytes);
            }

            /** Process messages */
            if(data != "")
            {
                /** Deserialize data */
            }
        }
	}
    
    void DoAcceptTcpClientCallback(IAsyncResult ar)
    {
        client = server.EndAcceptTcpClient(ar);

        /** Setup streams to read and write */
        networkStream = client.GetStream();
        writer = new StreamWriter(networkStream);
        reader = new StreamReader(networkStream);

        Debug.Log("Client connection completed");
    }

    void OnDestroy()
    {
        /** Clear socket connection */
        if (client != null && client.Connected) client.Close();
        server.Stop();
    }
}
