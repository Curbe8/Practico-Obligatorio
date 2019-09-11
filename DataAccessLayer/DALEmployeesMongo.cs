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
        private static IMongoDatabase database = client.GetDatabase("foo");
        private IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("bar");

        public async void AddEmployee(Employee emp)
        {
            BsonDocument datos = new BsonDocument();
            datos.Add("ID", emp.Id);
            datos.Add("NAME", emp.Name);
            datos.Add("START_DATE", emp.StartDate);

            if (emp.GetType() == typeof(FullTimeEmployee))
            {
                FullTimeEmployee fullTime = (FullTimeEmployee)emp;
                datos.Add("SALARY", fullTime.Salary);
            }
            else
            {
                PartTimeEmployee partTime = (PartTimeEmployee)emp;
                datos.Add("RATE", partTime.HourlyRate);
            }
            await this.collection.InsertOneAsync(datos);
        }

        public void DeleteEmployee(int id)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("ID", id);
            this.collection.DeleteOne(filter);
        }

        public void UpdateEmployee(Employee emp)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("ID", emp.Id);
            BsonDocument datos = new BsonDocument();
            datos.Add("ID", emp.Id);
            datos.Add("NAME", emp.Name);
            datos.Add("START_DATE", emp.StartDate);

            if (emp.GetType() == typeof(FullTimeEmployee))
            {
                FullTimeEmployee fullTime = (FullTimeEmployee)emp;
                datos.Add("SALARY", fullTime.Salary);
            }
            else
            {
                PartTimeEmployee partTime = (PartTimeEmployee)emp;
                datos.Add("RATE", partTime.HourlyRate);
            }
            this.collection.UpdateOne(filter, datos);
        }

        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>();
        }

        public Employee GetEmployee(int id)
        {
            BsonDocument filter = new BsonDocument();
            filter.Add("ID", id);
            BsonDocument documento = (BsonDocument)this.collection.Find(filter);
            IEnumerator<BsonValue> it = documento.Values.GetEnumerator();
            while(it.Current != null)
            {
                BsonValue valor = it.Current;
                valor.ToString();
                it.MoveNext();

            };
            return new FullTimeEmployee();
        }
    }
}
