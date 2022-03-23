using EmployeeDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiNew.Controllers
{
    
    public class EmployeesController : ApiController
    {    
        [BasicAuthentication]
        public HttpResponseMessage Get(string gender = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            using (EmpDBEntities db = new EmpDBEntities())
            {
                switch (username.ToLower())
                {
                    case "male":
                        return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(x => x.Gender.ToLower() == "male").ToList());

                    case "female":
                        return Request.CreateResponse(HttpStatusCode.OK, db.Employees.Where(x => x.Gender.ToLower() == "female").ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "values for gender must be male, female, and all" + gender + "is Invalid");
                }
            }
        }

        public HttpResponseMessage Get(int id)
        {
            try
            {
                using (EmpDBEntities db = new EmpDBEntities())
                {
                    var employee = db.Employees.FirstOrDefault(x => x.ID == id);
                    if (employee != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, employee);
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id" + id.ToString() + "not found");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Post([FromBody] Employee emp)
        {
            try
            {
                using (EmpDBEntities db = new EmpDBEntities())
                {
                    db.Employees.Add(emp);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.OK);
                    message.Headers.Location = new Uri(Request.RequestUri + emp.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Put(int id, [FromBody] Employee emp)
        {
            try
            {
                using (EmpDBEntities db = new EmpDBEntities())
                {
                    var update = db.Employees.Where(x => x.ID == id).FirstOrDefault();
                    if (update != null)
                    {
                        update.FirstName = emp.FirstName;
                        update.LastName = emp.LastName;
                        update.Gender = emp.Gender;
                        update.Salary = emp.Salary;
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, update);
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id" + id.ToString() + "not found to update");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (EmpDBEntities db = new EmpDBEntities())
                {
                    var delete = db.Employees.FirstOrDefault(x => x.ID == id);
                    if (delete != null)
                    {
                        db.Employees.Remove(delete);
                        db.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with id" + id.ToString() + "not found to delete");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
