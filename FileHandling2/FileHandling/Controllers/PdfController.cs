using FileHandling.Models.Domain.Pdf;
using FileHandling.Models.DTO;
using FileHandling.Repository.Abstract.Images;
using FileHandling.Repository.Abstract.Pdfs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController(IProductPdfRepository productPdfRepo, IPdfService pdfService) : ControllerBase
    {
        private IPdfService _fileService = pdfService;
        private IProductPdfRepository _productRepo = productPdfRepo;

        [HttpPost]
        public IActionResult AddPdf([FromForm] Pdf model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.PdfFile != null)
            {
                var fileResult = _fileService.SavePdf(model.PdfFile);
                if (fileResult.Item1 == 1)
                {
                    model.ResourcePdf = fileResult.Item2; // getting name of image
                }
                var productResult = _productRepo.AddPdf(model);
                if (productResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Added successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on adding product";

                }
            }
            return Ok(status);

        }


        [HttpDelete("productPdf/delete")]
        public IActionResult DeletePdf(string pdfName)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }


            string Name = _productRepo.GetPdfName(pdfName);


            if (pdfName != null)
            {
                var fileResult = _fileService.DeletePdf(Name);
                if (fileResult != true)
                {
                    status.StatusCode = 1;
                    status.Message = "Enter valid name";// getting name of image
                }

                var productResult = _productRepo.DeletePdf(pdfName);
                if (productResult)
                {
                    status.StatusCode = 1;
                    status.Message = "Deleted successfully";
                }
                else
                {
                    status.StatusCode = 0;
                    status.Message = "Error on Deleting product";

                }
            }
            return Ok(status);
        }


        [HttpGet("get/pdf/{fileName}")]
        public IActionResult GetPdf(string fileName)
        {
            var status = new Status();
            Pdf pdf = _productRepo.GetPdf(fileName);
            if (pdf != null)
            {
                var result = _fileService.GetPdf(pdf.ResourcePdf);

                if(result.Item1 == 1)
                {
                    var pdfResult = new FileResponseModel
                    {
                        StatusCode = 1,
                        Message = "Pdf Retrieved Successfully",
                        PdfName = pdf.ResourceName,
                        PdfContent = result.Item2,
                        ContentType = result.Item3
                    };
                    return Ok(pdfResult);
                }
            }

            status.StatusCode = 0;
            status.Message = "Error on showing Product";
            return NotFound(status);
            //if (result.Item1 == 1)
            //{
            //    status.StatusCode = 1;
            //    status.Message = "ImageRetrieved successfully";
            //    return File(result.Item2, result.Item3); // Return the file content with the appropriate content type

            //}
            
            //else
            //{
            //    status.StatusCode = 0;
            //    status.Message = "Error on Showing product";
            //    return NotFound(); // File not found or an error occurred
            //}
            }


        [HttpGet("get/all/pdfs")]
        public async Task<IActionResult> GetAllPdfs()
        {
            var status = new Status();
            var pdfs = _productRepo.GetAllPdfs();

            if (pdfs != null && pdfs.Any())
            {
                var pdfResults = new List<FileResponseModel>();

                foreach (var pdf in pdfs)
                {
                    var result = _fileService.GetPdf(pdf.ResourcePdf);

                    if (result.Item1 == 1)
                    {
                        var pdfResult = new FileResponseModel
                        {
                            StatusCode = 1,
                            Message = "Pdf Retrieved Successfully",
                            PdfName = pdf.ResourceName,
                            Standard = pdf.Standard,
                            PdfContent = result.Item2,
                            ContentType = result.Item3
                        };
                        pdfResults.Add(pdfResult);
                    }
                }

                return Ok(pdfResults);
            }

            status.StatusCode = 0;
            status.Message = "No PDFs found";
            return NotFound(status);
        }


        [HttpPost("Pdf/Standard/{std}")]
        public async Task<IActionResult> GetAllPdfs(int std)
        {
            var status = new Status();
            var pdfs = _productRepo.GetPdfByStandard(std);

            if (pdfs != null && pdfs.Any())
            {
                var pdfResults = new List<FileResponseModel>();

                foreach (var pdf in pdfs)
                {
                    var result = _fileService.GetPdf(pdf.ResourcePdf);

                    if (result.Item1 == 1)
                    {
                        var pdfResult = new FileResponseModel
                        {
                            StatusCode = 1,
                            Message = "Pdf Retrieved Successfully",
                            PdfName = pdf.ResourceName,
                            PdfContent = result.Item2,
                            Standard = pdf.Standard,
                            ContentType = result.Item3
                        };
                        pdfResults.Add(pdfResult);
                    }
                }

                return Ok(pdfResults);
            }

            status.StatusCode = 0;
            status.Message = "No PDFs found";
            return NotFound(status);
        }
    }
}
