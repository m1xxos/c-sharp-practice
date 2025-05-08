using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI_6_01.Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public List<Phone> Phones { get; set; } = new List<Phone>();

        public void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }

        public void RemoveAt(int index)
        {
            Phones.RemoveAt(index);
        }

        public int PhoneCount { get { return Phones.Count; } }
    }
}
