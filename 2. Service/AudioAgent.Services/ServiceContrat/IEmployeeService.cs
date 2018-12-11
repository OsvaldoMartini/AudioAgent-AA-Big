using System.Collections.Generic;
using System.ServiceModel;
using AudioAgent.Services.DataTransferObject;

namespace AudioAgent.Services.ServiceContrat
{
    [ServiceContract]
    public interface IEmployeeService
    {
        [OperationContract]
        IList<EmployeeDto> GetAllEmployee(EmployeeDto employee);

        [OperationContract]
        EmployeeDto FindByID(EmployeeDto employee);

        [OperationContract]
        string DeleteEmployee(EmployeeDto employee);

        [OperationContract]
        void UpdateEmployee(EmployeeDto employee);

        void InsertEmployee(EmployeeDto employee);

        EmployeeDto FindByUserName(EmployeeDto employee);
    }
}