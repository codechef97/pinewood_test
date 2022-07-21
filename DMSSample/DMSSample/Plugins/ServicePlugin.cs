using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pinewood.DMSSample.Business.Infra.AppService;
using Pinewood.DMSSample.Business.Database.Customer;
using Pinewood.DMSSample.Business.Database.PartInvoice;
using Pinewood.DMSSample.Business.Services.PartAvailabilityClient;

namespace Pinewood.DMSSample.Business.Plugins
{
    public static void RegisterApplicationServices(this IAppIoC ioc)
    {
        ioc.Register<ICustomerRepositoryDB>()
            .Register<IPartInvoiceRepositoryDB>()
            .Register<IPartAvailabilityClient>();
    }
}
