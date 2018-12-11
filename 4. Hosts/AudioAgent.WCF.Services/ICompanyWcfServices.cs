using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using AudioAgent.Services.DataTransferObject;

namespace AudioAgent.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICompanyWcfServices
    {
        //{ImageUrlSearch.svc/v1/Search/1)
        [OperationContract]
        [WebGet(UriTemplate = "v1/Search/{*url}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        List<CompanyImageDto> Search(string url);
    }

}
