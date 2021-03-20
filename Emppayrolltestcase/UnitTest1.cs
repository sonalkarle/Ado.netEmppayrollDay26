using EmployeePayroll;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Emppayrolltestcase
{
    public class Tests
    {/*
        [Test]
        public void RetrievingEmployeePayrollWithinDateRange_ShouldReturnList()
        {
            DateTime FromDate = DateTime.Parse("2018-01-01");
            DateTime ToDate = DateTime.Parse("2020-01-01");
            List<Employee> result = Program.GetAllEmployeePayrollData_FromDateRange(FromDate, ToDate);
            Assert.IsNotNull(result);
        }
        */

        [Test]
        public void UpdatingSalary_ShouldReturnNoOfUpdatedEmpObject()
        {
            string dateString = "2018-01-01";
            DateTime dateTime = DateTime.Parse(dateString);
            Employee employee = new Employee
            {
                ID = 5,
                Name = "Terrisa",
                BasicPay = 3000000,
          
            };
            var result = Program.UpdateSalary(employee);
            var expected = result.BasicPay;
            var actual = employee.BasicPay;
            Assert.AreEqual(expected,actual);
        }

        /*
        [Test]
        public void RemovingEmployeeFromPayroll_ShouldReturnExpected()
        {
            string dateString = "2019-11-13";
            DateTime dateTime = DateTime.Parse(dateString);
            Employee employee = new Employee
            {
                ID = 7,
               
            };
            bool result =Program.RemoveEmployeeFromPayroll(employee);
            Assert.IsTrue(result);
        }
        */


        [Test]
        public void InsertingAllEmployeeDetail_ShouldReturnIndetityKey()
        {
            string dateString = "May 01, 2020";
            DateTime dateTime = DateTime.Parse(dateString);
            Employee employee = new Employee
            {
                Name = "gwen",
                Gender = "F",
                StartDate = dateTime,
                BasicPay = 8000,
                Department = "DEO",
            };
            var result = Program.InsertEmployeeData(employee);
            var Expected = employee.Name;
            var Actual = result.Name;
            Assert.AreEqual(Expected, Actual);
        }
        [Test]
        public void RetrievingAverageSalaryOfAllEmloyees_ShouldReturnExpected()
        {
            decimal result = Program.GetAveragefSalary_OfAllEmployees();
            Assert.AreEqual(32000, result);
        }


        [Test]
        public void RetrievingNooffemaleEmloyees_ShouldReturnExpected()
        {
            int result = Program.GetNoOfFemaleEmployees();
            Assert.AreEqual(5, result);
        }

        [Test]
        public void RetrievingNoOfMaleEmloyees_ShouldReturnExpected()
        {
            int result = Program.GetNoOfMaleEmployees();
            Assert.AreEqual(2, result);
        }
        [Test]
        public void RetrievingMinSalaryOfFemaleEmloyees_ShouldReturnExpected()
        {
            double result = Program.GetMinOfSalary_OfFemaleEmployees();
            Assert.AreEqual(12000, result);
        }

        [Test]
        public void RetrievingSumOfSalaryOfAllFemaleEmloyees_ShouldReturnExpected()
        {
            double result = Program.GetSumOfSalary_OfAllFemaleEmployee();
            Assert.AreEqual(150000, result);
        }
        /// <summary>
        /// Retrievings the sum of salary of all male emloyees should return expected.
        /// </summary>
        [Test]
        public void RetrievingSumOfSalaryOfAllMaleEmloyees_ShouldReturnExpected()
        {
            double result = Program.GetSumOfSalary_OfAllMaleEmployee();
            Assert.AreEqual(1500000, result);
        }
    }
}