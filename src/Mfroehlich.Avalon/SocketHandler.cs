using System;
using System.Linq;
using System.Threading.Tasks;
using Mfroehlich.Avalon.Game;
using Microsoft.AspNetCore.Http;

namespace Mfroehlich.Avalon
{
    public class SocketHandler
    {
        private readonly RequestDelegate next;
        private readonly GameManager game;

        public SocketHandler(GameManager game, RequestDelegate next)
        {
            this.next = next;
            this.game = game;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest) {
                await next.Invoke(context);
                return;
            }

            var name = context.Request.Query["name"];
            if (name.Count != 1) {
                context.Response.StatusCode = 400;
                return;
            }

            if (!game.ValidateName(name)) {
                context.Response.StatusCode = 400;
                return;
            }

            var ws = await context.WebSockets.AcceptWebSocketAsync("protocolTwo");
            var socket = new Socket(ws);
            await game.AddSocket(socket, name);
            await socket.ReadLoop();
        }
    }
}