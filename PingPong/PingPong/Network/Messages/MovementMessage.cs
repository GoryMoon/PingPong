using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PingPong.Network.Messages
{
    public class MovementMessage: IMessage, IMessageHandler<MovementMessage, IMessage>
    {
        String s;
        int i;
        float f;
        bool b;
        char c;

        public void fromBytes(BinaryReader reader)
        {
            s = reader.ReadString();
            i = reader.ReadInt32();
            f = reader.ReadSingle();
            b = reader.ReadBoolean();
            c = reader.ReadChar();
        }

        public void toBytes(BinaryWriter writer)
        {
            writer.Write("Hej på dig");
            writer.Write(341);
            writer.Write(0.002452f);
            writer.Write(false);
            writer.Write('g');
        }

        public IMessage onMessage(MovementMessage message)
        {

            Log.debug("S:" + message.s);
            Log.debug("I:" + message.i);
            Log.debug("F:" + message.f);
            Log.debug("B:" + message.b);
            Log.debug("C:" + message.c + "\n\n\n");

            return null;
        }
    }
}
