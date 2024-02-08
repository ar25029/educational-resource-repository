using FileHandling.Models.Domain;
using FileHandling.Models.Domain.VideoModels;
using FileHandling.Models.DTO;
using FileHandling.Repository.Abstract.Images;
using FileHandling.Repository.Abstract.Videos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FileHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController(IProductVideoRepository productRepo, IVideoService fileService) : ControllerBase
    {
        private IVideoService _fileService = fileService;
        private IProductVideoRepository _productRepo = productRepo;
        [HttpPost]
        public IActionResult AddImage([FromForm] Video model)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }
            if (model.VideoFile != null)
            {
                var fileResult = _fileService.SaveVideo(model.VideoFile);
                if (fileResult.Item1 == 1)
                {
                    model.ResourceVideo = fileResult.Item2; // getting name of image
                }
                var productResult = _productRepo.AddVideo(model);
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


        [HttpDelete("productVideo/delete")]
        public IActionResult DeleteVideo(string videoName)
        {
            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Please pass the valid data";
                return Ok(status);
            }


            string Name = _productRepo.GetVideoName(videoName);


            if (videoName != null)
            {
                var fileResult = _fileService.DeleteVideo(Name);
                if (fileResult != true)
                {
                    status.StatusCode = 1;
                    status.Message = "Enter valid name";// getting name of image
                }

                var productResult = _productRepo.DeleteVideo(videoName);
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


        [HttpGet("get/video/{fileName}")]
        public IActionResult GetVideo(string fileName)
        {
            var status = new Status();
            string Name = _productRepo.GetVideoName(fileName);

            var result = _fileService.GetVideo(Name);

            if (result.Item1 == 1)
            {
                status.StatusCode = 1;
                status.Message = "Video Retrieved successfully";
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
