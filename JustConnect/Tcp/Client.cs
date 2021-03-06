﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace JustConnect.Tcp
{
    public delegate void ClientLogHandler(string data);
    public delegate void ClientReceivedHandler(string data);

    /// <summary>
    // client is accomplishing the actions for the user (connecting, disconnecting and sending or receiving of elements)
    /// </summary>
    public class Client
    {
        private const int BUFFER_SIZE = 2048;
        private Socket socket;
        private byte[] buffer = new byte[BUFFER_SIZE];

        public bool IsConnected { get; set; }
        public int Port { get; set; }

        public event ClientLogHandler Log;
        public event ClientReceivedHandler Received;

        public Client() : this(3131) { }
        public Client(int port)
        {
            Port = port;
        }
        /// <summary>
        /// connecting to nudel server with ip address. checking if connected and returning a message
        /// </summary>
        /// <param name="ip"> ipaddress of the program</param>
        public void Connect(IPAddress ip)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (!IsConnected)
            {
                try
                {
                    socket.Connect(ip, Port);
                    socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, Receive, socket);

                    IsConnected = true;

                    Log?.Invoke("Client connected");
                }
                catch (SocketException e)
                {
                    Log?.Invoke("Could not connect to server");
                }
            }
            else
            {
                Log?.Invoke("Client already connected");
            }
        }
        /// <summary>
        /// closing the connection of the socket and return client disconnecting
        /// </summary>
        public void Disconnect()
        {
            socket.Disconnect(false);
            socket.Close();

            IsConnected = false;

            Log?.Invoke("Client disconnected");
        }

        /// <summary>
        /// trying if the message was received and returning message if not
        /// </summary>
        /// <param name="AR"></param>
        private void Receive(IAsyncResult AR)
        {
            Socket serverSocket = (Socket)AR.AsyncState;

            try
            {
                int received = serverSocket.EndReceive(AR);

                byte[] recBuf = new byte[received];
                Array.Copy(buffer, recBuf, received);
                string data = Encoding.ASCII.GetString(recBuf);

                Received?.Invoke(data);

                socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, Receive, socket);
            }
            catch (Exception e)
            {
                if (e is SocketException || e is ObjectDisposedException)
                {
                    socket = null;
                    IsConnected = false;

                    Log?.Invoke("Client disconnected");

                    return;
                }
            }
        }
        /// <summary>
        /// sending encoded message via TCP
        /// </summary>
        /// <param name="data"></param>
        public void Send(String data)
        {
            new Thread(() =>
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                socket.Send(buffer, 0, buffer.Length, SocketFlags.None);
            }).Start();
        }
    }
}
