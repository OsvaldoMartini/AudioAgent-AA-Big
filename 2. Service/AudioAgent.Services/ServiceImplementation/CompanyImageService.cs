using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AudioAgent.Data;
using AudioAgent.Data.IRepository;
using AudioAgent.Data.Repository;
using AudioAgent.Services.DataTransferObject;
using AudioAgent.Services.ServiceContrat;
using AudioAgent.Services.Utils;

namespace AudioAgent.Services.ServiceImplementation
{
    public class CompanyImageService : ICompanyImageService, IDisposable
    {
        #region Primitive Properties

        private readonly ICompanyImageRepository _companyImageRepository;

        #endregion

        #region Constructors

        public CompanyImageService()
        {
            //Mapper DTOs -> BOs  and BOs -> DTOS
            //Mapper.CreateMap<CompanySaleDto, CompanySaleEntity>();
            //Mapper.CreateMap<CompanySaleEntity, CompanySaleDto>();
            ModelMapper.Configure();
            _companyImageRepository = new CompanyImageRepository();
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

        public List<CompanyImageDto> GetCompanyImageByUrl(string companyType, string url)
        {
            List<CompanyImageEntity> companiesImages = _companyImageRepository.GetCompanyImageByUrl(companyType, url);

            return Mapper.Map<List<CompanyImageEntity>, List<CompanyImageDto>>(companiesImages);
            
            //return Mapper.Map<CompanySaleEntity, CompanySaleDto > (companiesSales);
        }
      
    }
}