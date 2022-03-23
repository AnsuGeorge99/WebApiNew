using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiNew
{
    public class EmployeeSecurity
    {
        public static bool Login(string username, string password)
        {
            using (EmpDBEntities db = new EmpDBEntities())
            {
                return db.Users.Any(x => x.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && x.Password == password);
            }
        }
    }
}