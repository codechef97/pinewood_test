namespace Pinewood.DMSSample.Business.Models.PartInvoiceResult
{
    public class CreatePartInvoiceResult
    {
        public CreatePartInvoiceResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}