## DataExtractor

### 📘 Overview  
DataExtractor is a lightweight, extensible .NET Framework 4.7.2 console application designed to extract data from SQL Server databases using dynamic connection and query configuration defined in an external XML template. It exports the result into CSV or Excel files and logs the process for auditing and debugging purposes.

---
<br />



### 🚀 Features  
✅ Dynamic Configuration via XML: DB conn, query, o/p format, and path are defined in template.xml. <br />
✅ Data Export Support: CSV and Excel (via EPPlus Library). <br />
✅ Logs info/debug/error messages to a configurable file path (via app.config). <br />
✅ Secure and Configurable: No hardcoded credentials, externalized configuration. <br />
✅ Command Line Integration: Accepts XML template path as a runtime argument. <br />

---
<br />



### 📁 Project Structure
```
DataExtractor/
│── bin\
├── obj\
├── Properties\
├── Templates\template.xml
├── Program.cs         
├── TemplateXmlReader.cs       
├── DataExporter.cs          
├── ErrorLogger.cs
├── App.config
```

---
<br />




### 💡 Future Enhancements
1️⃣ Support for parameterized queries. <br />
2️⃣ Encrypted connection strings (via key or certificate). <br />
3️⃣ Unit testing framework (e.g., MSTest/NUnit integration). <br />
4️⃣ SFTP file upload after export. <br />
5️⃣ Email notifications after job completion. <br />
6️⃣ Integration with Windows Task Scheduler for automation. <br />
7️⃣ Support for more output formats (JSON, XML, PDF). <br />

---
<br />



### 🛠️ How to Run
1️⃣ Clone the project in your local. <br />
2️⃣ Pre-requisite : .NET Framework v4.7.2 and Nuget package (EPPlus) <br />
3️⃣ Command line usage : DataExtractor.exe <template.xml_path>. <br />

---
<br />



### 🤝 Contribution
Pull requests are welcome! To contribute:

1️⃣ Fork the repo <br />
2️⃣ Create a feature branch (git checkout -b feature-xyz) <br />
3️⃣ Commit changes (git commit -m "Added feature xyz") <br />
4️⃣ Push to your branch (git push origin feature-xyz) <br />
5️⃣ Create a pull request 

---
<br />
<br />



















