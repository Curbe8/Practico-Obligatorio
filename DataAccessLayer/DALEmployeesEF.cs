using Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class DALEmployeesEF : IDALEmployees
    {
        public void AddEmployee(Employee emp)
        {
            using (Model.ObligatorioEntities en = new Model.ObligatorioEntities())
            {
                Model.Employee nuevo = null;
                if (emp.GetType() == typeof(FullTimeEmployee))
                {
                    FullTimeEmployee fullTime = (FullTimeEmployee)emp;
                    nuevo = new Model.FullTimeEmployee()
                    {
                        EMP_ID = fullTime.Id,
                        NAME = fullTime.Name,
                        SALARY = fullTime.Salary,
                        START_DATE = fullTime.StartDate,
                        TYPE_EMP = 1
                    };
                }
                else
                {
                    PartTimeEmployee fullTime = (PartTimeEmployee)emp;
                    nuevo = new Model.PartTimeEmployee()
                    {
                        EMP_ID = fullTime.Id,
                        NAME = fullTime.Name,
                        RATE = fullTime.HourlyRate,
                        START_DATE = fullTime.StartDate,
                        TYPE_EMP = 1
                    };
                }
                en.Employee.Add(nuevo);
                en.SaveChanges();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (Model.ObligatorioEntities en = new Model.ObligatorioEntities())
            {
                Model.Employee remove = en.Employee.Find(id);
                if (remove != null)
                    en.Employee.Remove(remove);
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            using (Model.ObligatorioEntities en = new Model.ObligatorioEntities())
            {
                Model.Employee remove = en.Employee.Find(emp.Id);
                if (remove != null)
                {
                    en.Employee.Remove(remove);
                    en.SaveChanges();
                    AddEmployee(emp);
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> retorno = new List<Employee>();
            using (Model.ObligatorioEntities en = new Model.ObligatorioEntities())
            {
                List<Model.Employee> emps = en.Employee.ToList();
                if (emps.Count < 1)
                    return retorno;
                en.Employee.ToList().ForEach(emp =>
                {
                    Employee nuevo = null;
                    if (emp.GetType() == typeof(Model.FullTimeEmployee))
                    {
                        Model.FullTimeEmployee fullTime = (Model.FullTimeEmployee)emp;
                        nuevo = new FullTimeEmployee()
                        {
                            Id = fullTime.EMP_ID,
                            Name = fullTime.NAME,
                            StartDate = fullTime.START_DATE,
                            Salary = (int)fullTime.SALARY
                        };
                    }
                    else
                    {
                        Model.PartTimeEmployee partTime = (Model.PartTimeEmployee)emp;
                        nuevo = new PartTimeEmployee()
                        {
                            Id = partTime.EMP_ID,
                            Name = partTime.NAME,
                            StartDate = partTime.START_DATE,
                            HourlyRate = (double)partTime.RATE
                        };
                    }
                    retorno.Add(nuevo);
                });
            }
            return retorno;
        }

        public Employee GetEmployee(int id)
        {
            using (Model.ObligatorioEntities en = new Model.ObligatorioEntities())
            {
                Model.Employee emp = en.Employee.Find(id);
                Employee retorno = null;
                if (emp != null)
                {
                    if (emp.GetType() == typeof(Model.FullTimeEmployee))
                    {
                        Model.FullTimeEmployee fullTime = (Model.FullTimeEmployee)emp;
                        retorno = new FullTimeEmployee()
                        {
                            Id = fullTime.EMP_ID,
                            Name = fullTime.NAME,
                            StartDate = fullTime.START_DATE,
                            Salary = (int)fullTime.SALARY
                        };
                    }
                    else
                    {
                        Model.PartTimeEmployee partTime = (Model.PartTimeEmployee)emp;
                        retorno = new PartTimeEmployee()
                        {
                            Id = partTime.EMP_ID,
                            Name = partTime.NAME,
                            StartDate = partTime.START_DATE,
                            HourlyRate = (double)partTime.RATE
                        };
                    }
                }
                return retorno;
            }
        }
    }
}
