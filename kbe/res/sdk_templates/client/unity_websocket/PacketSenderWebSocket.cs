using NativeWebSocket;

namespace KBEngine
{
    public class PacketSenderWebSocket : PacketSenderBase
    {
        WebSocket _websocket;
        public PacketSenderWebSocket(NetworkInterfaceBase networkInterface) : base(networkInterface)
        {
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

