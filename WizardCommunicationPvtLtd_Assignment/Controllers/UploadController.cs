using System.Web.Mvc;
using System;
using System.IO;

namespace WizardCommunicationPvtLtd_Assignment.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadExcel()
        {
            if (Request.Files.Count == 0)
            {
                TempData["ErrorMessage"] = "Please select a file to upload.";
                return RedirectToAction("Index");
            }

            var excelFile = Request.Files[0];
            if (excelFile == null || excelFile.ContentLength == 0)
            {
                TempData["ErrorMessage"] = "The selected file is empty.";
                return RedirectToAction("Index");
            }

            if (!System.IO.Path.GetExtension(excelFile.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "Only .xlsx files are supported.";
                return RedirectToAction("Index");
            }

            // Store the file temporarily
            var tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".xlsx");
            excelFile.SaveAs(tempFilePath);
            Session["UploadedFilePath"] = tempFilePath;

            return RedirectToAction("MapColumns", "Mapping");
        }
    }
}