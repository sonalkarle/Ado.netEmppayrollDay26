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

        /// <summary>
        /// Updates the name of the salary by emp.
        /// </summary>
        /// <param name="empName">Name of the emp.</param>
        /// <param name="BasicPay">The salary.</param>
        /// <returns></returns>
        public static Employee UpdateSalary(Employee employee)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    string SpName = "dbo.[UpdateEmployeePayrollSalary]";
                    SqlCommand cmd = new SqlCommand(SpName, connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    employee = new Employee();
                    connection.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    if (rd.HasRows)
                    {
                        while (rd.Read())
                        {
                            employee.ID = (int)rd["ID"];
                            employee.Name =  (string)rd["Name"];
                            employee.StartDate = (DateTime)rd["StratDate"];
                            employee.Phonenumber = (Int64)rd["Phonenumber"];
                            employee.Address = (string)rd["Address"];
                            employee.Gender = (string)rd["Gender"];
                            employee.Salary = (Int32)rd["Salary"];
                            employee.BasicPay = (Int64)rd["BasicPay"];
                            employee.Deduction = (Int32)rd["Deduction"];
                            employee.TaxablePay = (Int32)rd["TaxablePay"];
                            employee.NetPay = (Int32)rd["NetPay"];
                            employee.IncomeTax = (Int32)rd["IncomeTax"];

                           
                            
                        }
                        if (employee == null)
                        {
                            throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.NO_DATA_FOUND, "no data found");
                        }
                    }
                    connection.Close();
                
                    return employee;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public static bool RemoveEmployeeFromPayroll(Employee employee)
        {
            string Spname = "dbo.[DeleteEmployeeDetail]";
            SqlCommand cmd = new SqlCommand(Spname, connection);
            cmd.CommandType = CommandType.StoredProcedure;
           
            try
            {
                EstablishConnection();
                using (connection)
                {
                    cmd.Parameters.AddWithValue("@ID", employee.ID);
                   
                    cmd.ExecuteNonQuery();
                    
                }
            }
            catch (SqlException)
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); ;
                }
            }
            return true;
        }


         public static Employee InsertEmployeeData(Employee employee)
        {
            string spName = "dbo.[AddingEmployeeDetails]";
            SqlConnection connection = new SqlConnection(connectionString);            
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(spName, connection);

                    cmd.CommandType = CommandType.StoredProcedure;
                   
                    cmd.Parameters.AddWithValue("@Name", employee.Name);
                    cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                    cmd.Parameters.AddWithValue("@BasicPay", employee.BasicPay);
                    cmd.Parameters.AddWithValue("@Department", employee.Department);
              
                    cmd.ExecuteNonQuery();
                    connection.Close();
                   
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return employee;
        }

        /// <summary>
        /// Gets the averagef salary of all employees.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="EmployeePlayrollException">no such sql procedure</exception>
        public static decimal GetAveragefSalary_OfAllEmployees()
        {
            decimal AverageSalary = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("AverageOfSalary_OfAllEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
               
                using (connection)
                {
                    connection.Open();
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        AverageSalary = rd.IsDBNull(0) ? default : rd.GetInt64(0);
                    }

                    return AverageSalary;
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("Could not find stored procedure"))
                {
                    throw new EmployeePlayrollException(EmployeePlayrollException.ExceptionType.NO_SUCH_SQL_PROCEDURE, "no such sql procedure");
                }
                try
                {
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace); ;
                }
            }
           
            return AverageSalary;
        }
        public static int GetNoOfFemaleEmployees()
        {
            int NumFemaleEmployees = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("dbo.Er_GetNoOf_FemaleEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                connection.Open();
                using (connection)
                {
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        NumFemaleEmployees = rd.IsDBNull(0) ? default : rd.GetInt32(0);
                    }
                    return NumFemaleEmployees;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return NumFemaleEmployees;
        }
        public static int GetNoOfMaleEmployees()
        {
            int NumMaleEmployees = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("dbo.Er_GetNoOf_MaleEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                connection.Open();
                using (connection)
                {
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        NumMaleEmployees = rd.IsDBNull(0) ? default : rd.GetInt32(0);
                    }
                    return NumMaleEmployees;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return NumMaleEmployees;
        }

        public static double GetMinOfSalary_OfFemaleEmployees()
        {
            double MinSalary = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("dbo.Er_GetMinOfSalary_OfFemaleEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                connection.Open();
                using (connection)
                {
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        MinSalary = rd.IsDBNull(0) ? default : rd.GetInt64(0);
                    }
                    return MinSalary;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return default;
        }
        public static double GetSumOfSalary_OfAllMaleEmployee()
        {
            double totalSalary = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("dbo.Er_GetSumOfSalary_OfAllMaleEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                connection.Open();
                using (connection)
                {
                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        totalSalary = rd.IsDBNull(0) ? default : rd.GetInt64(0);
                    }
                    return totalSalary;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return totalSalary;
        }

        /// <summary>
        /// Gets the sum of salary of all employee payroll data.
        /// </summary>
        /// <returns></returns>
        public static double GetSumOfSalary_OfAllFemaleEmployee()
        {
            double totalSalary = 0;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("dbo.Er_GetSumOfSalary_OfAllFemaleEmployees", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            try
            {
                using (connection)
                {
                    connection.Open();

                    SqlDataReader rd = command.ExecuteReader();
                    while (rd.Read())
                    {
                        totalSalary = rd.IsDBNull(0) ? default : rd.GetInt64(0);
                    }
                    return totalSalary;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return totalSalary;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("welcome to Employee Payroll Service");
            GetAllEmployeePayrollData();
        
            
            
        }
        
    }
}
