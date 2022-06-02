using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class MechanicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string PhoneNumber { get; set; }
        public string WorkExperience { get; set; }
        public LoginInfoModel LoginInfo { get; set; }
        public string FullName => $"{Name} {SecondName}";
    }
}
