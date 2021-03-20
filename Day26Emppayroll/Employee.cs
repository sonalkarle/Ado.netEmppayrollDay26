using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    public class Employee
    {
        public int  ID { get; set; }
        public string Name { get; set; }

        public DateTime StartDate { get; set; }
        public string Gender { get; set; }
        public double Phonenumber { get; set; }


        public string Address { get; set; }
       
        public string Department { get; set; }
        public double BasicPay { get; set; }
        public int Deduction { get; set; }
        public int TaxablePay { get; set; }
        public int IncomeTax { get; set; }
        public int NetPay { get; set; }
        public int Salary { get; set; }
    }
}
