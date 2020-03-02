using MLAPI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;
    public ServerClient(TcpClient client)
    {
        clientName = "Guest";
        tcp = client;
    }
}

public class ServerTcp : NetworkedBehaviour
{
    List<ServerClient> Clients;
    List<ServerClient> ClientsDC;
    public string ip = "127.0.0.1";
    public int port = 6321;
    TcpListener server;
    bool serverStarted = false;
    private void Start()
    {
        Clients = new List<ServerClient>();
        ClientsDC = new List<ServerClient>();
        try
        {
            ip = $"{GetIPAddress(System.Net.Dns.GetHostName())}";
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            StartedListening();
            serverStarted = true;
            Debug.Log($"{server.LocalEndpoint}");
            Debug.Log($"{server.Server.RemoteEndPoint}");

        }
        catch (Exception e)
        {
            Debug.Log($"Socket Error:{e.Message}");
        }
    }
    public static string GetIPAddress(string hostname)
    {
        IPHostEntry host;
        host = Dns.GetHostEntry(hostname);

        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return string.Empty;
    }
    private void Update()
    {
        if (!serverStarted)
        {
            return;
        }
        foreach (ServerClient c in Clients)
        {
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                ClientsDC.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader rdr = new StreamReader(s, true);
                    string data = rdr.ReadLine();
                    if (data != null)
                    {
                        OnIncomingData(c, data);
                    }
                }
            }
        }
    }

    private void OnIncomingData(ServerClient c, string data)
    {
        Debug.Log($"Client Name: {c.clientName} : {data}");
    }

    private void StartedListening()
    {
        server.BeginAcceptSocket(AcceptTcpClient, server);
    }
    bool IsConnected(TcpClient tcpc)
    {
        try
        {
            if (tcpc != null && tcpc.Client != null && tcpc.Client.Connected)
            {
                if (tcpc.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(tcpc.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
    void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener tcpl = (TcpListener)ar.AsyncState;
        Clients.Add(new ServerClient(tcpl.EndAcceptTcpClient(ar)));
    }
}
