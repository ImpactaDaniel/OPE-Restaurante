using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using Restaurante.Application.Common.Integration;
using Restaurante.Domain.Integrations.EventBus.Models;
using Restaurante.Domain.Integrations.Interface;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Integrations.Implementations
{
    public class AzureServiceBus : IEventBus
    {
        private readonly ServiceBusClient _client;
        public AzureServiceBus(IntegrationSettings integrationSettings)
        {
            _client = new ServiceBusClient(integrationSettings.EventBusConnectionString);
        }
        public async Task PublishAsync(string queueName, IntegrationEvent @event, CancellationToken cancellationToken = default)
        {
            var sender = _client.CreateSender(queueName);
            await sender.SendMessageAsync(new ServiceBusMessage
            {
                MessageId = @event.Id.ToString(),
                Body = GetBinaryData(@event)
            }, cancellationToken);
        }

        private static BinaryData GetBinaryData(object payload)
        {
            var base64string = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(JsonConvert.SerializeObject(payload, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            })));

            var bytes = Convert.FromBase64String(base64string);
            return new BinaryData(bytes);
        }
    }
}
