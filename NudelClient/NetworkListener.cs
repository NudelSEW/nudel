﻿using JustConnect.Tcp;
using Newtonsoft.Json;
using Nudel.Networking.Requests.Base;
using Nudel.Networking.Responses;
using Nudel.Networking.Responses.Base;
using System;
using System.Net;
using TcpClient = JustConnect.Tcp.Client;

namespace Nudel.Client
{
    public static class NetworkListener
    {
        private const int PORT = 8181;
        private static TcpClient client;
        private static readonly JsonSerializerSettings jsonSettings;

#if DEBUG
        public static event ClientLogHandler Log;
#endif

        static NetworkListener()
        {
            client = new TcpClient(PORT);

            jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };

#if DEBUG
            client.Log += Log;
#endif
            
            client.Received += ProcessResponse;
        }

        public static void Start()
        {
            client.Connect(IPAddress.Loopback);
        }

        public static void Stop()
        {
            client.Disconnect();
        }

        public static void SendRequest(Request request)
        {
            string requestString = JsonConvert.SerializeObject(request, jsonSettings);
            client.Send(requestString);
        }

        public static void ProcessResponse(string data)
        {
            Object rawResponse = JsonConvert.DeserializeObject<Object>(data, jsonSettings);

            Log?.Invoke($"Received Response of Type: {rawResponse.GetType()}");

            if (rawResponse is LoginRegisterResponse)
            {
                LoginRegisterResponse response = rawResponse as LoginRegisterResponse;

                MainModel.SessionToken = response.SessionToken;
            }
            else if (rawResponse is GetUserResponse)
            {
                GetUserResponse response = rawResponse as GetUserResponse;

                MainModel.User = response.User;
            }
        }
    }
}
