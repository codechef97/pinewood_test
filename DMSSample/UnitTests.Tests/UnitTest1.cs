using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Pinewood.DMSSample.Business.Controllers.PartInvoiceController;
using Pinewood.DMSSample.Business.Dao;
using Pinewood.DMSSample.Business.Infra.AppService;
using Pinewood.DMSSample.Business.Database.CustomerRepositoryDB;
using Pinewood.DMSSample.Business.Database.PartInvoiceRepositoryDB;
using Pinewood.DMSSample.Business.Services.PartAvailabilityClient;
namespace UnitTests.Tests
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        public void _custNotFound()
        {

            // Customer ID less than or equal to 0

            var test = new Mock<IAppIoC>();
            
            var customerRepository = new Mock<ICustomerRepositoryDB>();
            var partInvoiceRepository = new Mock<IPartInvoiceRepositoryDB>();
            var partAvailabilityService = new Mock<IPartAvailabilityClient>();

            Customer customer = null;
            customerRepository.Setup(r => r.GetByName(It.IsAny<string>())).Returns(customer);
            
            test.Setup(c => c.Resolve<ICustomerRepositoryDB>()).Returns(customerRepository.Object);

            test.Setup(c => c.Resolve<IPartInvoiceRepositoryDB>()).Returns(partInvoiceRepository.Object);

            test.Setup(c => c.Resolve<IPartAvailabilityClient>()).Returns(partAvailabilityService.Object);

            var partInvoiceController = new PartInvoiceController(test.Object);
            
            var customerName = "Hrushikesh";

            var quantity = 5;
            
            var stockCode = "XX";
            
            var result = partInvoiceController.CreatePartInvoice(stockCode, quantity, customerName);

            Assert.IsTrue(result.Success == false);
        }

        [TestMethod]
        public void _createInvoiceSuccessCase()
        {

            // Success Scenario 

            var test = new Mock<IAppIoC>();

            var customerRepository = new Mock<ICustomerRepositoryDB>();
            var partInvoiceRepository = new Mock<IPartInvoiceRepositoryDB>();
            var partAvailabilityService = new Mock<IPartAvailabilityClient>();

            Customer _Customer = new Customer( id: 99,  name: "Hk",  address: "Liverpool");

            customerRepository.Setup(r => r.GetByName(It.IsAny<string>())).Returns(_Customer);
            test.Setup(c => c.Resolve<ICustomerRepositoryDB>()).Returns(customerRepository.Object);

            partInvoiceRepository.Setup(r => r.Add(It.IsAny<PartInvoice>()));
            test.Setup(c => c.Resolve<IPartInvoiceRepositoryDB>()).Returns(partInvoiceRepository.Object);

            var availability = 1;
            partAvailabilityService.Setup(s => s.GetAvailability(It.IsAny<string>())).Returns(availability);
            test.Setup(c => c.Resolve<IPartAvailabilityClient>()).Returns(partAvailabilityService.Object);

            var partInvoiceController = new PartInvoiceController(test.Object);
            var stockCode = "XX";
            var quantity = 5;
            var customerName = "Hrushikesh";

            var result = partInvoiceController.CreatePartInvoice(stockCode, quantity, customerName);

            Assert.IsTrue(result.Success == true);
        }
        
        [TestMethod]
        public void _checkEmptyStockCode()
        {

            // No Stock Code
            var test = new Mock<IAppIoC>();

            var customerRepository = new Mock<ICustomerRepositoryDB>();
            
            var partInvoiceRepository = new Mock<IPartInvoiceRepositoryDB>();

            var partAvailabilityService = new Mock<IPartAvailabilityClient>();

            test.Setup(c => c.Resolve<ICustomerRepositoryDB>()).Returns(customerRepository.Object);

            test.Setup(c => c.Resolve<IPartInvoiceRepositoryDB>()).Returns(partInvoiceRepository.Object);

            test.Setup(c => c.Resolve<IPartAvailabilityClient>()).Returns(partAvailabilityService.Object);

            var partInvoiceController = new PartInvoiceController(test.Object);

            var stockCode = string.Empty;
            var quantity = 10;
            var customerName = "PineWood";
            
            var result = partInvoiceController.CreatePartInvoice(stockCode, quantity, customerName);

            Assert.IsTrue(result.Success == false);
        }

        [TestMethod]
        public void _checkQuantity()
        {

        }

        
    }
}
