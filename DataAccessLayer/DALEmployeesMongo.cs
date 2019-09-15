using MongoDB.Bson;
using MongoDB.Driver;
using Shared.Entities;
using System;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class DALEmployeesMongo : IDALEmployees
    {
        private static MongoClient client = new MongoClient("mongodb://localhost:27017");
        private static IMongoDatabase database = client.GetDatabase("obligatorio");
        private IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("empleados");

        public async void AddEmployee(Employee emp)
        {
            try
            {
                BsonDocument datos = new BsonDocument();
                datos.Add("_id", emp.Id);
                datos.Add("name", emp.Name);
                datos.Add("start_date", emp.StartDate);

                if (emp.GetType() == typeof(FullTimeEmployee))
                {
                    FullTimeEmployee fullTime = (FullTimeEmployee)emp;
                    datos.Add("salary", fullTime.Salary);
                    datos.Add("type_emp", 1);
                }
                else
                {
                    PartTimeEmployee partTime = (PartTimeEmployee)emp;
                    datos.Add("rate", partTime.HourlyRate);
                    datos.Add("type_emp", 0);
                }
                await this.collection.InsertOneAsync(datos);
            }
            catch (MongoWriteException e)
            {
                // Clave de entidad duplicada
            }
        }

        public void DeleteEmployee(int id)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("_id", id);
            this.collection.DeleteOne(filter);
        }

        public void UpdateEmployee(Employee emp)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("_id", emp.Id);
            BsonDocument datos = new BsonDocument();
            datos.Add("name", emp.Name);
            datos.Add("start_date", emp.StartDate);

            if (emp.GetType() == typeof(FullTimeEmployee))
            {
                FullTimeEmployee fullTime = (FullTimeEmployee)emp;
                datos.Add("salary", fullTime.Salary);
                datos.Add("type_emp", 1);
            }
            else
            {
                PartTimeEmployee partTime = (PartTimeEmployee)emp;
                datos.Add("rate", partTime.HourlyRate);
                datos.Add("type_emp", 0);
            }
            this.collection.ReplaceOne(filter, datos);
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> retorno = new List<Employee>();
            Employee nuevo;

            BsonDocument filter = new BsonDocument();
            filter.Add("type_emp", 1);
            List<BsonDocument> listaFull = this.collection.Find(filter).ToList();

            listaFull.ForEach(empFull =>
            {
                nuevo = new FullTimeEmployee()
                {
                    Id = empFull.GetValue("_id").ToInt32(),
                    Name = empFull.GetValue("name").ToString(),
                    Salary = empFull.GetValue("salary").ToInt32(),
                    StartDate = new DateTime()
                };
                retorno.Add(nuevo);
            });

            filter = new BsonDocument();
            filter.Add("type_emp", 0);
            List<BsonDocument> listaPart = this.collection.Find(filter).ToList();

            listaPart.ForEach(empPart =>
            {
                nuevo = new PartTimeEmployee()
                {
                    Id = empPart.GetValue("_id").ToInt32(),
                    Name = empPart.GetValue("name").ToString(),
                    HourlyRate = empPart.GetValue("rate").ToInt32(),
                    StartDate = new DateTime()
                };
                retorno.Add(nuevo);
            });
            return retorno;
        }

        public Employee GetEmployee(int id)
        {
            Employee ret = new FullTimeEmployee();
            BsonDocument filter = new BsonDocument();
            filter.Add("_id", id);
            List<BsonDocument> lista = this.collection.Find(filter).ToList();
            lista.ForEach(emp => {
                if (emp.GetValue("type_emp").ToBoolean())
                {
                    ret = new FullTimeEmployee()
                    {
                        Id = emp.GetValue("_id").ToInt32(),
                        Name = emp.GetValue("name").ToString(),
                        Salary = emp.GetValue("salary").ToInt32(),
                        StartDate = new DateTime()
                    };
                }
                else
                {
                    ret = new PartTimeEmployee()
                    {
                        Id = emp.GetValue("_id").ToInt32(),
                        Name = emp.GetValue("name").ToString(),
                        HourlyRate = emp.GetValue("rate").ToInt32(),
                        StartDate = new DateTime()
                    };
                }
            });
            return ret;
        }
    }
}
