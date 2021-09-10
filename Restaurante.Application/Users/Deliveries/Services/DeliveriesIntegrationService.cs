using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Deliveries.Services
{
    public class DeliveriesIntegrationService : IEntregadorIntegrationService
    {
        private readonly INotifier _notifier;
        private readonly ILogger<DeliveriesIntegrationService> _logger;
        public DeliveriesIntegrationService(IntegrationConfiguration integrationConfiguration,
                                            INotifier notifier,
                                            ILogger<DeliveriesIntegrationService> logger)
        {
            IntegrationConfiguration = integrationConfiguration;
            _notifier = notifier;
            _logger = logger;
        }

        public IntegrationConfiguration IntegrationConfiguration { get; }

        public async Task<IntegrationResponse> CreateNewEntregador(DeliveryPersonIntegration entregador, CancellationToken cancellationToken = default)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(IntegrationConfiguration.Url);

            IntegrationResponse integrationResponse = null;

            var jsonRequest = JsonConvert.SerializeObject(entregador);

            var bytesRequest = Encoding.UTF8.GetBytes(jsonRequest);
            try
            {
                using (var requestStream = await webRequest.GetRequestStreamAsync())
                {
                    await requestStream.WriteAsync(bytesRequest, cancellationToken);
                    requestStream.Close();
                }
                using (var response = (HttpWebResponse)await webRequest.GetResponseAsync())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        return null;
                    using (var responseStream = new StreamReader(response.GetResponseStream()))
                    {
                        var responseString = responseStream.ReadToEnd();
                        integrationResponse = JsonConvert.DeserializeObject<IntegrationResponse>(responseString);
                        responseStream.Close();
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return null;
            }
            return integrationResponse;
        }

        public Task<IList<DeliveryPersonIntegration>> GetAvailables(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
