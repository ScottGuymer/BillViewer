namespace BillViewer.Core.Queries.Handlers
{
	using System;
	using System.Threading.Tasks;

    public class BroadbandQueryHandler : IQueryHandlerAsync<BillQuery, BroadbandQueryHandler>
    {
        Task<BroadbandQueryHandler> IQueryHandlerAsync<BillQuery, BroadbandQueryHandler>.Execute(BillQuery query)
        {
            throw new NotImplementedException();
        }
    }
}