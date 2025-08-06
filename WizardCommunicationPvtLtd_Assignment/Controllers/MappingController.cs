using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WizardCommunicationPvtLtd_Assignment.Models;
using WizardCommunicationPvtLtd_Assignment.Services;

namespace WizardCommunicationPvtLtd_Assignment.Controllers
{
    public class MappingController : Controller
    {
        public ActionResult MapColumns()
        {
            var filePath = Session["UploadedFilePath"] as string;
            if (string.IsNullOrEmpty(filePath))
            {
                return RedirectToAction("Index", "Upload");
            }

            // Get Excel headers
            var headers = new List<string>();
            using (var package = new ExcelPackage(new System.IO.FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                {
                    headers.Add(worksheet.Cells[1, col].Text);
                }
            }

            // Get model properties
            var modelProperties = typeof(Customer).GetProperties()
                .Where(p => p.Name != "Id")
                .Select(p => p.Name)
                .ToList();

            ViewBag.Headers = headers;
            ViewBag.ModelProperties = modelProperties;

            return View();
        }

        [HttpPost]
        public ActionResult ProcessMapping(FormCollection form)
        {
            var filePath = Session["UploadedFilePath"] as string;
            if (string.IsNullOrEmpty(filePath))
            {
                return RedirectToAction("Index", "Upload");
            }

            // Process the mapping and save to database
            try
            {
                var mapping = new Dictionary<string, string>();
                foreach (var key in form.AllKeys)
                {
                    if (key.StartsWith("map_"))
                    {
                        var excelCol = key.Substring(4);
                        var dbField = form[key];
                        if (!string.IsNullOrEmpty(dbField))
                        {
                            mapping.Add(excelCol, dbField);
                        }
                    }
                }

                // Process the Excel file with the mapping
                var service = new ExcelProcessingService();
                var result = service.ImportCustomers(filePath, mapping);

                TempData["SuccessMessage"] = $"Successfully imported {result} customers!";
                return RedirectToAction("Index", "Upload");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error during import: {ex.Message}";
                return RedirectToAction("MapColumns");
            }
        }
    }
}