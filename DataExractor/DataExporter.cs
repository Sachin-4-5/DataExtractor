using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using OfficeOpenXml;    //Added EPPlus Nuget package for xml export


namespace DataExractor
{
    public class DataExporter
    {
        public static void Export(TemplateXmlReader config)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(config.ConnectionString))
                using (SqlCommand cmd = new SqlCommand(config.Query, conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    conn.Open();
                    adapter.Fill(table);
                    conn.Close();

                    if (config.OutputFormat == "CSV")
                        ExportToCsv(table, config.OutputPath);
                    else if (config.OutputFormat == "EXCEL")
                        ExportToExcel(table, config.OutputPath);
                    else
                        ErrorLogger.WriteErrorLog("DataExporter", "Export", "Unsupported format.", "ERROR", null);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteErrorLog("DataExporter", "Export", "Export failed.", "ERROR", ex);
            }
        }


        private static void ExportToCsv(DataTable table, string path)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.WriteLine(string.Join(",", table.Columns.Cast<DataColumn>().Select(c => c.ColumnName)));
                    foreach (DataRow row in table.Rows)
                        writer.WriteLine(string.Join(",", row.ItemArray.Select(f => f.ToString().Replace(",", " "))));
                }
                ErrorLogger.WriteErrorLog("DataExporter", "ExportToCsv", "CSV exported successfully.", "INFO", null);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteErrorLog("DataExporter", "ExportToCsv", "CSV export failed.", "ERROR", ex);
            }
        }



        private static void ExportToExcel(DataTable table, string path)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var sheet = package.Workbook.Worksheets.Add("Data");
                    sheet.Cells.LoadFromDataTable(table, true);
                    package.SaveAs(new FileInfo(path));
                }
                ErrorLogger.WriteErrorLog("DataExporter", "ExportToExcel", "Excel exported successfully.", "INFO", null);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteErrorLog("DataExporter", "ExportToExcel", "Excel export failed.", "ERROR", ex);
            }
        }
    }
}
