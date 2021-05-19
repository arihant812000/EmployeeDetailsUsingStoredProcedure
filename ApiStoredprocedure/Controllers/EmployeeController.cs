using ApiStoredprocedure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiStoredprocedure.Controllers
{
    
    public class EmployeeController : ApiController
    {
private EmployeeContext context = new Models.EmployeeContext();
        public IEnumerable<employeeDetails> get()
        {
          
 var data=context.Database.SqlQuery<employeeDetails>("getAll").ToList();
            
            foreach (var dat in data)
            {
            var id = new System.Data.SqlClient.SqlParameter
            {
                ParameterName = "id",
                Value = dat.EmpId
            };
                dat.empaddr = context.Database.SqlQuery<EmployeeAddress>("exec getAdress @id",id).ToList();
            }
            return data;
           
           
        }
        public HttpResponseMessage post([FromBody]employeeDetails ed)
        {
            try
            {
                if (ed.First_Name == ""||ed.First_Name==null|| ed.Last_Name == "" || ed.Last_Name == null|| ed.Age == 0 || ed.Age == null || ed.Date_of_Birth == "" || ed.Date_of_Birth == null || ed.Department == "" || ed.Department == null || ed.Email == "" || ed.Email == null || ed.Job_Role == "" || ed.Job_Role == null || ed.Phone_No == "" || ed.Phone_No == null || ed.empaddr.Count == 0 )
                {
                    throw new Exception();
                }
 string str = Newtonsoft.Json.JsonConvert.SerializeObject(ed);

            int res = context.Database.ExecuteSqlCommand("exec AddEmployee {0}",str);
            return Request.CreateResponse(HttpStatusCode.OK,"Employee added");
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Something went  wrong try again later");
            }
           
        }
        public HttpResponseMessage put([FromBody] employeeDetails ed,[FromUri]int id)
        {
            try
            {
                if (ed.First_Name == "" || ed.First_Name == null || ed.Last_Name == "" || ed.Last_Name == null || ed.Age == 0 || ed.Age == null || ed.Date_of_Birth == "" || ed.Date_of_Birth == null || ed.Department == "" || ed.Department == null || ed.Email == "" || ed.Email == null || ed.Job_Role == "" || ed.Job_Role == null || ed.Phone_No == "" || ed.Phone_No == null || ed.empaddr.Count == 0)
                {
                    throw new Exception();
                }
                string str = Newtonsoft.Json.JsonConvert.SerializeObject(ed);

            int res = context.Database.ExecuteSqlCommand("exec EditEmployee {0},{1}",id, str);
            return Request.CreateResponse(HttpStatusCode.OK,"Employee edited sucessfully");
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No such data found");
            }
            
        }
        public HttpResponseMessage delete(int id)
        {
            try
            {
 int res = context.Database.ExecuteSqlCommand("exec deleteEmp {0}", id );
                return Request.CreateResponse(HttpStatusCode.OK, "Employee deleted sucessfully");
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "No such employee found");
            }
           
           
           
        }
    }
}
