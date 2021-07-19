namespace Mfroehlich.Common.HttpOptions
{
    public class HttpOptions
    {
        public int Port { get; set; } = 8080;
        public string Logs { get; set; } = "./logs";
    }
}