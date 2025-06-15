using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataExractor
{
    public class TemplateXmlReader
    {
        public string ConnectionString { get; set; }
        public string Query { get; set; }
        public string OutputFormat { get; set; }    // CSV or EXCEL
        public string OutputPath { get; set; }


        public static TemplateXmlReader LoadFromXml(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            var db = doc.Root.Element("Database");
            var output = doc.Root.Element("Output");

            return new TemplateXmlReader
            {
                ConnectionString = db.Element("ConnectionString").Value.Trim(),
                Query = doc.Root.Element("Query").Value.Trim(),
                OutputFormat = output.Element("Format").Value.Trim().ToUpper(),
                OutputPath = output.Element("Path").Value.Trim()
            };
        }
    }
}
