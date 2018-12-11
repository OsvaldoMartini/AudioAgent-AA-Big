using System.Collections.Generic;
using AutoMapper;
using AudioAgent.Data;
using AudioAgent.Services.DataTransferObject;

namespace AudioAgent.Services.Utils
{
    public class ModelMapper
    {
        private static bool isConfigured;
        private static readonly object lockObject = new object();

        public static void Configure()
        {
            lock (lockObject)
            {
                if (!isConfigured)
                {

                    #region [ Company ]

                    Mapper.CreateMap<CompanyDto, CompanyEntity>()
                        .ForMember(dest => dest.EmployeeList,
                            opt => opt.MapFrom(src => src.EmployeeList));

                    Mapper.CreateMap<CompanyEntity, CompanyDto>()
                        .ForMember(dest => dest.EmployeeList,
                            opt => opt.MapFrom<ICollection<EmployeeEntity>>(src => src.EmployeeList))
                        .ForMember(dest => dest.RoleName, opt => opt.Ignore())
                        .ForMember(dest => dest.NameUserRole, opt => opt.Ignore());
                    #endregion

                    #region [ GeoLocalization ]

                    Mapper.CreateMap<GeoLocalizationDto, GeoLocalizationEntity>();
                    Mapper.CreateMap<GeoLocalizationEntity, GeoLocalizationDto>()
                        .ForMember(dest => dest.RoleName, opt => opt.Ignore())
                        .ForMember(dest => dest.NameUserRole, opt => opt.Ignore())
                        .ForMember(dest => dest.CompanyID, opt => opt.Ignore());

                    Mapper.CreateMap<IEnumerable<GeoLocalizationDto>, IEnumerable<GeoLocalizationEntity>>();
                    Mapper.CreateMap<IEnumerable<GeoLocalizationEntity>, IEnumerable<GeoLocalizationDto>>();

                    #endregion

                    #region [ Employee ]

                    Mapper.CreateMap<EmployeeDto, EmployeeEntity>();
                    
                    Mapper.CreateMap<EmployeeEntity, EmployeeDto>()
                        .ForMember(dest => dest.RoleName, opt => opt.Ignore())
                        .ForMember(dest => dest.NameUserRole, opt => opt.Ignore())
                        .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore());

                    Mapper.CreateMap<IEnumerable<EmployeeEntity>, IEnumerable<EmployeeDto>>();
                    Mapper.CreateMap<IEnumerable<EmployeeDto>, IEnumerable<EmployeeEntity>>();

                    #endregion

                   #region [ CompanySales ]

                    Mapper.CreateMap<CompanyImageDto, CompanyImageEntity>()
                        .ForMember(dest => dest.TCompany,
                            opt => opt.MapFrom(src => src.TCompany));

                    Mapper.CreateMap<CompanyImageEntity, CompanyImageDto>()
                        .ForMember(dest => dest.CompanyImageList,
                            opt => opt.MapFrom<ICollection<CompanyImageEntity>>(src => src.CompanyImageList))
                        .ForMember(dest => dest.TCompany,
                            opt => opt.MapFrom(src => src.TCompany))
                        .ForMember(dest => dest.RoleName, opt => opt.Ignore())
                        .ForMember(dest => dest.NameUserRole, opt => opt.Ignore());

                    Mapper.CreateMap<IList<CompanyImageEntity>, IList<CompanyImageDto>>();
                    Mapper.CreateMap<IList<CompanyImageDto>, IList<CompanyImageEntity>>();
                    #endregion


                    Mapper.AssertConfigurationIsValid();

                    isConfigured = true;
                }
            }
        }
    }
}