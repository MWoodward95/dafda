using System;

namespace Dafda.Consuming
{
    internal sealed class ThrowDefaultErrorsStrategy : IMalformedMessageStrategy
    {
        public TransportLevelMessage Create(Func<TransportLevelMessage> p)
        {
            return p();
        }
    }
}