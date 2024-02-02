using FileHandling.Models.Domain;
using FileHandling.Models.DTO;
using FileHandling.Repository.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace FileHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductRepository productRepo, IFileService fileService) : ControllerBase
    {
        private IFileService _fileService = fileService;
        private IProductRepository _productRepo = productRepo;



        [HttpPost]
        public IActionResult Add([FromForm] Product model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if (fileResult.Item1 == 1)
                {
                    model.ProductImage = fileResult.Item2; // getting name of image
                }
                var productResult = _productRepo.Add(model);
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


        [HttpDelete("product/delete")]
        public IActionResult DeleteFile(string imageName)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }


            string Name = _productRepo.GetName(imageName);


            if (imageName != null)
            {
                var fileResult = _fileService.DeleteImage(Name);
                if (fileResult != true)
                {
                    status.StatusCode = 1;
                    status.Message = "Enter valid name";// getting name of image
                }

                var productResult = _productRepo.Delete(imageName);
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


        [HttpGet("get/{fileName}")]
        public IActionResult GetImage(string fileName)
        {
            var status = new Status();
            string Name = _productRepo.GetName(fileName);

            var result = _fileService.GetImage(Name);

            if (result.Item1 == 1)
            {
                status.StatusCode = 1;
                status.Message = "ImageRetrieved successfully";
                return File(result.Item2, result.Item3); // Return the file content with the appropriate content type
                
            }
            else
            {
                status.StatusCode = 0;
                status.Message = "Error on Showing product";
                return NotFound(); // File not found or an error occurred
            }
        }
    }
}
