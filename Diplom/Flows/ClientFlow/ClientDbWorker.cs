using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.ClientFlow
{
    public class ClientDbWorker
    {
        public void AddClientToDb(ClientModel clientModel)
        {
            using (var db = new STOEntities())
            {
                var client = new Client
                {
                    Name = clientModel.Name,
                    SecondName = clientModel.SecondName,
                    PhoneNumber = clientModel.PhoneNumber
                };
                db.Client.Add(client);
                db.SaveChanges();
            }
        }

        public void EditClient(ClientModel clientModel)
        {
            using (var db = new STOEntities())
            {
                var client = db.Client.FirstOrDefault(x => x.ID == clientModel.Id);
                if (client == null)
                {
                    return;
                }

                client.Name = clientModel.Name;
                client.SecondName = clientModel.SecondName;
                client.PhoneNumber = clientModel.PhoneNumber;
                db.SaveChanges();
            }
        }

        public void DeleteClientFromDb(ClientModel clientModel)
        {
            using (var db = new STOEntities())
            {
                var client = db.Client.FirstOrDefault(x => x.ID == clientModel.Id);
                if (client == null)
                {
                    return;
                }
                db.Client.Remove(client);
                db.SaveChanges();

            }
        }

        public List<ClientModel> GetClients()
        {
            var result = new List<ClientModel>();
            using (var db = new STOEntities())
            {
                var clients = db.Client;
                foreach (var client in clients)
                {
                    var clientModel = new ClientModel
                    {
                        Id = client.ID,
                        Name = client.Name,
                        SecondName = client.SecondName,
                        PhoneNumber = client.PhoneNumber
                    };
                    result.Add(clientModel);
                }
            }

            return result;
        }

        public List<CarModel> GetAllAvailableCars(ClientModel clientModel)
        {
            var result = new List<CarModel>();
            using (var db = new STOEntities())
            {
                var allCars = db.Car;
                var clientCars = db.Client_Cars.Where(x=>x.ClientID == clientModel.Id);
                if (clientCars.Count() != 0)
                {
                    foreach (var clientCar in clientCars)
                    {
                        foreach (var car in allCars)
                        {
                            if (clientCar.CarID == car.ID)
                            {

                            }
                            else
                            {
                                var carModel = new CarModel
                                {
                                    Id = car.ID,
                                    Mark = car.Mark,
                                    Model = car.Model,
                                    Year = car.Year,
                                    LastTo = car.LastTO
                                };
                                result.Add(carModel);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var car in allCars)
                    {
                        var carModel = new CarModel
                        {
                            Id = car.ID,
                            Mark = car.Mark,
                            Model = car.Model,
                            Year = car.Year,
                            LastTo = car.LastTO
                        };
                        result.Add(carModel);
                    }
                }

            }
            return result;
        }

        public List<CarModel> GetClientCars(ClientModel clientModel)
        {
            var result = new List<CarModel>();
            using (var db = new STOEntities())
            {
                var clientCarsList = db.Client_Cars.Where(x => x.ClientID == clientModel.Id);
                foreach (var clientCar in clientCarsList)
                {
                    var cars = db.Car.Where(y => y.ID == clientCar.CarID);
                    foreach (var car in cars)
                    {
                        var carModel = new CarModel
                        {
                            Id = car.ID,
                            Mark = car.Mark,
                            Model = car.Model,
                            Year = car.Year,
                            LastTo = car.LastTO
                        };
                        result.Add(carModel);
                    }
                }
                return result;
            }

        }

        public void RemoveCarFromClient(ClientModel clientModel, CarModel carModel)
        {
            using (var db = new STOEntities())
            {
                var clientCars = db.Client_Cars.FirstOrDefault(x => x.ClientID == clientModel.Id && x.CarID == carModel.Id);
                db.Client_Cars.Remove(clientCars);
                db.SaveChanges();
            }
        }

        public void AddCarToClient(ClientModel clientModel, CarModel carModel)
        {
            using (var db = new STOEntities())
            {
                var clientCars = new Client_Cars();
                {
                    clientCars.CarID = carModel.Id;
                    clientCars.ClientID = clientModel.Id;
                }
                db.Client_Cars.Add(clientCars);
                db.SaveChanges();
            }
        }


    }
}
