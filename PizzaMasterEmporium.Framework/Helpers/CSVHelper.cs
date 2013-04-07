using System;
using System.Data;
using System.IO;

namespace PizzaMasterEmporium.Framework.Helpers
{
    public class CSVHelper
    {
        public static DataTable Import(String file, Boolean hasColumnHeadings, Char delimiter)
        {
            if (String.IsNullOrEmpty(file))
                throw new ArgumentException("File parameter is null or empty");

            if (!File.Exists(file))
                throw new FileNotFoundException("Cannot find the file specified");

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Delete);
            DataTable dataTable = new DataTable();
            String row = String.Empty;

            StreamReader streamReader = new StreamReader(fileStream);
            
            row = streamReader.ReadLine();

            if (hasColumnHeadings)
            {
                if (!String.IsNullOrEmpty(row))
                {
                    String[] columnHeadings = row.Split(new[] {delimiter}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String column in columnHeadings)
                        dataTable.Columns.Add(new DataColumn(CleanupInput(column)));
                }

                row = streamReader.ReadLine();
            }
            else
            {
                String[] values = row.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < values.Length; i++)
                    dataTable.Columns.Add(new DataColumn(String.Format("Column{0}", i)));
            }

            while (row != null)
            {
                if (!String.IsNullOrEmpty(row))
                {
                    String[] data = row.Split(new[] {delimiter}, StringSplitOptions.None);

                    if (data.Length > dataTable.Columns.Count)
                        throw new Exception("Too many columns in row");

                    Object[] record = new Object[dataTable.Columns.Count];
                    for (int i = 1; i < dataTable.Columns.Count + 1; i++)
                        record[i - 1] = CleanupInput(data[i - 1]);

                    dataTable.Rows.Add(record);
                }

                row = streamReader.ReadLine();
            }

            return dataTable;
        }

        public static void Create(String path, String file, String[] headers, DataTable data)
        {
            ValidateInput(path, file);

            if (data == null)
                throw new ArgumentException("Data parameter is null");

            String fileToCreate = String.Format("{0}{1}", path.EndsWith("\\") ? path : String.Format("{0}\\", path), file);

            StreamWriter streamWriter = null;

            try
            {
                streamWriter = new StreamWriter(fileToCreate);

                if (headers != null || headers.Length > 0)
                    streamWriter.WriteLine(String.Join(",", headers));

                foreach (DataRow row in data.Rows)
                {
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        if (i < data.Columns.Count - 1)
                        {
                            if (row[i] is DateTime)
                                streamWriter.Write(String.Format("{0},", DateTime.Parse(row[i].ToString()).ToString("s")));
                            else
                                streamWriter.Write(String.Format("{0},", row[i]));
                        }
                        else
                            streamWriter.Write(row[i].ToString());
                    }

                    streamWriter.Write(Environment.NewLine);
                }
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        public static void Append(String path, String file, String[] data)
        {
            ValidateInput(path, file);

            if (data == null)
                throw new ArgumentException("Data parameter is null");

            String fileToAppend = String.Format("{0}{1}", path.EndsWith("\\") ? path : String.Format("{0}\\", path), file);

            if (!File.Exists(fileToAppend))
                throw new FileNotFoundException("File Not Found");

            StreamWriter streamWriter = null;
            try
            {
                streamWriter = new StreamWriter(fileToAppend, true);

                streamWriter.WriteLine(String.Join(",", data));
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        #region Helper Methods

        private static void ValidateInput(String path, String file)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentException("Path parameter is null or empty");

            if (!Directory.Exists(path.EndsWith("\\") ? path : String.Format("{0}\\", path)))
                throw new DirectoryNotFoundException("Unable to find directory for path supplied");

            if (String.IsNullOrEmpty(file))
                throw new ArgumentException("File parameter is null or empty");
        }

        private static String CleanupInput(String input)
        {
            String cleanedInput = input;

            while (cleanedInput.Contains("\""))
            {
                cleanedInput = cleanedInput.Replace("\"", String.Empty);
            }

            return cleanedInput.Trim();
        }

        #endregion
    }
}
