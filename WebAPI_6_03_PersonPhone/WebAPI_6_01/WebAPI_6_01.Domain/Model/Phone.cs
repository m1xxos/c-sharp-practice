using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_6_01.Domain
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string PhoneType { get; set; } = String.Empty;
        public string PhoneNumber { get; set; } = String.Empty;

        public Guid PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}
