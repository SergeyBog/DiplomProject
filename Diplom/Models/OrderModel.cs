using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public ClientModel Client { get; set; }
        public CarModel Car { get; set; }
        public MechanicModel Mechanic { get; set; }
        public DescriptionModel Description { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }
}
