using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using NativeWebSocket;
using System.Threading.Tasks;
using System;

namespace KBEngine
{
    public class NetworkInterfaceWebSocket : NetworkInterfaceBase
    {
        ConnectState state;
        private MemoryStream stream = new MemoryStream();

        public override bool valid()
        {
            return _websocket != null && (_websocket.State == WebSocketState.Open);
        }

        protected override PacketReceiverBase createPacketReceiver()
        {
            return new PacketReceiverWebSocket(this);
        }

        protected override PacketSenderBase createPacketSender()
        {
            return new PacketSenderWebSocket(this);
        }

        protected override Socket createSocket()
        {
            throw new System.NotImplementedException();
        }

        protected override WebSocket createWebSocket(ConnectState state)
        {
            
            string url;
            if(state.connectIP.StartsWith("ws://") || state.connectIP.StartsWith("wss://"))
            {
                url = $"{state.connectIP}:${state.connectPort}";
            }
            else
            {
                url = $"ws://{state.connectIP}:{state.connectPort}";
            }
            _websocket = new WebSocket(url);
            _websocket.OnOpen += OnOpen;
            _websocket.OnError += OnError;
            _websocket.OnClose += OnClose;
            this.state = state;
            Debug.Log($"WebSocket::createWebSocket {url}");
            return _websocket;
        }

        async protected override void onAsyncConnect(ConnectState state)
        {
            Debug.Log($"WebSocket::onAsyncConnect {state.websocket.State}");
            await state.websocket.Connect();
        }

        async public void Connect()
        {
            await state.websocket.Connect();
        }

        void OnOpen()
        {
            Debug.Log($"WebSocket::Connection open! {_websocket.State}");
            Event.fireIn("_onConnectionState", new object[] { state });
        }

        void OnError(string err)
        {
            Debug.Log($"WebSocket::Error! {err}");
        }

        void OnClose(WebSocketCloseCode code)
        {
            Debug.Log("WebSocket::Connection closed!");
        }

        public override void process()
        {
            base.process();
#if !UNITY_WEBGL || UNITY_EDITOR
            if(_websocket != null)
            {
                // 不加这个OnMessage无法收到消息
                _websocket.DispatchMessageQueue();
            }
#endif
        }
    }
}
