namespace BillViewer.Core
{
    using System.Threading.Tasks;

    public interface IQueryHandlerAsync<in TQuery, TResult>
    {
        Task<TResult> Execute(TQuery query);
    }
}