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
        public int ClientId { get; set; }
        public int CarId { get; set; }
        public int MechanicId { get; set; }
        public int DescriptionId { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
    }
}
