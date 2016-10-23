using System.Collections.Generic;
using Client.localhost;

namespace Client.ServiceAgents
{
    public interface IStockService
    {
        GetAllResponseMessage GetAll(int page, int size);
        GetByIdsResponseMessage GetByRemoteIds(List<int> remoteIds, int page, int size);
        GetByIdResponseMessage GetByRemoteId(int remoteId);
    }
}
