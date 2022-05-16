using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public class DescriptionModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int MechanicId { get; set; }
        public int CarId { get; set; }
        public List<SpareModel> SpareList { get; set; }
        public List<ServiceModel> ServiceList { get; set; }
    }
}
