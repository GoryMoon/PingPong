using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PingPong.Network.Messages;

namespace PingPong.Network
{
    public class ClientPacketHandler
    {

        public static NetworkWrapper INSTANCE = new NetworkWrapper();

        public static void init()
        {
            INSTANCE.registerMessage(typeof(MovementMessage), typeof(MovementMessage), 0);
        }

    }
}
