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

        public async Task<GetByIdsResponseMessage> GetByIdsAsync(List<int> ids, int page, int size)
        {
            var idArray = new ArrayOfInt();
            idArray.AddRange(ids);
            var response = await _client.GetByIdsAsync(idArray, page, size);
            return response.Body.GetByIdsResult;
        }

        public async Task<GetByIdResponseMessage> GetByIdAsync(int id)
        {
            var response = await _client.GetByIdAsync(id);
            return response.Body.GetByIdResult;
        }
    }
}