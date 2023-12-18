using System.Net.Sockets;
using UnityEngine;
using NativeWebSocket;

namespace KBEngine
{
    public class NetworkInterfaceWebSocket : NetworkInterfaceBase
    {
        private string url;
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
            string connectPort = state.connectPort > 0 ? $":{state.connectPort}" : "";
            if(state.connectIP.StartsWith("ws://") || state.connectIP.StartsWith("wss://"))
            {
                url = $"{state.connectIP}{connectPort}";
            }
            else
            {
                url = $"ws://{state.connectIP}{connectPort}";
            }
            _websocket = new WebSocket(url);
            _websocket.OnOpen += OnOpen;
            _websocket.OnError += OnError;
            _websocket.OnClose += OnClose;
            this.state = state;
            Debug.Log($"WebSocket::createWebSocket {url}");
            return _websocket;
        }

        protected override async void onAsyncConnect(ConnectState state)
        {
            Debug.Log($"WebSocket::onAsyncConnect {state.websocket.State} {url}");
            await state.websocket.Connect();
        }

        public async void Connect()
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
            Debug.Log($"WebSocket::Connection closed! {url}");
            close();
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
