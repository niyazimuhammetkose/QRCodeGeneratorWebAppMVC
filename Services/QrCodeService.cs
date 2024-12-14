using QrCodeGeneratorWebAppMVC.Models;
using QRCoder;
using static QRCoder.PayloadGenerator;
using static QRCoder.PayloadGenerator.ContactData;
using System.Drawing;

namespace QrCodeGeneratorWebAppMVC.Services
{

    public class QrCodeService
    {
        public string RelativePath { get; set; } = Directory.GetCurrentDirectory();
        public string FilePath { get; set; } = Directory.GetCurrentDirectory();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public QrCodeService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        protected void SetFilePath(string fileName)
        {
            this.RelativePath = Path.Combine("Resources", fileName).Replace("\\", "/");
            this.FilePath = Path.Combine(_webHostEnvironment.WebRootPath, this.RelativePath);
        }

        public void GenerateUrlQrCode(string url, string fileName)
        {
            PngByteQRCode qrCode = this.CreateQRCode(url);
            this.SaveQRCodeToFile(qrCode, fileName);
        }

        public void GenerateVCardQrCode(VCardContactData vCardContactData, string fileName)
        {
            //var addressOrder = PayloadGenerator.ContactData.AddressOrder;

            ContactData contactData = new ContactData(
                outputType: ContactOutputType.VCard3,
                firstname: vCardContactData.FirstName,
                lastname: vCardContactData.LastName,
                nickname: vCardContactData.NickName,
                org: vCardContactData.Org,
                orgTitle: vCardContactData.OrgTitle,
                phone: vCardContactData.Phone,
                mobilePhone: vCardContactData.MobilePhone,
                workPhone: vCardContactData.WorkPhone,
                email: vCardContactData.Email,
                birthday: vCardContactData.BirthDay,
                website: vCardContactData.Website,
                street: vCardContactData.Street,
                houseNumber: vCardContactData.HouseNumber,
                city: vCardContactData.City,
                stateRegion: vCardContactData.StateRegion,
                zipCode: vCardContactData.ZipCode,
                country: vCardContactData.Country,
                //addressOrder: addressOrder,
                note: vCardContactData.Note
                );

            string payload = contactData.ToString();

            PngByteQRCode qrCode = this.CreateQRCode(payload);
            this.SaveQRCodeToFile(qrCode, fileName, 40);
        }

        private PngByteQRCode CreateQRCode(string plainText)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(plainText, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);

            return qrCode;
        }

        private void SaveQRCodeToFile(PngByteQRCode qrCode, string fileName, int fileSize = 20)
        {
            SetFilePath(fileName);

            byte[] qrCodeAsPngByteArr = qrCode.GetGraphic(fileSize);

            File.WriteAllBytes(this.FilePath, qrCodeAsPngByteArr);
        }

    }
}
