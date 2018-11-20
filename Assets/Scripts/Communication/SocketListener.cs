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
            if(networkStream.DataAvailable)
            {
                Debug.Log("Received data: " + reader.ReadToEnd());
            }
        }
	}

    // Process the client connection.
    void DoAcceptTcpClientCallback(IAsyncResult ar)
    {
        client = server.EndAcceptTcpClient(ar);
        /** Setup streams to read and write */

        networkStream = client.GetStream();
        writer = new StreamWriter(networkStream);
        reader = new StreamReader(networkStream);

        Debug.Log("Client connected completed");
    }

    void OnDestroy()
    {
        /** Clear socket connection */
        if (client != null && client.Connected) client.Close();
        server.Stop();
    }
}
