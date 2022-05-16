using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.CarFlow
{
    public class CarDbWorker
    {
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
    }
}
