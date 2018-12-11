using AudioAgent.Data.Utils;

namespace AudioAgent.Data.IRepository
{
    public interface IEmployeeRepository : IGenericRepository<EmployeeEntity>
    {
        EmployeeEntity FindByUserName(EmployeeEntity _employee);
    }
}