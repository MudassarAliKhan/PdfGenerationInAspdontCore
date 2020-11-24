using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PdfGenerationInAspdontCore.Models;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Drawing;
using System.IO;
using Syncfusion.Pdf.Parsing;
using Syncfusion.HtmlConverter;
using Microsoft.AspNetCore.Hosting;
//using HTMLtoPDFSample.Models;

namespace PdfGenerationInAspdontCore.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        private readonly IHostingEnvironment _hostingEnvironment;
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;

        //}
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View();
            //CreateDocument();
        }
        [HttpPost]
        public IActionResult ExportToPDF(String submit)
        {
            HtmlToPdfConverter htmlConverter = new HtmlToPdfConverter();
            WebKitConverterSettings settings = new WebKitConverterSettings();
            settings.WebKitPath = Path.Combine(_hostingEnvironment.ContentRootPath, "QtBinariesWindows");
            htmlConverter.ConverterSettings = settings;
            PdfDocument document = htmlConverter.Convert("https://lms.kpcerc.com/");
            document.PageSettings.Margins.Left = 10;
            document.PageSettings.Margins.Right = 10;
            document.PageSettings.Margins.Top = 20;
            document.PageSettings.Margins.Bottom = 20;
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            document.Close(true);
            stream.Position = 0;

            FileStreamResult fileStreamResult = new FileStreamResult(stream,"application/pdf");
            fileStreamResult.FileDownloadName = "Qouation.pdf";
            return fileStreamResult;

            ////Create a new PDF document
            //PdfDocument document = new PdfDocument();

            ////Add a page to the document
            //PdfPage page = document.Pages.Add();

            ////Create PDF graphics for the page
            //PdfGraphics graphics = page.Graphics;

            ////Set the standard font
            //PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);
            ////Draw the text
            //graphics.DrawString("Hello World!!!", font, PdfBrushes.Black, new PointF(0, 0));
            ////Saving the PDF to the MemoryStream
            // MemoryStream stream = new MemoryStream();
            // document.Save(stream);
            ////If the position is not set to '0' then the PDF will be empty.
            // stream.Position = 0;
            ////Download the PDF document in the browser.
            // FileStreamResult fileStreamResult = new FileStreamResult(stream, "application/pdf");
            // fileStreamResult.FileDownloadName = "Output.pdf";
            // return fileStreamResult;

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
