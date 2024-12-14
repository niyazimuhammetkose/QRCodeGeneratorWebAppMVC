namespace QrCodeGeneratorWebAppMVC.Models.ViewModels.QrCodes
{
    public class QrCodePaginationViewModel
    {
        public IEnumerable<QrCode>? QrCodes { get; set; } // QR kodlar
        public int CurrentPage { get; set; } // Şu anki sayfa
        public int TotalItems { get; set; } // Toplam kayıt
        public int PageSize { get; set; } // Sayfa başına öğe sayısı
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize); // Toplam sayfa
    }
}
