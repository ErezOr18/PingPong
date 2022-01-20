namespace pingPong.Common
{
    public class RequestInfo
    {
        RequestType Type { get; }
        uint Length { get; }
        public byte[] Data { get; }

        public RequestInfo(RequestType type, byte[] data)
        {
            Type = type;
            Length = (uint)data.Length;
            Data = data;
        }
    }
}
