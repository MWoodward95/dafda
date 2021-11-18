using System;

namespace Dafda.Consuming
{
    internal interface IMalformedMessageStrategy
    {
        TransportLevelMessage Create(Func<TransportLevelMessage> p);
    }
}