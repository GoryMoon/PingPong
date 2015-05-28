using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PingPong.Network
{
    public class NetworkWrapper
    {

        private Dictionary<int, Network> messages = new Dictionary<int,Network>();

        public void registerMessage(Type message, Type messageHandler, int id)
        {
            if (!messages.ContainsKey(id))
            {
                messages.Add(id, new Network(message, messageHandler));
            }
        }

        public void onMessage(string result)
        {
            byte[] b = Convert.FromBase64String(result);
            BinaryReader br = new BinaryReader(new MemoryStream(b));
            int id = br.ReadInt32();

            IMessage message = ((IMessage)Activator.CreateInstance(messages[id].message));
            message.fromBytes(br);

            IMessageHandler handler = ((IMessageHandler)Activator.CreateInstance(messages[id].handler));
            var m = messages[id].handler.GetMethod("onMessage");
            var g = m.Invoke(handler, new object[] { message });
        }

        public void sendMessage<T>(IMessage message)
        {
            int id = messages.FirstOrDefault(x => x.Value.message == typeof(T)).Key;

            BinaryWriter bw = new BinaryWriter(new MemoryStream());
            bw.Write(id);
            message.toBytes(bw);
            byte[] bytes = ((MemoryStream)bw.BaseStream).ToArray();

            string result = Convert.ToBase64String(bytes);
            onMessage(result);
        }

    }


    struct Network
    {
        public Type message;
        public Type handler;

        public Network(Type message, Type handler)
        {
            this.message = message;
            this.handler = handler;
        }
    }
}
