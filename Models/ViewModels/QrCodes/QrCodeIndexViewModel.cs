using Microsoft.AspNetCore.Mvc.Rendering;

namespace QrCodeGeneratorWebAppMVC.Models.ViewModels.QrCodes
{
    public class QrCodeIndexViewModel
    {
        public IEnumerable<QrCode>? PagedQrCodes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);


        public List<QrCode>? QrCodes { get; set; }
        public SelectList? QrCodeTypes { get; set; }
        public string? QrCodeType { get; set; }
        public string? SearchString { get; set; }

    }
}
