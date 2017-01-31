using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Mfroehlich.Avalon
{
    public class Socket
    {
        private static JsonSerializer Json = JsonSerializer.Create(new JsonSerializerSettings {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        public event EventHandler Closed;
        public event EventHandler<JObject> Received;

        private readonly WebSocket socket;

        public Socket(WebSocket socket)
        {
            this.socket = socket;
        }

        public async Task SendAsync(string key, object value)
        {
            using (var mem = new MemoryStream(4096)) {
                using (var text = new StreamWriter(mem)) {
                    Json.Serialize(text, new
                    {
                        type = key,
                        body = value
                    });
                }

                ArraySegment<byte> buffer;
                if (!mem.TryGetBuffer(out buffer)) {
                    throw new Exception("???");
                }

                await socket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        public async Task ReadLoop()
        {
            var buffer = new byte[4096];
            var seg = new ArraySegment<byte>(buffer);

            try {
                while (socket.State == WebSocketState.Open) {
                    var incoming = await socket.ReceiveAsync(seg, CancellationToken.None);
                    var str = Encoding.UTF8.GetString(seg.Array, seg.Offset, incoming.Count);
                    var json = JObject.Parse(str);
                    Received?.Invoke(this, json);
                }
            }
            catch (Exception) {
                //
            }

            Closed?.Invoke(this, new EventArgs());
        }
    }
}