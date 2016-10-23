using System.Collections.Generic;
using System.Threading.Tasks;
using Client.ServiceReference;

namespace Client.ServiceAgents
{
    public class StockService : IStockService
    {
        private readonly StockExchangeServiceSoapClient _client;

        public StockService()
        {
            _client = new StockExchangeServiceSoapClient();
        }
        public async Task<GetAllResponseMessage> GetAllAsync(int page, int size)
        {
            var response = await _client.GetAllAsync(page, size);
            return response.Body.GetAllResult;
        }

        public async Task<GetByIdsResponseMessage> GetByRemoteIdsAsync(List<int> remoteIds, int page, int size)
        {
            var idArray = new ArrayOfInt();
            idArray.AddRange(remoteIds);
            var response = await _client.GetByIdsAsync(idArray, page, size);
            return response.Body.GetByIdsResult;
        }

        public async Task<GetByIdResponseMessage> GetByRemoteIdAsync(int remoteId)
        {
            var response = await _client.GetByIdAsync(remoteId);
            return response.Body.GetByIdResult;
        }
    }
}