using System.Net.Sockets;
using UnityEngine;
using UnityWebSocket;

namespace KBEngine
{
    public class NetworkInterfaceWebSocket : NetworkInterfaceBase
    {
        private string url;
        ConnectState state;
        private MemoryStream stream = new MemoryStream();

        public override bool valid()
        {
            return _websocket != null && (_websocket.ReadyState == WebSocketState.Open);
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
            Debug.Log($"WebSocket::onAsyncConnect {state.websocket.ReadyState} {url}");
            state.websocket.ConnectAsync();
        }

        public void Connect()
        {
            state.websocket.ConnectAsync();
        }

        void OnOpen(object sender, OpenEventArgs e)
        {
            Debug.Log($"WebSocket::Connection open! {_websocket.ReadyState}");
            EventMgr.Fire("_onConnectionState", state);
        }

        void OnError(object sender, ErrorEventArgs e)
        {
            Debug.Log($"WebSocket::Error! {e}");
        }

        void OnClose(object sender, CloseEventArgs e)
        {
            Debug.Log($"WebSocket::Connection closed! {url}");
            close();
        }
    }
}
