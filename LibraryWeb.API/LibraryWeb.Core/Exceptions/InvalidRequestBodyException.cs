using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Core.Exceptions
{
    public class InvalidRequestBodyException : Exception
    {
        public string[] Errors { get; set; }
    }
}
