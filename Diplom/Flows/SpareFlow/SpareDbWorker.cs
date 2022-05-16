using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.SpareFlow
{
    public class SpareDbWorker
    {
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
    }
}
