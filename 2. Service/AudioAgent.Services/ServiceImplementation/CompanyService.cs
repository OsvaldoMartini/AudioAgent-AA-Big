using System;
using AutoMapper;
using AudioAgent.Data;
using AudioAgent.Data.IRepository;
using AudioAgent.Data.Repository;
using AudioAgent.Services.DataTransferObject;
using AudioAgent.Services.ServiceContrat;

namespace AudioAgent.Services.ServiceImplementation
{
    public class CompanyService : ICompanyService, IDisposable
    {
        #region Primitive Properties

        private readonly ICompanyRepository _companyRepository;

        #endregion

        #region Constructors

        public CompanyService()
        {
            //Mapper DTOs -> BOs  and BOs -> DTOS
            Mapper.CreateMap<CompanyDto, CompanyEntity>();
            Mapper.CreateMap<CompanyEntity, CompanyDto>();

            _companyRepository = new CompanyRepository();
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }

        public CompanyDto FindByID(int id)
        {
            var company = _companyRepository.FindByID(id);

            return Mapper.Map<CompanyEntity, CompanyDto>(company);
        }
    }
}