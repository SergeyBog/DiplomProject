using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.MechanicFlow
{
    public class MechanicWindowModel
    {
        public MechanicModel MechanicModel = new MechanicModel();
        private readonly DbWorker _worker = new DbWorker();

        public List<MechanicModel> GetMechanics()
        {
            var result = _worker.GetMechanics();
            return result;
        }

        public void AddMechanicToDb(MechanicModel mechanicModel)
        {
            _worker.AddMechanicToDb(mechanicModel);
        }

        public void DeleteMechanicFromDb()
        {
            _worker.DeleteMechanicFromDb(MechanicModel);
        }

        public void EditMechanic()
        {
            _worker.EditMechanic(MechanicModel);
        }

    }
}
