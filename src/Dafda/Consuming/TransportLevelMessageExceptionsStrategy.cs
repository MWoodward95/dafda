using System;

namespace Dafda.Consuming
{
    internal sealed class TransportLevelMessageExceptionsStrategy : IMalformedMessageStrategy
    {
        public TransportLevelMessage Create(Func<TransportLevelMessage> p)
        {
            try 
            { 
                return p();
            }
            catch(Exception e)
            {
                throw new TransportLevelMessageException(e);
            }
        }
    }
}
