using System.Collections.Generic;
using AudioAgent.Data.Utils;

namespace AudioAgent.Data.IRepository
{
    public interface ICompanyImageRepository : IGenericRepository<CompanyImageEntity>
    {
        //Return Company Images By Type
        List<CompanyImageEntity> GetCompanyImageByUrl(string typeCompany, string url); 

    }
}