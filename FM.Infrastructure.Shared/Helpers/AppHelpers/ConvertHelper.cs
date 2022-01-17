using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using FM.Application.Exceptions;

namespace FM.Infrastructure.Shared.Helpers.AppHelpers
{
    public class ConvertHelper
    {
        public static DataTable ConvertToDataTable<T>(List<T> models)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // Loop through all the properties            
            // Adding Column to our datatable
            foreach (PropertyInfo prop in props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }

            // Adding Row
            foreach (T item in models)
            {
                var values = new object[props.Length];
                for (int i = 0; i < props.Length; i++)
                {
                    //inserting property values to datatable rows  
                    values[i] = props[i].GetValue(item, null);
                }
                // Finally add value to datatable  
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public static DateTime? TryConvertStringToDateTime(string input)
        {
            if (String.IsNullOrEmpty(input)) return null;

            if (input.Length == 5)
            {
                input += "/1904";
            }

            return Convert.ToDateTime(input);
        }

        public static int TryConvertStringToInt(string input)
        {
            if (String.IsNullOrEmpty(input)) return 0;

            return Convert.ToInt32(input);
        }

        public static long TryConvertStringToLong(string input)
        {
            if (String.IsNullOrEmpty(input)) return 0;

            return Convert.ToInt64(input);
        }

        public static int RegexFindAndConvertToInt(string input)
        {
            try
            {
                return Int32.Parse(Regex.Match(input, @"\d+").Value);
            }
            catch (Exception e)
            {
                throw new ExceptionCustom("RegexFindAndConvertToInt", input, e.Message, e);

            }
        }

        public static bool? TryConvertStringToBoolean(string input)
        {
            if (String.IsNullOrEmpty(input)) return null;

            return Convert.ToBoolean(input);
        }
    }
}