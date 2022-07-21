
using System.Threading.Tasks;
namespace Pinewood.DMSSample.Business.Services.PartAvailabilityClient
{

    public class PartAvailabilityClient : IDisposable, IPartAvailabilityClient
    {
        private HttpClient _httpClient;

        public PartAvailabilityClient()
        {
            _httpClient = new HttpClient();
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> GetAvailability(string stockCode)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://www.api.pinewood.com/parts/availability/{stockCode}");

            if (responseMessage.IsSuccessStatusCode)
            {
                string responseString = await responseMessage.Content.ReadAsStringAsync();
                return int.Parse(responseString);
            }
            else
            {
                throw new Exception($"Could not get part availability for {stockCode}");
            }
        }
    }
}
