namespace QrCodeGeneratorWebAppMVC.Models
{
    public class VCardContactData
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? NickName { get; set; }
        public string? Org { get; set; }
        public string? OrgTitle { get; set; }
        public string? Phone { get; set; }
        public string? MobilePhone { get; set; }
        public string? WorkPhone { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Website { get; set; }
        public string? Street { get; set; }
        public string? HouseNumber { get; set; }
        public string? City { get; set; }
        public string? StateRegion { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? Note { get; set; }

        //public VCardContactData(string FirstName, string LastName, string NickName, string Org, string OrgTitle, string Phone, string MobilePhone, string WorkPhone, string Email, DateTime BirthDay, string Website, string Street, string HouseNumber, string City, string StateRegion, string ZipCode, string Country, string Note)
        //{
        //    this.FirstName = FirstName;
        //    this.LastName = LastName;
        //    this.NickName = NickName;
        //    this.Org = Org;
        //    this.OrgTitle = OrgTitle;
        //    this.Phone = Phone;
        //    this.MobilePhone = MobilePhone;
        //    this.WorkPhone = WorkPhone;
        //    this.Email = Email;
        //    this.BirthDay = BirthDay;
        //    this.Website = Website;
        //    this.Street = Street;
        //    this.HouseNumber = HouseNumber;
        //    this.City = City;
        //    this.StateRegion = StateRegion;
        //    this.ZipCode = ZipCode;
        //    this.Country = Country;
        //    this.Note = Note;
        //}
    }
}
