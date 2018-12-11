using System.Collections.Generic;
using System.ServiceModel;
using AudioAgent.Services.DataTransferObject;

namespace AudioAgent.Services.ServiceContrat
{
    [ServiceContract]
    public interface ICompanyImageService
    {
        [OperationContract]
        List<CompanyImageDto> GetCompanyImageByUrl(string typeCompany, string url);
    }
}