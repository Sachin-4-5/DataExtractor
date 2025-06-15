using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExractor
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    args = new string[] { @"D:\Projects\Console Application - 4\DataExractor\Templates\template.xml" };
                }

                string xmlPath = args[0];   //reading command line argument.
                TemplateXmlReader config = TemplateXmlReader.LoadFromXml(xmlPath);

                ErrorLogger.WriteErrorLog("Program", "Main", "Starting export.", "INFO", null);
                DataExporter.Export(config);
                ErrorLogger.WriteErrorLog("Program", "Main", "Export complete.", "INFO", null);
            }
            catch (Exception ex)
            {
                ErrorLogger.WriteErrorLog("Program", "Main", "Fatal error.", "ERROR", ex);
            }
        }
    }
}
