using Pinewood.DMSSample.Business.Controllers.Invoice;
using Pinewood.DMSSample.Business.Infra.AppService;
using System.Threading.Tasks;
namespace Pinewood.DMSSample.Business
{
    public class DMSClient
    {
        private IPartInvoiceController __Controller;

        public DMSClient()
        {
            var _AppIoc = new AppIoC();
            _AppIoc.EnterService
            __Controller = new PartInvoiceController(_AppIoc);
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            return await __Controller.CreatePartInvoiceAsync(stockCode, quantity, customerName);
        }
    }
}