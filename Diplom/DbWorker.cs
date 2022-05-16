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
        
    }
}
