using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebSockets;
using System.Web;
using System.Web.Http;
using System.Collections.Concurrent;
using wsep182.Domain;
using wsep182.services;

namespace WebServices.Controllers
{
    public class WebSocketController : ApiController
    {
        static readonly Dictionary<string, WebSocket> _users = new Dictionary<string, WebSocket>();
        public static Dictionary<string, LinkedList<String>> PendingMessages = new Dictionary<string, LinkedList<String>>(); 


        public HttpResponseMessage Get()
        {
            if (System.Web.HttpContext.Current.IsWebSocketRequest)
            {
                System.Web.HttpContext.Current.AcceptWebSocketRequest(Process);
            }
            return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }

        private async Task Process(AspNetWebSocketContext context)
        {
            WebSocket socket = context.WebSocket;
            ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[4096]);
            string hash = "";
            if(context.CookieCollection[0].Name == "HashCode")
            {
                hash = context.CookieCollection[0].Value;
            }
            else if(context.CookieCollection.Count > 1 && context.CookieCollection[1].Name == "HashCode")
            {
                hash = context.CookieCollection[1].Value;
            }
            else
            {
                return;
            }

            updateSocket(hash, socket);

            if (socket.State == WebSocketState.Open)
            {
                User newConnectedUser = hashServices.getUserByHash(hash);
                LinkedList<String> CurrentPendingMessages;
                PendingMessages.TryGetValue(newConnectedUser.getUserName(), out CurrentPendingMessages);
                if (CurrentPendingMessages != null)
                {
                    foreach (String message in CurrentPendingMessages)
                    { 
                        sendMessageToClient(hash, message);
                    }
                    CurrentPendingMessages.Clear();
                }
            }


            while (socket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await socket.ReceiveAsync(buffer, CancellationToken.None)
                                                            .ConfigureAwait(false);
                String userMessage = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                userMessage = "You sent: " + userMessage + " at " +
                    DateTime.Now.ToLongTimeString() + " from ip " + context.UserHostAddress.ToString();
                var sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(userMessage));


                await socket.SendAsync(sendbuffer, WebSocketMessageType.Text, true, CancellationToken.None)
                            .ConfigureAwait(false);
            }
        }

        public static void sendMessageToClient(string hash, String message)
        {
            WebSocket socket=null;
            _users.TryGetValue(hash, out socket);
            if (socket == null)
            {
                addMessage(hash, message);
                return; //no such socket exists
            }
            try
            {
                if (socket.State == WebSocketState.Open)
                {

                    var sendbuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));

                    socket.SendAsync(sendbuffer, WebSocketMessageType.Text, true, CancellationToken.None)
                                .ConfigureAwait(false);
                }
                else
                {
                    lock (_users) //make sure the socket wasn't reconnected so we won't lose the socket
                    {
                        _users.Remove(hash);
                        addMessage(hash, message);
                    }

                }
            }
            catch(System.ObjectDisposedException /*e*/)
            {
                _users.Remove(hash);
                addMessage(hash, message);
            }
        }

        public static void addMessage(string hash, String message)
        {
            User newConnectedUser = hashServices.getUserByHash(hash);
            LinkedList<String> CurrentPendingMessages;
            PendingMessages.TryGetValue(newConnectedUser.getUserName(), out CurrentPendingMessages);
            if (CurrentPendingMessages == null)
            {
                CurrentPendingMessages = new LinkedList<String>();
                CurrentPendingMessages.AddLast(message);
                PendingMessages.Add(newConnectedUser.getUserName(), CurrentPendingMessages);
            }
            else
            {
                CurrentPendingMessages.AddLast(message);
            }
        }

        private static void updateSocket(String hash, WebSocket socket)
        {
            WebSocket soc;
            _users.TryGetValue(hash, out soc);
            if (soc != null)
                _users.Remove(hash);
            _users.Add(hash, socket);
        }

    }
}