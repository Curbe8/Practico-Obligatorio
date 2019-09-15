using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiServices.Controllers
{
    public class EmployeeController : ApiController
    {
        private BusinessLogicLayer.IBLEmployees IEmp = new BusinessLogicLayer.BLEmployees(metodos);
        private static DataAccessLayer.DALEmployeesMongo metodos = new DataAccessLayer.DALEmployeesMongo();
        // GET api/<controller>
        public List<Shared.Entities.Employee> Get()
        {
            return IEmp.GetAllEmployees();
        }

        // GET api/<controller>/5
        public Shared.Entities.Employee Get(int id)
        {
            return IEmp.GetEmployee(id);
        }

        // POST api/<controller>
        public void Post([FromBody]Shared.Entities.Employee emp)
        {
            IEmp.AddEmployee(emp);
        }

        // PUT api/<controller>/5
        public void Put(Shared.Entities.Employee emp)
        {
            IEmp.UpdateEmployee(emp);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            IEmp.DeleteEmployee(id);
        }

        public double CalcSalary(int id, int hours)
        {
            return IEmp.CalcPartTimeEmployeeSalary(id, hours);
        }
    }
}