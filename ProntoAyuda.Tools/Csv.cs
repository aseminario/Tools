using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Reflection;

namespace ProntoAyuda.Tools
{
    public class Csv
    {

        #region CSV from objects

        
        public static System.IO.MemoryStream CreateCsvAsStream(object[] data, ColumnDescriptor[] properties)
        {
            var template = CreateTemplate(properties);
            return CreateCsvAsStream(data, properties, template, true);
        }
        private static System.IO.MemoryStream CreateCsvAsStream(object[] data, ColumnDescriptor[] properties, string template, bool duplicateQuotes, Encoding encoding = null)
        {

            var (propertyNames, columnHeadings) = GetSourcePropertyNamesAndColumnHeadings(properties);


            var vals = new List<List<object>>();

            if (data.Any())
            {
                Type t = data[0].GetType();

                var props = t.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Instance);

                foreach (var d in data)
                {
                    vals.Add(ExtractValuesOfObject(d, props, propertyNames, duplicateQuotes));
                }
            }

            return CreateStream(vals, template, columnHeadings, encoding);
        }

        #endregion

        #region CSV from DataTable

        public static System.IO.MemoryStream GetStreamFromTableForCsv(DataTable data, ColumnDescriptor[] properties, bool duplicateQuotesOfStringValues)
        {
            var template = CreateTemplate(properties);

            var (propertyNames, columnHeadings) = GetSourcePropertyNamesAndColumnHeadings(properties);



            var vals = new List<List<object>>();


            foreach (DataRow d in data.Rows)
            {
                var recordVals = new SortedDictionary<int, object>();
                foreach (var prop in propertyNames)
                {
                    var objValue = d.IsNull(prop) ? string.Empty : d[prop];
                    objValue = DuplicateQuotesIfString(objValue, duplicateQuotesOfStringValues);
                    recordVals.Add(propertyNames.IndexOf(prop), objValue);
                }
                vals.Add(recordVals.Values.ToList());
            }
            

            return CreateStream(vals, template, columnHeadings.ToArray());
        }

        #endregion

        
        private static System.IO.MemoryStream CreateStream(List<List<object>> t, string template, IEnumerable<string> columnNames,
            System.Text.Encoding encoding = null)
        {
            var ms = new System.IO.MemoryStream();
            var tw = encoding == null ? new System.IO.StreamWriter(ms) : new System.IO.StreamWriter(ms, encoding);

            tw.WriteLine(template, columnNames);
            //tw.AutoFlush = true;
            
            foreach (var r in t)
            {
                tw.WriteLine(string.Format(template, r.ToArray()));
            }
            
            tw.Flush();
            
            ms.Position = 0;
            return ms;
        }

        private static string CreateTemplate(ColumnDescriptor[] properties)
        {
            var template = string.Empty;
            for (var i = 0; i < properties.Length; i++)
            {
                if (template.Length > 0) template += ",";
                template += $"\"{i}\"";
            }
            return template;
        }

        private static List<object> ExtractValuesOfObject(object d, PropertyInfo[] props, List<string> sourcePropertyNames, bool duplicateQuotesOfStringValues)
        {
            var recordVals = new SortedDictionary<int, object>();
            foreach (var prop in props)
            {
                if (sourcePropertyNames.Contains(prop.Name))
                {
                    var objVal = prop.GetValue(d, null);
                    objVal = DuplicateQuotesIfString(objVal, duplicateQuotesOfStringValues);
                    recordVals.Add(sourcePropertyNames.IndexOf(prop.Name), objVal);
                }
            }
            return recordVals.Values.ToList();
        }

        private static object DuplicateQuotesIfString(object value, bool duplicateQuotesOfStringValues)
        {
            if (!duplicateQuotesOfStringValues)
            {
                return value;
            }

            if (!(value is string strVal))
            {
                return value;
            }

            return DuplicateQuotes(strVal);
        }
        private static string DuplicateQuotes(string val)
        {
            return val.Replace("\"", "\"\"");
        }

        private static Tuple<List<string>, List<string>> GetSourcePropertyNamesAndColumnHeadings(ColumnDescriptor[] columns)
        {
            var propertyNames = new List<string>();
            var columnHeadings = new List<string>();
            foreach (var p in columns)
            {
                propertyNames.Add(p.SourcePropertyName);
                columnHeadings.Add(p.ColumnHeading);
            }

            return Tuple.Create(propertyNames, columnHeadings);
        }

        public class ColumnDescriptor
        {
            public ColumnDescriptor(string sourcePropertyName, string columnHeading)
            {
                this.SourcePropertyName = sourcePropertyName;
                this.ColumnHeading = columnHeading;
            }
            public ColumnDescriptor(string sourcePropertyName)
            {
                this.SourcePropertyName = sourcePropertyName;
                this.ColumnHeading = sourcePropertyName;
            }

            public string SourcePropertyName { get; set; }
            public string ColumnHeading { get; set; }
        }
    }
}
