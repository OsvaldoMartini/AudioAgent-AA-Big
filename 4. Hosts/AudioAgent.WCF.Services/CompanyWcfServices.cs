using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using AudioAgent.Services.DataTransferObject;
using AudioAgent.Services.ServiceImplementation;

namespace AudioAgent.WCF.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class CompanyWcfServices : ICompanyWcfServices
    {
        public CompanyWcfServices()
        {
            CultureInfo ci = new CultureInfo("en-US", false);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public List<CompanyImageDto> Search(string url)
        {
            if (url.Contains("https"))
            {
                url = url.Replace("https", "https://");
            }
            else if (url.Contains("http"))
            {
                url = url.Replace("http", "http://");
            }

            CompanyImageService _companySalesService = new CompanyImageService();

            var companySales = _companySalesService.GetCompanyImageByUrl(null, url);

            return companySales;
        }
    }
}
