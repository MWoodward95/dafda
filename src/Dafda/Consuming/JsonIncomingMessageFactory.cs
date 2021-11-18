using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dafda.Consuming
{
    internal class JsonIncomingMessageFactory : IIncomingMessageFactory
    {
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
        };

        private readonly IMalformedMessageStrategy _malformedMessageStrategy = new ThrowDefaultErrorsStrategy();

        public JsonIncomingMessageFactory(IMalformedMessageStrategy malformedMessageStrategy)
        {
            this._malformedMessageStrategy = malformedMessageStrategy;
        }

        public TransportLevelMessage Create(string rawMessage)
        {
            return _malformedMessageStrategy.Create(() => InternalCreate(rawMessage));
        }

        private TransportLevelMessage InternalCreate(string rawMessage)
        {
            var jsonDocument = JsonDocument.Parse(rawMessage);

            var dataProperty = jsonDocument.RootElement.GetProperty(MessageEnvelopeProperties.Data);
            var jsonData = dataProperty.GetRawText();

            var metadataProperties = jsonDocument
                .RootElement
                .EnumerateObject()
                .Where(property => property.Name != MessageEnvelopeProperties.Data)
                .ToDictionary(x => x.Name, x => x.Value.ToString());

            return new TransportLevelMessage(new Metadata(metadataProperties), type => JsonSerializer.Deserialize(jsonData, type, JsonSerializerOptions));
        }
    }
}
