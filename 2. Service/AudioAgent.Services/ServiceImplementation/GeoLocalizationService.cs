using System;
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
    public class GeoLocalizationService : IGeoLocalizationService, IDisposable
    {
        #region Primitive Properties

        private readonly IGeoLocalizationRepository _geoLocalizationRepository;

        #endregion

        #region Constructors

        public GeoLocalizationService()
        {
            ModelMapper.Configure();
            _geoLocalizationRepository = new GeoLocalizationRepository();
        }

        #endregion


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IList<GeoLocalizationDto> GetAllGeoLocalization(GeoLocalizationDto geoLocalization)
        {
            return Mapper.Map<IList<GeoLocalizationEntity>, IList<GeoLocalizationDto>>(_geoLocalizationRepository.GetAllByCompany(Mapper.Map<GeoLocalizationDto, GeoLocalizationEntity>(geoLocalization)).ToList());
        }

        public void UpdateGeoLocalization(GeoLocalizationDto geoLocalization)
        {
            _geoLocalizationRepository.Update(Mapper.Map<GeoLocalizationDto, GeoLocalizationEntity>(geoLocalization));
        }

        public GeoLocalizationDto FindByID(GeoLocalizationDto geoLocalization)
        {
            var geoLocalizationFirst = _geoLocalizationRepository.FindByID(Mapper.Map<GeoLocalizationDto, GeoLocalizationEntity>(geoLocalization));

            return Mapper.Map<GeoLocalizationEntity, GeoLocalizationDto>(geoLocalizationFirst);
        }

        public void InsertGeoLocalization(GeoLocalizationDto geoLocalization)
        {
            _geoLocalizationRepository.Insert(Mapper.Map<GeoLocalizationDto, GeoLocalizationEntity>(geoLocalization));
        }

        public string DeleteGeoLocalization(GeoLocalizationDto geoLocalization)
        {
            return _geoLocalizationRepository.Delete(Mapper.Map<GeoLocalizationDto, GeoLocalizationEntity>(geoLocalization));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}