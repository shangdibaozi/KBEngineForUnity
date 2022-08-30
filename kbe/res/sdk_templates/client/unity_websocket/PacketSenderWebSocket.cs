using KBEngine;
using NativeWebSocket;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

namespace KBEngine
{
    public class PacketSenderWebSocket : PacketSenderBase
    {
        private byte[] _buffer;

        int _wpos = 0;              // д�������λ��
        int _spos = 0;              // ������ϵ�����λ��

        object _sendingObj = new object();
        Boolean _sending = false;
        WebSocket _websocket;
        public PacketSenderWebSocket(NetworkInterfaceBase networkInterface) : base(networkInterface)
        {
            _buffer = new byte[KBEngineApp.app.getInitArgs().TCP_SEND_BUFFER_MAX];

            _wpos = 0;
            _spos = 0;
            _sending = false;
            _websocket = networkInterface.websocket;
        }

        public override bool send(MemoryStream stream)
        {
            int dataLength = (int)stream.length();
            if (dataLength <= 0)
                return true;

            _websocket.Send(stream.getbuffer());

            return true;
        }

        protected override void _asyncSend()
        {
            
        }
    }
}

