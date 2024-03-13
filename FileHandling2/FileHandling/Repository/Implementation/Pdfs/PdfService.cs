﻿using FileHandling.Models.Domain.Pdf;
using FileHandling.Repository.Abstract.Pdfs;

namespace FileHandling.Repository.Implementation.Pdfs
{
    public class PdfService : IPdfService
    {
        public IWebHostEnvironment _environment;
        public PdfService(IWebHostEnvironment environment)
        {

            _environment = environment;

        }
        public  Tuple<int, string> SavePdf(IFormFile imageFile)
        {
            try
            {
                var contentPath = _environment.ContentRootPath;
                var path = Path.Combine(contentPath, "uploads");
                var ext = Path.GetExtension(imageFile.FileName);


                // Create a subfolder for PDFs if the file is a PDF or DOC
                if (ext == ".pdf" || ext == ".doc")
                {
                    path = Path.Combine(contentPath, "uploads\\Pdf's");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                else if (ext == ".mp4" || ext == ".mpeg")
                {
                    path = Path.Combine(contentPath, "uploads\\Videos");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }


                //Check the allowed extensions
                var allowedExtensions = new string[] {".pdf", ".doc"};
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }

                // Generate a unique filename
                string uniqueString = Guid.NewGuid().ToString();
                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);

                // Save the file
                
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, ex.Message);
            }

        }

        //public bool DeletePdf(string pdfFileName)
        //{
        //    try
        //    {
        //        var wwwPath = this._environment.ContentRootPath;
        //        var path = Path.Combine(wwwPath, "uploads\\Pdf's", pdfFileName);
        //        if (File.Exists(path))
        //        {

        //            File.Delete(path);
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}




        public Tuple<int, byte[], string> GetPdf(string fileName)
        {


            try
            {
               
                var wwwPath = this._environment.ContentRootPath;
                var ext = Path.GetExtension(fileName);
                var path = Path.Combine(wwwPath, "uploads\\Pdf's", fileName);

                if (ext == ".pdf" || ext == ".doc")
                {
                    path = Path.Combine(wwwPath, "uploads\\Pdf's", fileName);

                } else if (ext == ".mp4" || ext == ".mpeg")
                {
                    path = Path.Combine(wwwPath, "uploads\\Videos", fileName);

                }

                //if(ext == '.mp4' )
                if (File.Exists(path))
                {
                    var fileBytes = System.IO.File.ReadAllBytes(path);

                    // Determine the content type based on the file extension
                    string contentType;
                    switch (Path.GetExtension(fileName).ToLower().Trim())
                    {
                        case ".pdf":
                            contentType = "application/pdf";
                            break;
                        case ".doc":
                            contentType = "application/doc";
                            break;
                           // For checking if category is video
                        case ".mp4":
                            contentType = "video/mp4";
                            break;
                        case ".mpeg":
                            contentType = "video/mpeg";
                            break;

                        default:
                            contentType = "application/octet-stream";
                            break;
                    }

                    return new Tuple<int, byte[], string>(1, fileBytes, contentType);
                }
                else
                {
                    return new Tuple<int, byte[], string>(0, null, null); // File not found
                }
            }
            catch (Exception ex)
            {
                return new Tuple<int, byte[], string>(0, null, null); // Return null in case of an exception
            }
        }

    }
}
