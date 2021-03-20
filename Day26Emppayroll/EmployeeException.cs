using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeePayroll
{
    public class EmployeePlayrollException : Exception
    {
        /// <summary>
        /// custom Exception Types
        /// </summary>
        public enum ExceptionType
        {
            NO_DATA_FOUND,
            INSERTION_ERROR,
            NO_SUCH_SQL_PROCEDURE,
            CONNECTION_FAILED
        }
        ExceptionType exceptionType;
        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeePlayrollException"/> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="message">The message.</param>
        public EmployeePlayrollException(ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }
}
