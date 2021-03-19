using System;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace TimeManager.Web.Exceptions
{
    public class PostgreCustomException : Exception
    {
        public string EntityName { get; }

        public PostgreCustomException(string message, Exception inner, string entityName)
            : base(message, inner)
        {
            EntityName = entityName;
        }

        public static string Handle(Exception exception)
        {
            if (exception.InnerException is PostgresException)
            {
                var postgresEx = (PostgresException) exception.InnerException;
                //return postgresEx.Code + " " + postgresEx.ColumnName;
                switch (postgresEx.Code)
                {
                    case "23505"://	unique_violation
                        return "Не уникальное значение поля " + GetColumnNameFromConstraintName(postgresEx.ConstraintName);
                    case "23502": // not_null_violation
                        return "Поле не может быть пустым " + postgresEx.ColumnName;
                }
            }
            return exception.Message;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraintName">constraintName = "IX_TableName_ColumnName"</param>
        /// <returns>column name</returns>
        public static string GetColumnNameFromConstraintName(string constraintName)
        {
            var temp = constraintName.Split('_');
            var result = temp[temp.Length - 1];
            return result;
        }
      /*  public static string HandleException(public virtual void HandleException(Exception exception)
{
  if (exception is DbUpdateConcurrencyException concurrencyEx)
  {
    // A custom exception of yours for concurrency issues
    throw new ConcurrencyException();
  }
  else if (exception is DbUpdateException dbUpdateEx)
  {
    if (dbUpdateEx.InnerException != null
            && dbUpdateEx.InnerException.InnerException != null)
    {
      if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
      {
        switch (sqlException.Number)
        {
          case 2627:  // Unique constraint error
          case 547:   // Constraint check violation
          case 2601:  // Duplicated key row error
                      // Constraint violation exception
            // A custom exception of yours for concurrency issues
            throw new ConcurrencyException();
          default:
            // A custom exception of yours for other DB issues
            throw new DatabaseAccessException(
              dbUpdateEx.Message, dbUpdateEx.InnerException);
        }
      }

      throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
    }
  }

  // If we're here then no exception has been thrown
  // So add another piece of code below for other exceptions not yet handled...
} exception)
        {
            if (exception is DbUpdateException dbUpdateEx)
            {
                if (dbUpdateEx.InnerException != null
                    && dbUpdateEx.InnerException.InnerException != null)
                {
                    if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627: // Unique constraint error
                            case 547: // Constraint check violation
                            case 2601: // Duplicated key row error
                                // Constraint violation exception
                                // A custom exception of yours for concurrency issues
                                throw new ConcurrencyException();
                            default:
                                // A custom exception of yours for other DB issues
                                throw new DatabaseAccessException(
                                    dbUpdateEx.Message, dbUpdateEx.InnerException);
                        }
                    }

                    throw new DatabaseAccessException(dbUpdateEx.Message, dbUpdateEx.InnerException);
                }
            }

            // If we're here then no exception has been thrown
            // So add another piece of code below for other exceptions not yet handled...
        }*/
    }
}