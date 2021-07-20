using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    public class EmployeeController : ApiController
    {
        /*
        public IEnumerable<Employee> Get()
        {
            using (WEBAPIEntities dbcontext=new WEBAPIEntities())
            {
                return dbcontext.Employees.ToList();
            }

        }

        public Employee Get(int id)
        {
            using (WEBAPIEntities dbcontext = new WEBAPIEntities())
            {
                return dbcontext.Employees.FirstOrDefault(e => e.ID == id);
            }
        }
        */
        public HttpResponseMessage Get() {

            using (WEBAPIEntities dbcontext = new WEBAPIEntities())
            {
                var Employees= dbcontext.Employees.ToList();
                return Request.CreateResponse(HttpStatusCode.OK, Employees);

            }

        }

        public HttpResponseMessage Get(int id)
        {

            using (WEBAPIEntities dbcontext = new WEBAPIEntities())
            {
                var entity = dbcontext.Employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id " + id.ToString() + "not found");
                }
               

            }

        }
        /*
        public void Post([FromBody] Employee employee)
        {
            using (WEBAPIEntities dbcontext = new WEBAPIEntities())
            {
                dbcontext.Employees.Add(employee);
                dbcontext.SaveChanges();
            }
        }*/

        
        public HttpResponseMessage Post([FromBody] Employee employee)
        {
            try
            {
                using (WEBAPIEntities dbcontext = new WEBAPIEntities())
                {
                    dbcontext.Employees.Add(employee);
                    dbcontext.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        /*
        public void Put(int id, [FromBody] Employee employee)
        {
            using (WEBAPIEntities dbcontext = new WEBAPIEntities())
            {
                var entity = dbcontext.Employees.FirstOrDefault(e => e.ID == id);
                entity.FirstName = employee.FirstName;
                entity.LastName = employee.LastName;
                entity.Gender = employee.Gender;
                entity.Salary = employee.Salary;
                dbcontext.SaveChanges();
            }
        }*/
        
        
        public HttpResponseMessage Put([FromBody]int id, [FromUri] Employee employee)
        {
            try
            {
                using (WEBAPIEntities dbcontext = new WEBAPIEntities())
                {
                    var entity = dbcontext.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id " + id.ToString() + " not found to update");
                    }
                    else
                    {
                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;
                        dbcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        /*
        public void Delete(int id)
        {
            using (WEBAPIEntities dbcontext = new WEBAPIEntities())
            {
                dbcontext.Employees.Remove(dbcontext.Employees.FirstOrDefault(e => e.ID == id));
                dbcontext.SaveChanges();
            }
        }*/


        
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using (WEBAPIEntities dbcontext = new WEBAPIEntities())
                {
                    var entity = dbcontext.Employees.FirstOrDefault(e => e.ID == id);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + id.ToString() + " not found to delete");
                    }
                    else
                    {
                        dbcontext.Employees.Remove(entity);
                        dbcontext.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }



    }



}
