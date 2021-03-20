using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EmployeePayroll
{
    public class Program
    {
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
       

        static void Main(string[] args)
        {
            Console.WriteLine("welcome to Employee Payroll Service");
            
        
            
            
        }
        
    }
}
