using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PingPong.Network
{
    public interface IMessageHandler
    {

    }

    public interface IMessageHandler<T, L>: IMessageHandler
    {

        L onMessage(T message);

    }
}
