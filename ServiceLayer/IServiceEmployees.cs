using Shared.Entities;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServiceLayer
{
    [ServiceContract(Namespace = "http://localhost:8834/tsi1/")]
    public interface IServiceEmployees
    {
        [OperationContract]
        void AddEmployee(Employee emp);

        [OperationContract]
        void DeleteEmployee(int id);

        [OperationContract]
        void UpdateEmployee(Employee emp);

        [OperationContract]
        List<Employee> GetAllEmployees();

        [OperationContract]
        Employee GetEmployee(int id);

        [OperationContract]
        double CalcPartTimeEmployeeSalary(int idEmployee, int hours);
    }
}
