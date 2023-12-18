
namespace KBEngine
{
    public class PacketReceiverWebSocket : PacketReceiverBase
    {
        private byte[] _buffer;

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
            _messageReader.process(buffer, 0, (uint)buffer.Length);
        }
    }
}
