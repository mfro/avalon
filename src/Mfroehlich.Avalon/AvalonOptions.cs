using Mfroehlich.Common.HttpOptions;

namespace Mfroehlich.Avalon
{
    public class AvalonOptions : HttpOptions
    {
        public string[] Origins { get; set; }
    }
}