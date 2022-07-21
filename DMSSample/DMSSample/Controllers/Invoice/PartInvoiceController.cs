using Pinewood.DMSSample.Business.Dao;
using Pinewood.DMSSample.Business.Infra.AppService;
using Pinewood.DMSSample.Business.Models.PartInvoiceResult;
using Pinewood.DMSSample.Business.Database.Customer;
using Pinewood.DMSSample.Business.Database.PartInvoice;
using System;
using Pinewood.DMSSample.Business.Services.PartAvailabilityClient;

namespace Pinewood.DMSSample.Business.Controllers.Invoice
{
    public interface IPartInvoiceController
    {
        CreatePartInvoiceResult CreatePartInvoiceAsync(string stockCode, int quantity, string customerName);
    }
    public class PartInvoiceController : IPartInvoiceController
    {
        private IPartAvailabilityClient __IPartAvailabilityClient;

        private ICustomerRepositoryDB __CustomerRepositoryDB;

        private IPartInvoiceRepositoryDB __PartInvoiceRepositoryDB;

        public PartInvoiceController(IAppIoC ioc)
        {
            __IPartAvailabilityClient = ioc.Resolve<IPartAvailabilityClient>();
            __CustomerRepositoryDB = ioc.Resolve<ICustomerRepositoryDB>();
            __PartInvoiceRepositoryDB = ioc.Resolve<IPartInvoiceRepositoryDB>();
        }

        public async Task<CreatePartInvoiceResult> CreatePartInvoiceAsync(string stockCode, int quantity, string customerName)
        {
            // for better exception handling, we can add try catch block.
            if (string.IsNullOrEmpty(stockCode))
            {
                return new CreatePartInvoiceResult(false);
            }

            if (quantity <= 0)
            {
                return new CreatePartInvoiceResult(false);
            }

            Customer? _Customer = __CustomerRepositoryDB.GetByName(customerName);
            int _CustomerID = _Customer?.ID ?? 0;
            if (_CustomerID <= 0 )
            {
                return new CreatePartInvoiceResult(false);
            }
            
            int _Availability = await __IPartAvailabilityClient.GetAvailability(stockCode);
                if (_Availability <= 0)
                {
                    return new CreatePartInvoiceResult(false);
                }
            

            PartInvoice _PartInvoice = new PartInvoice(
                stockCode: stockCode,
                quantity: quantity,
                customerID: _CustomerID
            );


            __PartInvoiceRepositoryDB.Add(_PartInvoice);

            return new CreatePartInvoiceResult(true);
        }
    }
}
