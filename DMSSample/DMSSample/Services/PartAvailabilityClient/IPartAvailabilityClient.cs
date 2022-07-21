
using System.Threading.Tasks;
namespace Pinewood.DMSSample.Business.Services.PartAvailabilityClient
{
    public interface IPartAvailabilityClient
    {
        Task<int> GetAvailability(string StockCode);
    }
}
