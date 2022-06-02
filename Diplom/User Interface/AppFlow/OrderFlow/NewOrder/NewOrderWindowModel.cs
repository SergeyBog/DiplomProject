using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.Flows.AppFlow.OrderFlow.NewOrder
{
    public class NewOrderWindowModel
    {
        public OrderModel OrderModel = new OrderModel();
        public ClientModel ClientModel = new ClientModel();
        public CarModel CarModel = new CarModel();
        public MechanicModel MechanicModel = new MechanicModel();
        public DescriptionModel DescriptionModel = new DescriptionModel();
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
            return _worker.GetClientCars(ClientModel);
        }

        public void RemoveServiceFromOrder(int index)
        {
            ServicesOrderList.RemoveAt(index);
        }

        public void RemoveSpareFromOrder(int index)
        {
            SparesOrderList.RemoveAt(index);
        }

        public void AddOrderToDataBase()
        {
            CreateDescription();
            GetDescriptionId();
            AddServicesAndSparesToDb();
            _worker.AddOrderToDb(OrderModel);
        }

        public void GetDescriptionId()
        {
            OrderModel.Description = _worker.GetOrderDescription(DescriptionModel.Description);
            OrderModel.Client = ClientModel;
            OrderModel.Car = CarModel;
            OrderModel.Mechanic = MechanicModel;
        }

        public void AddServicesAndSparesToDb()
        {
            _worker.AddSparesToOrder(SparesOrderList, _worker.GetOrderDescription(DescriptionModel.Description).Id);
            _worker.AddServicesToOrder(ServicesOrderList, _worker.GetOrderDescription(DescriptionModel.Description).Id);
        }

        public void GetMechanic(string name, string secondName)
        {
            MechanicModel = _worker.GetMechanicForName(name, secondName);
        }

        public void CreateDescription()
        {
            DescriptionModel.CarId = CarModel.Id;
            DescriptionModel.MechanicId = MechanicModel.Id;
           _worker.AddDescriptionToDataBase(DescriptionModel);
        }

        public int CountTotalPrice()
        {
            return ServicesOrderList.Sum(service => service.Cost) + SparesOrderList.Sum(selector:spare => spare.Cost);
        }
    }
}
