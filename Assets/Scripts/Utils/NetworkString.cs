using Unity.Collections;
using Unity.Netcode;

namespace SimonSays.Net
{
    public class NetworkString : INetworkSerializable
    {
        private FixedString32Bytes data;
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref data);
        }

        public override string ToString() => data.ToString();

        public static implicit operator string(NetworkString s) => s.ToString();
        public static implicit operator NetworkString(string s) => new() { data = new FixedString32Bytes(s) };
    }
}