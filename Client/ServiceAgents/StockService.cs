using System.Collections.Generic;
using Client.localhost;

namespace Client.ServiceAgents
{
    public class StockService : IStockService
    {
        private readonly StockExchangeService _client;

        public StockService()
        {
            _client = new StockExchangeService();
            var authentication = new Authentication() {Password = "testpagdfgssword", User = "testuser"};
            _client.AuthenticationValue = authentication;
        }
        public GetAllResponseMessage GetAll(int page, int size)
        {
            var response = _client.GetAll(page, size);
            return response;
        }

        public GetByIdsResponseMessage GetByRemoteIds(List<int> remoteIds, int page, int size)
        {
            var response = _client.GetByIds(remoteIds.ToArray(), page, size);
            return response;
        }

        public GetByIdResponseMessage GetByRemoteId(int remoteId)
        {
            var response = _client.GetById(remoteId);
            return response;
        }
    }
}