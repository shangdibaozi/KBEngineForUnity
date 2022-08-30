using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace KBEngine
{
    public class PacketReceiverWebSocket : PacketReceiverBase
    {
        private byte[] _buffer;

        // socket向缓冲区写的起始位置
        int _wpos = 0;

        // 主线程读取数据的起始位置
        int _rpos = 0;

        public PacketReceiverWebSocket(NetworkInterfaceBase networkInterface) : base(networkInterface)
        {
            //_buffer = new byte[KBEngineApp.app.getInitArgs().TCP_RECV_BUFFER_MAX];
            _messageReader = new MessageReaderTCP();
            networkInterface.websocket.OnMessage += OnMessage;
        }

        public override void process()
        {
            
        }

        protected override void _asyncReceive()
        {
            throw new System.NotImplementedException();
        }

        public void OnMessage(byte[] buffer)
        {
            //Debug.Log($"WebSocket::OnMessage buffer Length {buffer.Length}");
            //Array.Copy(buffer, 0, _buffer, 0, buffer.Length);
            //_wpos = buffer.Length;
            //_rpos = 0;
            _messageReader.process(buffer, 0, (uint)buffer.Length);
        }
    }
}
