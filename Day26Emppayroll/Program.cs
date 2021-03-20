using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmployeePayroll
{
    public class Program
    {
        /// <summary>
        /// uc1: Check connetion estblish or not
        /// </summary>
        static string connectionString = "Data Source=DESKTOP-OKP25QH;Initial Catalog=Emp_payroll;Integrated Security=SSPI";
        static SqlConnection connection = new SqlConnection(connectionString);
        static void EstablishConnection()
        {
            if (connection != null && connection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    connection.Open();
                }
                catch (Exception)
                {
                    throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.CONNECTION_FAILED, "connection failed");
                }
            }
        }
        static void CloseConnection()
        {
            if (connection != null && !connection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    connection.Close();
                }
                catch (Exception)
                {
                    throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.CONNECTION_FAILED, "connection failed");
                }
            }
        }
        /// <summary>
        /// UC2: retrive the employee data
        /// </summary>
        /// <returns></returns>
        public static List<Employee> GetAllEmployeePayrollData()
        {
            List<Employee> employees = new List<Employee>();
            Employee emp = new Employee();
            SqlConnection connection = new SqlConnection(connectionString);
            string Spname = "dbo.GetAllEmployeeData";
            using (connection)
            {
                SqlCommand sqlCommand = new SqlCommand(Spname, connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                connection.Open();
                SqlDataReader dr = sqlCommand.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        emp.ID = dr.GetInt32(0);
                        emp.Name = dr.GetString(1);
                        emp.StartDate = dr.GetDateTime(2).Date;
                        emp.Gender = (dr.GetString(3));
                        emp.Phonenumber = dr.IsDBNull(4) ? 0 : dr.GetInt64(4);
                        emp.Address = dr.IsDBNull(5) ? "" : dr.GetString(5);
                        emp.Department = dr.GetString(6);
                        emp.BasicPay = dr.IsDBNull(7) ? 0 : dr.GetInt64(7);
                        emp.Deduction = dr.IsDBNull(8) ? 0 : dr.GetInt32(8);
                        emp.TaxablePay = dr.IsDBNull(9) ? 0 : dr.GetInt32(9);
                        emp.IncomeTax = dr.IsDBNull(10) ? 0 : dr.GetInt32(10);
                        employees.Add(emp);
                        Console.WriteLine(emp.ID + "," + emp.Name + "," + emp.Phonenumber+"," + emp.Department);
                    }

                }
                connection.Close();
            }
            return employees;
        }

       

        static void Main(string[] args)
        {
            Console.WriteLine("welcome to Employee Payroll Service");
            GetAllEmployeePayrollData();
        
            
            
        }
        
    }
}
