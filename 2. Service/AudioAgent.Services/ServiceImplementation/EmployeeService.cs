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
    public class EmployeeService : IEmployeeService, IDisposable
    {
        #region Primitive Properties

        private readonly IEmployeeRepository _employeeRepository;

        #endregion

        #region Constructors


        private static EmployeeService self;

        public static EmployeeService Instance()
        {
            if (self == null)
                self = new EmployeeService();
            return self;
        }

        public EmployeeService()
        {
            ModelMapper.Configure();
            _employeeRepository = EmployeeRepository.Instance();
        }

        #endregion


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IList<EmployeeDto> GetAllEmployee(EmployeeDto employee)
        {
            var employeeList = _employeeRepository.GetAllByCompany(Mapper.Map<EmployeeDto, EmployeeEntity>(employee));

            return Mapper.Map<IList<EmployeeEntity>, IList<EmployeeDto>>(employeeList.ToList());
        }

        public void UpdateEmployee(EmployeeDto employee)
        {
            _employeeRepository.Update(Mapper.Map<EmployeeDto, EmployeeEntity>(employee));
        }

        public EmployeeDto FindByID(EmployeeDto employee)
        {
            var employeeFirst = _employeeRepository.FindByID(Mapper.Map<EmployeeDto, EmployeeEntity>(employee));

            return Mapper.Map<EmployeeEntity, EmployeeDto>(employeeFirst);
        }

        public EmployeeDto FindByUserName(EmployeeDto employee)
        {
            var employeeFirst = _employeeRepository.FindByUserName(Mapper.Map<EmployeeDto, EmployeeEntity>(employee));
            return Mapper.Map<EmployeeEntity, EmployeeDto>(employeeFirst);
        }


        public void InsertEmployee(EmployeeDto employee)
        {
            _employeeRepository.Insert(Mapper.Map<EmployeeDto, EmployeeEntity>(employee));
        }

        public string DeleteEmployee(EmployeeDto employee)
        {
            return _employeeRepository.Delete(Mapper.Map<EmployeeDto, EmployeeEntity>(employee));
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