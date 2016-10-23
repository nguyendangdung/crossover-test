using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ServiceReference;

namespace Client.ServiceAgents
{
    public interface IStockService
    {
        Task<GetAllResponseMessage> GetAllAsync(int page, int size);
        Task<GetByIdsResponseMessage> GetByRemoteIdsAsync(List<int> remoteIds, int page, int size);
        Task<GetByIdResponseMessage> GetByRemoteIdAsync(int remoteId);
    }
}
