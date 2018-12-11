using System.ServiceModel;
using AudioAgent.Services.DataTransferObject;

namespace AudioAgent.Services.ServiceContrat
{
    [ServiceContract]
    public interface ICompanyService
    {
        [OperationContract]
        
        CompanyDto FindByID(int id);
    }
}