﻿using System;
using Npgsql;

namespace TimeManager.Web.Exceptions
{
    public class PostgreCustomExceptionHandler
    {
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
            return exception.InnerException.Message ?? exception.Message;
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
    }
}