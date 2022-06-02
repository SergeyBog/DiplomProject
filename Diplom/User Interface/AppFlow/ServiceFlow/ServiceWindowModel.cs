using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.CarFlow;
using Diplom.Models;

namespace Diplom.ServiceFlow
{
    public class ServiceWindowModel
    {
        public ServiceModel ServiceModel = new ServiceModel();
        private readonly DbWorker _worker = new DbWorker();

        public List<ServiceModel> GetServices()
        {
            return _worker.GetServices();
        }

        public void AddServiceToDb(ServiceModel serviceModel)
        {
            _worker.AddServiceToDb(serviceModel);
        }

        public void EditService()
        {
            _worker.EditService(ServiceModel);
        }

        public void DeleteService()
        {
            _worker.DeleteServiceFromDb(ServiceModel);
        }

        public void GetDataForModel(string naming, int cost)
        {
            ServiceModel.Naming = naming;
            ServiceModel.Cost = cost;
        }

    }
}
