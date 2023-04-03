namespace LibraryWeb.Contracts.Data.Entities
{
    public class Book : BaseEntity
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateOnly? DateIssued { get; set; }
        public DateOnly? DateDue { get; set; }
    }
}
