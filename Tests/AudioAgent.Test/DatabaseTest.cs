﻿using System;
using System.Diagnostics;
using AudioAgent.Data;
using AudioAgent.Data.IRepository;
using AudioAgent.Services.DataTransferObject;
using AudioAgent.Services.ServiceContrat;
using AudioAgent.Services.ServiceImplementation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AudioAgent.SelleniumTest
{
    [TestClass]
    public class DatabaseTest
    {
        private static IEmployeeService ServiceEmployee { get; set; }
        private static ICompanyService CompanyService { get; set; }
        private static IGeoLocalizationService GeoLocalizationService { get; set; }

        //Inicialização dentro dos Testes	
        [ClassInitialize]
        public static void SetUp(TestContext testContext)
        {
            ServiceEmployee = EmployeeService.Instance();
            CompanyService = new CompanyService();
            GeoLocalizationService = new GeoLocalizationService();
        }



        [TestMethod]
        public void EmployeeInsert()
        {

            EmployeeDto employee = new EmployeeDto
            {
                FirstName =  "Claudia",
                LastName = "Martini",
                Email = "claudiabhz@gmail.com",
                UserName = "claudia",
                Password = "12346",
                RoleID = 1,
                TCompany = new CompanyDto {CompanyID = 1}
                
            };

            EmployeeService empSetvice = EmployeeService.Instance();
            empSetvice.InsertEmployee(employee);

            //Assert.AreEqual(id > 0,id,"Id Greater then '0'");

        }




        #region EmployeeAdd

        [TestMethod]
        public void EmployeeAdd()
        {
            var employee = new EmployeeEntity
            {
                LastName = "Almeida",
                FirstName = "Claudia",
                UserName = "calmeida",
                Email = "claudiabhz@hillgate.co.uk",
                Password = "1234567",
                RoleID = 3
            };

            var mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(c => c.Add(It.IsAny<EmployeeEntity>()));

            mockEmployeeRepository.Verify(c => c.Add(It.IsAny<EmployeeEntity>()), Times.Never);

            //repositoryEmployee.Add(employee);
        }

        #endregion

        #region EmployeeUpdateByFirst

        [TestMethod]
        public void EmployeeUpdateByFirst()
        {
            EmployeeDto employeeDto = new EmployeeDto
            {
                TCompany = new CompanyDto
                {
                  CompanyID  = 2,
                  Name = "Hillgate"
                },
                UserName = "claudia",
                Password = "123456"
            };


            var employee = ServiceEmployee.FindByUserName(employeeDto);

           if (employee != null)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Id: {0} -> Name: {1} ", employee.EmployeeID, employee.FirstName + " " + employee.LastName));
                employee.LastName = "Almeida Tereza";

                ServiceEmployee.UpdateEmployee(employee);

                foreach (EmployeeDto employeeLoop in ServiceEmployee.GetAllEmployee(new EmployeeDto
                {
                    TCompany = new CompanyDto
                    {
                        CompanyID = 2,
                        Name = "Hillgate"
                    }
                    
                }))
                {
                    System.Diagnostics.Debug.WriteLine("Employees:" + employeeLoop.EmployeeID + "-" + employeeLoop.FirstName + " " + employeeLoop.LastName);
                }

            }
            else
            {
                System.Diagnostics.Debug.WriteLine(String.Format("None with name: {0} ", employeeDto.UserName));
                //Assert.Fail("Vazio");
            }
        }

        [TestMethod]
        public void EmployeeDelete()
        {
            EmployeeDto employeeDto = new EmployeeDto
            {
                TCompany = new CompanyDto{CompanyID = 2},
                UserName = "Marias",
                Password = "123456"
            };


            var employee = ServiceEmployee.FindByUserName(employeeDto);

            if (employee != null)
            {
                System.Diagnostics.Debug.WriteLine(String.Format("Id: {0} -> Name: {1} ", employee.EmployeeID,
                    employee.FirstName + " " + employee.LastName));
             
                ServiceEmployee.DeleteEmployee(employee);
            }
        }


        [TestMethod]
        public void EmployeeUpdateBySingle()
        {
            //Entity Framework
            //string email = "Claudia@gmail.com";

            //var employee = repositoryEmployee.Single(c => c.Email.Equals(email));

            //if (employee != null)
            //{
            //    System.Diagnostics.Debug.WriteLine(String.Format("Id: {0} -> Name: {1} ", employee.EmployeeID, employee.FirstName + " " + employee.LastName));
            //    employee.LastName = "Martini";

            //    repositoryEmployee.Update(employee);

            //    foreach (EmployeeEntity employeeLoop in repositoryEmployee.GetAll())
            //    {
            //        System.Diagnostics.Debug.WriteLine("Employees:" + employeeLoop.EmployeeID + "-" + employeeLoop.FirstName + " " + employeeLoop.LastName);
            //    }

            //}
            //else
            //{
            //    System.Diagnostics.Debug.WriteLine(String.Format("None with the email: {0} ", email));
            //    //Assert.Fail("Vazio");
            //}
        }


        
        [TestMethod]
        public void Test_Company_FindById()
        {
            var company = CompanyService.FindByID(1);
            System.Diagnostics.Debug.WriteLine(company);

        }
        #endregion

        #region GeoLocalization Insert

        [TestMethod]
        public void Test_GeoLocalization_Insert()
        {
            GeoLocalizationDto geoLocal = new GeoLocalizationDto() {CompanyID = 1, EmployeeID = 1, Lat = "51.522483", Lng = "-0.125034",LocalName = "Russel Square"};
            try
            {
                GeoLocalizationService.InsertGeoLocalization(geoLocal);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"MySql Error: {ex.Message}");
            }
        }
        #endregion


    }

}