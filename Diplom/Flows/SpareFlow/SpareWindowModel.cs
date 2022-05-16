using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.SpareFlow
{
    public class SpareWindowModel
    {
        public SpareModel SpareModel = new SpareModel();
        private readonly SpareDbWorker _worker = new SpareDbWorker();

        public List<SpareModel> GetSpares()
        {
            return _worker.GetSpares();
        }

        public void AddSpareToDb(SpareModel spareModel)
        {
            _worker.AddSpareToDb(spareModel);
        }

        public void EditSpare()
        {
            _worker.EditSpare(SpareModel);
        }

        public void DeleteSpare()
        {
            _worker.DeleteSpareFromDb(SpareModel);
        }

        public void GetDataForModel(string naming, int cost)
        {
            SpareModel.Naming = naming;
            SpareModel.Cost = cost;
        }
    }
}
