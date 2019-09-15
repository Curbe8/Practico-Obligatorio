using DataAccessLayer;
using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLEmployees : IBLEmployees
    {
       private IDALEmployees _dal;

        public BLEmployees(IDALEmployees dal)
        {
            _dal = dal;
        }

        public void AddEmployee(Employee emp)
        {
            this._dal.AddEmployee(emp);
        }

        public void DeleteEmployee(int id)
        {
            this._dal.DeleteEmployee(id);
        }

        public void UpdateEmployee(Employee emp)
        {
            this._dal.UpdateEmployee(emp);
        }

        public List<Employee> GetAllEmployees()
        {
            return this._dal.GetAllEmployees();
        }

        public Employee GetEmployee(int id)
        {
            return this._dal.GetEmployee(id);
        }

        public double CalcPartTimeEmployeeSalary(int idEmployee, int hours)
        {
            Employee emp = this._dal.GetEmployee(idEmployee);
            if(emp != null && emp.GetType() == typeof(PartTimeEmployee))
            {
                PartTimeEmployee partTime = (PartTimeEmployee)emp;
                return hours * partTime.HourlyRate;
            } else
            {
                if(emp == null)
                    throw new NullReferenceException();
                else
                    throw new InvalidCastException();
            }
        }
    }
}
