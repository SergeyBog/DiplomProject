using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diplom.Models;

namespace Diplom.MechanicFlow
{
    public class MechanicDbWorker
    {
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
                    x.ID == mechanicModel.LoginInfo.Id );
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
    }
}
