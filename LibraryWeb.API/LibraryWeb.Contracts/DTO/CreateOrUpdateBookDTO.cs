using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Contracts.DTO
{
    internal class CreateOrUpdateBookDTO
    {
        public string ISBN { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateOnly DateIssued { get; set; }
        public DateOnly DateDue { get; set; }
    }
}
