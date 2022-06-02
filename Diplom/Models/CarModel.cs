using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class CarModel
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string LastTo { get; set; }
        public string FullName => $"{Mark} {Model}";
    }
}
