using OfficeOpenXml;
using System;
using System.Collections.Generic;
using WizardCommunicationPvtLtd_Assignment.Models;

namespace WizardCommunicationPvtLtd_Assignment.Services
{
    public class ExcelProcessingService
    {
        public int ImportCustomers(string filePath, Dictionary<string, string> columnMappings)
        {
            var importedCount = 0;

            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            using (var db = new CustomerDbContext())
            {
                var worksheet = package.Workbook.Worksheets[1];
                var rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Skip header row
                {
                    var customer = new Customer();
                    bool hasData = false;

                    foreach (var mapping in columnMappings)
                    {
                        var excelCol = GetExcelColumnByName(worksheet, mapping.Key);
                        if (excelCol > 0)
                        {
                            var value = worksheet.Cells[row, excelCol].Text;
                            if (!string.IsNullOrEmpty(value))
                            {
                                var property = typeof(Customer).GetProperty(mapping.Value);
                                if (property != null)
                                {
                                    try
                                    {
                                        property.SetValue(customer, Convert.ChangeType(value, property.PropertyType));
                                        hasData = true;
                                    }
                                    catch { /* Handle conversion errors */ }
                                }
                            }
                        }
                    }
                    try
                    {
                        if (hasData)
                        {
                            db.Customer.Add(customer);
                            importedCount++;
                        }
                    }
                    catch(Exception ex)
                    {
                        // Log the error (optional)
                        Console.WriteLine($"Error adding customer at row {row}: {ex.Message}");
                    }
                }

                db.SaveChanges();
            }

            return importedCount;
        }

        private int GetExcelColumnByName(ExcelWorksheet worksheet, string columnName)
        {
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                if (worksheet.Cells[1, col].Text.Equals(columnName, StringComparison.OrdinalIgnoreCase))
                    return col;
            }
            return -1;
        }
    }
}