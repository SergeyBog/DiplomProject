using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;
using Microsoft.Win32;

namespace Diplom.ServiceFlow
{
    public class ServiceDbWorker
    {
        public void AddServiceToDb(ServiceModel serviceModel)
        {
            using (var db = new STOEntities())
            {
                var service = new Service
                {
                    Naming = serviceModel.Naming,
                    Cost = serviceModel.Cost
                };
                db.Service.Add(service);
                db.SaveChanges();
            }
        }

        public void EditService(ServiceModel serviceModel)
        {
            using (var db = new STOEntities())
            {
                var service = db.Service.FirstOrDefault(x => x.ID == serviceModel.Id);
                if (service == null)
                {
                    return;
                }
                service.Naming = serviceModel.Naming;
                service.Cost = serviceModel.Cost;
                db.SaveChanges();
            }
        }

        public void DeleteServiceFromDb(ServiceModel serviceModel)
        {
            using (var db = new STOEntities())
            {
                var service = db.Service.FirstOrDefault(x => x.ID == serviceModel.Id);
                if (service == null)
                {
                    return;
                }
                db.Service.Remove(service);
                db.SaveChanges();
            }
        }

        public List<ServiceModel> GetServices()
        {
            var result = new List<ServiceModel>();
            using (var db = new STOEntities())
            {
                var services = db.Service;
                foreach (var service in services)
                {
                    var serviceModel = new ServiceModel()
                    {
                        Id = service.ID,
                        Naming = service.Naming,
                        Cost = service.Cost
                    };
                    result.Add(serviceModel);
                }
            }
            return result;
        }
    }
}
