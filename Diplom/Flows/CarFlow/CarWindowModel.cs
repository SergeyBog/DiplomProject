using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.CarFlow
{
    public class CarWindowModel
    {
        public CarModel CarModel = new CarModel();
        private readonly CarDbWorker _worker = new CarDbWorker();

        public List<CarModel> GetCars()
        {
           return _worker.GetCars();
        }

        public void AddCarToDb(CarModel carModel)
        {
            _worker.AddCarToDb(carModel);
        }
        public void EditCar()
        {
            _worker.EditCar(CarModel);
        }

        public void DeleteCar()
        {
            _worker.DeleteCarFromDb(CarModel);
        }

        public void GetDataForModel(string mark, string model, string year, string lastTo)
        {
            CarModel.Mark = mark;
            CarModel.Model = model;
            CarModel.Year = year;
            CarModel.LastTo = lastTo;
        }

    }
}
