using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.User_Interface.AppFlow.OrderFlow.Order_description
{
    public class OrderDescriptionWindowModel
    {
        public OrderModel OrderModel = new OrderModel();
        private readonly DbWorker _worker = new DbWorker();
        public List<SpareModel> SparesOrderList = new List<SpareModel>();
        public List<ServiceModel> ServicesOrderList = new List<ServiceModel>();

        public List<ClientModel> GetClients()
        {
            return _worker.GetClients();
        }

        public List<SpareModel> GetSpares()
        {
            return _worker.GetSpares();
        }

        public List<ServiceModel> GetServices()
        {
            return _worker.GetServices();
        }

        public List<CarModel> GetClientCars()
        {
            return _worker.GetClientCars(OrderModel.Client);
        }

        public List<SpareModel> GetSparesInOrder()
        {
            SparesOrderList = _worker.GetSpareDoneForOrder(OrderModel.Description.Id);
            return _worker.GetSpareDoneForOrder(OrderModel.Description.Id);
        }

        public List<ServiceModel> GetServicesInOrder()
        {
            ServicesOrderList = _worker.GetServiceDoneForOrder(OrderModel.Description.Id);
            return _worker.GetServiceDoneForOrder(OrderModel.Description.Id);
        }

        public void RemoveServiceFromOrder(int index)
        {
            ServicesOrderList.RemoveAt(index);
        }

        public void RemoveSpareFromOrder(int index)
        {
            SparesOrderList.RemoveAt(index);
        }

        public void EditOrder()
        {
           // GetDescriptionId();
            EditDescription();
            AddServicesAndSparesToDb();
            _worker.EditOrder(OrderModel);
        }

        private void AddServicesAndSparesToDb()
        {
            _worker.DeleteSparesFromOrder(OrderModel.Description.Id);
            _worker.DeleteServicesFromOrder(OrderModel.Description.Id);
            _worker.AddSparesToOrder(SparesOrderList, OrderModel.Description.Id);
            _worker.AddServicesToOrder(ServicesOrderList, OrderModel.Description.Id);
        }

        public void GetMechanic(string name, string secondName)
        {
            OrderModel.Mechanic = _worker.GetMechanicForName(name, secondName);
        }

        private void EditDescription()
        {
            OrderModel.Description.CarId = OrderModel.Car.Id;
            OrderModel.Description.MechanicId = OrderModel.Mechanic.Id;
            _worker.EditDescription(OrderModel.Description);
        }

        public int CountTotalPrice()
        {
            return ServicesOrderList.Sum(service => service.Cost) + SparesOrderList.Sum(selector: spare => spare.Cost);
        }
    }
}
