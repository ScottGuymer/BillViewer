namespace BillViewer.Core.Queries.Handlers
{
    using System.Threading.Tasks;

    using BillViewer.Core.Models.Bill;

    using Flurl.Http;

    public class BillQueryHandler : IQueryHandlerAsync<BillQuery, Bill>
    {
        public async Task<Bill> Execute(BillQuery query)
        {
            // Here we would probably do something around the account id to get a different bill
            // clearly the endpoint here would be insome sort of settings somewhere.
            try
            {
                return await "http://safe-plains-5453.herokuapp.com/bill.json".GetJsonAsync<Bill>();
            }
            catch (System.Exception)
            {
                // todo would probably want to handle this here!
                // but we will just throw it here and make the app crumble!
                throw;
            }
        }
    }
}