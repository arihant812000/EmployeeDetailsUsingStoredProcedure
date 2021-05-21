using EmpApiEntityFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmpApiEntityFramework.Controllers
{
    public class EmployeeController : ApiController
    {
        private employeedatainfoEntities context;
        EmployeeController()
        {
            context = new employeedatainfoEntities();
        }
        public IEnumerable<employeeDetail> get()
        {
            return context.getAll().ToList();
        }
        public HttpResponseMessage post(employeeDetail ed)
        {
            String emp = Newtonsoft.Json.JsonConvert.SerializeObject(ed);
            int res = context.AddEmployee(emp);
            if (res != 0)
            {
 return Request.CreateResponse(HttpStatusCode.OK, "employee added");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
            }
        }
        public HttpResponseMessage put([FromBody]employeeDetail ed,[FromUri]int id)
        {
            String emp = Newtonsoft.Json.JsonConvert.SerializeObject(ed);
            int res = context.EditEmployee(id,emp);
            if (res != 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "employee edited");
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "something went wrong");
            }
        }
        public HttpResponseMessage delete(int id)
        {
            try
            {
 int res=context.deleteEmp(id);
                if (res != 0)
                {
 return Request.CreateResponse(HttpStatusCode.OK, "deleted");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "entry not found");
                }
            }
           catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "entry not found");
            }
        } 
    }
}
