using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom
{
    public class DbWorker
    {
        public bool CheckLogin(string login, string password)
        {
            bool result = false;
            using (var db = new STOEntities())
            {
                var loginList = db.Login_Info;
                foreach (var log in loginList)
                {
                    if (log.Login == login && log.Password == password)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public string GetUserType(string login, string password)
        {
            string result = "";
            using (var db = new STOEntities())
            {
                var loginList = db.Login_Info;
                foreach (var log in loginList)
                {
                    if (log.Login == login && log.Password == password)
                    {
                        result = log.UserType;
                    }
                }
            }
            return result;
        }

        public AdminModel GetAdmin(string login, string password)
        {
            AdminModel result = new AdminModel();
            using (var db = new STOEntities())
            {
                var logininfo = db.Login_Info.FirstOrDefault(x => x.Login == login && x.Password == password);
                var admin = db.Admin.FirstOrDefault(x => x.Login_InfoID == logininfo.ID);
                result.Id = admin.ID;
                result.Name = admin.Name;
                result.SecondName = admin.SecondName;
                result.PhoneNumber = admin.PhoneNumber;
                result.LoginInfo = new LoginInfoModel()
                {
                    Id = admin.Login_Info.ID, 
                    Login = admin.Login_Info.Login,
                    Password = admin.Login_Info.Password,
                    UserType = admin.Login_Info.UserType
                };
            }

            return result;
        }

        public MechanicModel GetMechanic(string login, string password)
        {
            MechanicModel result = new MechanicModel();
            using (var db = new STOEntities())
            {
                var logininfo = db.Login_Info.FirstOrDefault(x => x.Login == login && x.Password == password);
                var mechanic = db.Mechanic.FirstOrDefault(x => x.Login_InfoID == logininfo.ID);
                result.Id = mechanic.ID;
                result.Name = mechanic.Name;
                result.SecondName = mechanic.SecondName;
                result.PhoneNumber = mechanic.PhoneNumber;
                result.WorkExperience = mechanic.WorkExperience;
                result.LoginInfo = new LoginInfoModel()
                {
                    Id = mechanic.Login_Info.ID,
                    Login = mechanic.Login_Info.Login,
                    Password = mechanic.Login_Info.Password,
                    UserType = mechanic.Login_Info.UserType
                };
            }

            return result;
        }

        public MechanicModel GetMechanicForName(string name, string secondName)
        {
            MechanicModel result = new MechanicModel();
            using (var db = new STOEntities())
            {
                var mechanic = db.Mechanic.FirstOrDefault(x => x.Name == name && x.SecondName == secondName);
                result.Id = mechanic.ID;
                result.Name = mechanic.Name;
                result.SecondName = mechanic.SecondName;
                result.PhoneNumber = mechanic.PhoneNumber;
                result.WorkExperience = mechanic.WorkExperience;
                result.LoginInfo = new LoginInfoModel()
                {
                    Id = mechanic.Login_Info.ID,
                    Login = mechanic.Login_Info.Login,
                    Password = mechanic.Login_Info.Password,
                    UserType = mechanic.Login_Info.UserType
                };
            }
            return result;
        }

        public void AddCarToDb(CarModel carModel)
        {
            using (var db = new STOEntities())
            {
                var car = new Car
                {
                    Mark = carModel.Mark,
                    Model = carModel.Model,
                    Year = carModel.Year,
                    LastTO = carModel.LastTo
                };
                db.Car.Add(car);
                db.SaveChanges();
            }
        }

        public void EditCar(CarModel carModel)
        {
            using (var db = new STOEntities())
            {
                var car = db.Car.FirstOrDefault(x => x.ID == carModel.Id);
                if (car == null)
                {
                    return;
                }
                car.Mark = carModel.Mark;
                car.Model = carModel.Model;
                car.Year = carModel.Year;
                car.LastTO = car.LastTO;
                db.SaveChanges();
            }
        }

        public void DeleteCarFromDb(CarModel carModel)
        {
            using (var db = new STOEntities())
            {
                var car = db.Car.FirstOrDefault(x => x.ID == carModel.Id);
                if (car == null)
                {
                    return;
                }
                db.Car.Remove(car);
                db.SaveChanges();
            }
        }

        public List<CarModel> GetCars()
        {
            var result = new List<CarModel>();
            using (var db = new STOEntities())
            {
                var cars = db.Car;
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
                var clientCars = db.Client_Cars.Where(x => x.ClientID == clientModel.Id);
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

        public void AddLoginInfoToDb(LoginInfoModel loginInfoModel)
        {
            using (var db = new STOEntities())
            {
                var loginInfo = new Login_Info();
                loginInfo.Login = loginInfo.Login;
                loginInfo.Password = loginInfo.Password;
                loginInfo.UserType = loginInfo.UserType;
                db.Login_Info.Add(loginInfo);
                db.SaveChanges();
            }
        }

        public void AddMechanicToDb(MechanicModel mechanicModel)
        {
            using (var db = new STOEntities())
            {
                var mechanic = new Mechanic
                {
                    Name = mechanicModel.Name,
                    SecondName = mechanicModel.SecondName,
                    PhoneNumber = mechanicModel.PhoneNumber,
                    WorkExperience = mechanicModel.WorkExperience,
                    Login_InfoID = mechanicModel.LoginInfo.Id
                };
                AddLoginInfoToDb(mechanicModel.LoginInfo);
                db.Mechanic.Add(mechanic);
                db.SaveChanges();
            }
        }

        public void AddAdminNow()
        {
            using (var db = new STOEntities())
            {
                var admin = new Admin()
                {
                    Name = "Main",
                    SecondName = "Admin",
                    PhoneNumber = "+380979563344",
                    Login_InfoID = 1
                };
                db.Admin.Add(admin);
                db.SaveChanges();
            }

        }

        public void EditMechanic(MechanicModel mechanicModel)
        {
            using (var db = new STOEntities())
            {
                var mechanic = db.Mechanic.FirstOrDefault(x => x.ID == mechanicModel.Id);
                if (mechanic == null)
                {
                    return;
                }
                var loginInfo = db.Login_Info.FirstOrDefault(x =>
                    x.ID == mechanicModel.LoginInfo.Id);
                if (loginInfo == null)
                {
                    return;
                }
                mechanic.Name = mechanicModel.Name;
                mechanic.SecondName = mechanicModel.SecondName;
                mechanic.PhoneNumber = mechanicModel.PhoneNumber;
                mechanic.WorkExperience = mechanicModel.WorkExperience;
                loginInfo.Login = mechanicModel.LoginInfo.Login;
                loginInfo.Password = mechanicModel.LoginInfo.Password;
                db.SaveChanges();
            }
        }

        public void DeleteMechanicFromDb(MechanicModel mechanicModel)
        {
            using (var db = new STOEntities())
            {
                var mechanic = db.Mechanic.FirstOrDefault(x => x.ID == mechanicModel.Id);
                if (mechanic == null)
                {
                    return;
                }
                var loginInfo = db.Login_Info.FirstOrDefault(x =>
                    x.ID == mechanicModel.LoginInfo.Id);
                if (loginInfo == null)
                {
                    return;
                }
                db.Login_Info.Remove(loginInfo);
                db.Mechanic.Remove(mechanic);
                db.SaveChanges();
            }
        }

        public List<MechanicModel> GetMechanics()
        {
            var result = new List<MechanicModel>();
            using (var db = new STOEntities())
            {
                var mechanics = db.Mechanic;
                foreach (var mechanic in mechanics)
                {
                    var mechanicModel = new MechanicModel
                    {
                        Id = mechanic.ID,
                        Name = mechanic.Name,
                        SecondName = mechanic.SecondName,
                        PhoneNumber = mechanic.PhoneNumber,
                        WorkExperience = mechanic.WorkExperience
                    };
                    var loginInfo = db.Login_Info.FirstOrDefault(x => x.ID == mechanic.Login_InfoID);
                    var loginInfoModel = new LoginInfoModel
                    {
                        Id = loginInfo.ID,
                        Login = loginInfo.Login,
                        Password = loginInfo.Password,
                        UserType = loginInfo.UserType
                    };
                    mechanicModel.LoginInfo = loginInfoModel;
                    result.Add(mechanicModel);
                }
            }
            return result;
        }

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

        public void AddSpareToDb(SpareModel spareModel)
        {
            using (var db = new STOEntities())
            {
                var spare = new Spare()
                {
                    Naming = spareModel.Naming,
                    Cost = spareModel.Cost
                };
                db.Spare.Add(spare);
                db.SaveChanges();
            }
        }

        public void EditSpare(SpareModel spareModel)
        {
            using (var db = new STOEntities())
            {
                var spare = db.Spare.FirstOrDefault(x => x.ID == spareModel.Id);
                if (spare == null)
                {
                    return;
                }
                spare.Naming = spareModel.Naming;
                spare.Cost = spareModel.Cost;
                db.SaveChanges();
            }
        }

        public void DeleteSpareFromDb(SpareModel spareModel)
        {
            using (var db = new STOEntities())
            {
                var spare = db.Spare.FirstOrDefault(x => x.ID == spareModel.Id);
                if (spare == null)
                {
                    return;
                }
                db.Spare.Remove(spare);
                db.SaveChanges();
            }
        }

        public List<SpareModel> GetSpares()
        {
            var result = new List<SpareModel>();
            using (var db = new STOEntities())
            {
                var spares = db.Spare;
                foreach (var spare in spares)
                {
                    var spareModel = new SpareModel()
                    {
                        Id = spare.ID,
                        Naming = spare.Naming,
                        Cost = spare.Cost
                    };
                    result.Add(spareModel);
                }
            }
            return result;
        }

        public void AddOrderToDb(OrderModel orderModel)
        {
            using (var db = new STOEntities())
            {
                var order = new Order()
                {
                    ClientID = orderModel.Client.Id,
                    CarID = orderModel.Car.Id,
                    MechanicID = orderModel.Mechanic.Id ,
                    DescriptionID = orderModel.Description.Id,
                    Date_Start = orderModel.DateStart,
                    Date_End = orderModel.DateEnd
                };
                db.Order.Add(order);
                db.SaveChanges();
            }
        }

       /* public void EditOrder(OrderModel orderModel)
        {
            using (var db = new STOEntities())
            {
                var order = db.Order.FirstOrDefault(x => x.ID == orderModel.Id);
                if (order == null)
                {
                    return;
                }
                order.ClientID = orderModel.ClientId;
                order.CarID = orderModel.CarId;
                order.MechanicID = orderModel.MechanicId;
                order.DescriptionID = orderModel.DescriptionId;
                order.Date_Start = orderModel.DateStart;
                order.Date_End = orderModel.DateEnd;
                db.SaveChanges();
            }
        }*/

        public void DeleteOrderFromDb(OrderModel orderModel)
        {
            using (var db = new STOEntities())
            {
                var order = db.Order.FirstOrDefault(x => x.ID == orderModel.Id);
                if (order == null)
                {
                    return;
                }
                db.Order.Remove(order);
                db.SaveChanges();
            }
        }

        public void DeleteOrderDescription(int descId)
        {
            using (var db = new STOEntities())
            {
                var description = db.Description.FirstOrDefault(x => x.ID == descId);
                if (description == null)
                {
                    return;
                }
                db.Description.Remove(description);
                db.SaveChanges();

            }
        }

        public void DeleteOrderSpares(int descId)
        {
            using (var db =new STOEntities())
            {
                var spareUsed = db.Spares_Used.Where(x => x.DecriptionID == descId);
                foreach (var spare in spareUsed)
                {
                    db.Spares_Used.Remove(spare);
                }
                db.SaveChanges();

            }
        }

        public void DeleteOrderServices(int descId)
        {
            using (var db = new STOEntities())
            {
                var servicesDone = db.Service_Done.Where(x => x.DescriptionID == descId);
                foreach (var service in servicesDone)
                {
                    db.Service_Done.Remove(service);
                }
                db.SaveChanges();
            }
        }

        public List<OrderModel> GetOrders()
        {
            var result = new List<OrderModel>();
            using (var db = new STOEntities())
            {
                var orders = db.Order;
                foreach (var order in orders)
                {
                    var orderModel = new OrderModel()
                    {
                        Id = order.ID,
                        Mechanic = new MechanicModel(){Id = order.Mechanic.ID, Name = order.Mechanic.Name,
                            SecondName = order.Mechanic.SecondName, PhoneNumber = order.Mechanic.PhoneNumber,
                            WorkExperience = order.Mechanic.WorkExperience, LoginInfo = new LoginInfoModel(){Id = order.Mechanic.Login_Info.ID, Login = order.Mechanic.Login_Info.Login,
                                Password = order.Mechanic.Login_Info.Password, UserType = order.Mechanic.Login_Info.UserType}},

                        Car = new CarModel(){Id = order.Car.ID, Mark = order.Car.Mark, Model = order.Car.Model, Year = order.Car.Year, LastTo = order.Car.Year}, 
                        Client = new ClientModel(){Id = order.Client.ID, Name = order.Client.Name, SecondName = order.Client.SecondName, PhoneNumber = order.Client.PhoneNumber},
                        Description = new DescriptionModel(){Id = order.Description.ID, CarId = order.Car.ID, MechanicId = order.Mechanic.ID, Description = order.Description.Description1},
                        DateStart = order.Date_Start,
                        DateEnd = order.Date_End
                    };
                    result.Add(orderModel);
                }
            }

            return result;
        }

        public DescriptionModel GetOrderDescription(string text)
        {
            DescriptionModel result = new DescriptionModel();
            using (var db = new STOEntities())
            {
                var description = db.Description.FirstOrDefault(x => x.Description1 == text);
                var discModel = new DescriptionModel()
                {
                    Id = description.ID,
                    MechanicId = description.MechanicID,
                    CarId = description.CarID,
                    Description = description.Description1
                };
                result = discModel;
            }

            return result;
        }

        public void AddDescriptionToDataBase(DescriptionModel descriptionModel)
        {
            using (var db = new STOEntities())
            {
                var description = new Description()
                {
                    Description1 = descriptionModel.Description,
                    CarID = descriptionModel.CarId,
                    MechanicID = descriptionModel.MechanicId,
                };
                db.Description.Add(description);
                db.SaveChanges();
            }
        }

        public void AddSparesToOrder(List<SpareModel> spareList, int discId)
        {
            using (var db = new STOEntities())
            {
                foreach (var spareModel in spareList)
                {
                    var spareUsed = new Spares_Used()
                    {
                        SpareID = spareModel.Id,
                        DecriptionID = discId
                    };
                    db.Spares_Used.Add(spareUsed);
                    db.SaveChanges();
                };
                
            }
        }

        public void AddServicesToOrder(List<ServiceModel> serviceList, int discId)
        {
            using (var db = new STOEntities())
            {
                foreach (var serviceModel in serviceList)
                {
                    var serviceDone = new Service_Done()
                    {
                        ServiceID = serviceModel.Id,
                        DescriptionID = discId
                    };
                    db.Service_Done.Add(serviceDone);
                    db.SaveChanges();
                };

            }
        }

        public List<ServiceModel> GetServiceDoneForOrder(int discId)
        {
            List<ServiceModel> result = new List<ServiceModel>();
            using (var db = new STOEntities())
            {
                var serviceDone = db.Service_Done.Where(x => x.DescriptionID == discId);
                foreach (var service in serviceDone)
                {
                    var services = db.Service.Where(y => y.ID == service.ServiceID);
                    foreach (var serv in services)
                    {
                        var serviceModel = new ServiceModel()
                        {
                            Id = serv.ID,
                            Naming = serv.Naming,
                            Cost = serv.Cost
                        };
                        result.Add(serviceModel);
                    }
                }
            }
            return result;
        }

        public List<SpareModel> GetSpareDoneForOrder(int discId)
        {
            List<SpareModel> result = new List<SpareModel>();
            using (var db = new STOEntities())
            {
                var spareUsed = db.Spares_Used.Where(x => x.DecriptionID == discId);
                foreach (var spare in spareUsed)
                {
                    var spares = db.Spare.Where(y => y.ID == spare.SpareID);
                    foreach (var spa in spares)
                    {
                        var spareModel = new SpareModel()
                        {
                            Id = spa.ID,
                            Naming = spa.Naming,
                            Cost = spa.Cost
                        };
                        result.Add(spareModel);
                    }
                }
            }
            return result;
        }

        public void DeleteItemsFromOrder(int disId)
        {
            using (var db = new STOEntities())
            {
                var sparesToRemove = db.Spares_Used.Where(x => x.DecriptionID == disId);
                foreach (var spare in sparesToRemove)
                {
                    db.Spares_Used.Remove(spare);
                    db.SaveChanges();
                }
                var servicesToRemove = db.Service_Done.Where(x => x.DescriptionID == disId);
                foreach (var service in servicesToRemove)
                {
                    db.Service_Done.Remove(service);
                    db.SaveChanges();
                }
            }
        }

    }

}
