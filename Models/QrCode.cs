using System.ComponentModel.DataAnnotations;

namespace QrCodeGeneratorWebAppMVC.Models
{
    public class QrCode
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty; // "url" veya "vCard"
        public string? ImagePath { get; set; }
        public string? Url { get; set; }
        public string? VCardFirstName { get; set; }
        public string? VCardLastName { get; set; }
        public string? VCardNickName { get; set; }
        public string? VCardOrg { get; set; }
        public string? VCardOrgTitle { get; set; }
        public string? VCardPhone { get; set; }
        public string? VCardMobilePhone { get; set; }
        public string? VCardWorkPhone { get; set; }
        public string? VCardEmail { get; set; }
        public DateTime? VCardBirthDay { get; set; }
        public string? VCardWebsite { get; set; }
        public string? VCardStreet { get; set; }
        public string? VCardHouseNumber { get; set; }
        public string? VCardCity { get; set; }
        public string? VCardStateRegion { get; set; }
        public string? VCardZipCode { get; set; }
        public string? VCardCountry { get; set; }
        public string? VCardNote { get; set; }
    }
}
