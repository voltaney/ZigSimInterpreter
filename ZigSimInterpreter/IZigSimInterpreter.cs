namespace ZigSimInterpreter
{
    public interface IZigSimInterpreter
    {
        public ZigSimResult Read(byte[] input);
    }
}
