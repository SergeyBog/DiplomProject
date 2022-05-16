using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.ClientFlow
{
    public class ClientWindowModel
    {
        public ClientModel ClientModel = new ClientModel();
        private readonly ClientDbWorker _worker = new ClientDbWorker();

        public void GetDataForModel(string name,string secondName,string phone)
        {
            ClientModel.Name = name;
            ClientModel.SecondName = secondName;
            ClientModel.PhoneNumber = phone;
        }
        public List<ClientModel> GetClients()
        {
            return _worker.GetClients();
        }

        public void AddClientToDb(ClientModel clientModel)
        {
            _worker.AddClientToDb(clientModel);
        }

        public void EditClient()
        {
            _worker.EditClient(ClientModel);
        }

        public void DeleteClient()
        {
            _worker.DeleteClientFromDb(ClientModel);
        }

        public List<CarModel> GetClientCars()
        {
            return _worker.GetClientCars(ClientModel);
        }

        public List<CarModel> GetAllAvailableCars()
        {
            return _worker.GetAllAvailableCars(ClientModel);
        }

        public void RemoveCarFromClient(CarModel carModel)
        {
            _worker.RemoveCarFromClient(ClientModel,carModel);
        }

        public void AddCarToClient(CarModel carModel)
        {
            _worker.AddCarToClient(ClientModel,carModel);
        }
    }
}
