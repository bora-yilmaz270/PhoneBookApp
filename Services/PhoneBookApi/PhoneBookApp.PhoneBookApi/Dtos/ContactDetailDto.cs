namespace PhoneBookApp.PhoneBookApi.Dtos
{
    public class ContactDetailDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public  List<ContactInfoDto> ContactInfos { get; set; }
    }
}
