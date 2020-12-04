using Order.Service.Proxies.Catalog.Commands;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog
{
	public interface ICatalogQueueProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
    }
}
