using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PingPong.Network
{
    public interface IMessage
    {

        void fromBytes(BinaryReader reader);
        void toBytes(BinaryWriter writer);

    }
}
