using FileHandling.Repository.Abstract.Images;

namespace FileHandling.Repository.Implementation.Images
{
    public class ImageService : IImageService
    {
        public IWebHostEnvironment _environment;
        public ImageService(IWebHostEnvironment environment)
        {

            _environment = environment;

        }


        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = _environment.ContentRootPath;
                //path = "c://Projects/Productminiapi/uploads , Something like that

                var path = Path.Combine(contentPath, "uploads");
                var ext = Path.GetExtension(imageFile.FileName);
                if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                {
                    path = Path.Combine(contentPath, "uploads\\Images");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                //else if (ext == ".pdf" || ext == ".doc")
                //{
                //    path = Path.Combine(contentPath, "uploads\\Pdf's");
                //    if (!Directory.Exists(path))
                //    {
                //        Directory.CreateDirectory(path);
                //    }
                //}


                //Check the allowed extensions

                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg", ".gif "/*,".pdf", ".doc", ".mp4" ,".mpeg"*/};
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }

                string uniqueString = Guid.NewGuid().ToString();

                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                //var fileWithPath = Path.Combine(path, imageFile.FileName);
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

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = _environment.ContentRootPath;
                var path = Path.Combine(wwwPath, "uploads\\Images", imageFileName);
                if (File.Exists(path))
                {

                    File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



       



        public Tuple<int, byte[], string> GetImage(string fileName)
        {


            try
            {
                var wwwPath = _environment.ContentRootPath;



                var path = Path.Combine(wwwPath, "uploads\\Images", fileName);

                if (File.Exists(path))
                {
                    var fileBytes = File.ReadAllBytes(path);

                    // Determine the content type based on the file extension
                    string contentType;
                    switch (Path.GetExtension(fileName).ToLower().Trim())
                    {
                        case ".jpg":
                        case ".jpeg":
                            contentType = "image/jpeg";
                            break;
                        case ".png":
                            contentType = "image/png";
                            break;
                        case ".gif":
                            contentType = "image/gif";
                            break;
                        case ".bmp":
                            contentType = "image/bmp";
                            break;
                        case ".svg":
                            contentType = "image/svg+xml";
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





       


        //public List<Tuple<int, byte[], string>> GetAllImages()
        //{
        //    List<Tuple<int, byte[], string>> imagesList = new List<Tuple<int, byte[], string>>();
        //    try
        //    {
        //        var wwwPath = this._environment.ContentRootPath;

        //        var directoryPath = Path.Combine(wwwPath, "uploads\\Images");

        //        if (Directory.Exists(directoryPath))
        //        {
        //            var imageFiles = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories)
        //                                      .Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg") || s.EndsWith(".png") || s.EndsWith(".gif") || s.EndsWith(".bmp") || s.EndsWith(".svg"))
        //                                      .ToList();

        //            foreach (var imagePath in imageFiles)
        //            {
        //                var fileName = Path.GetFileName(imagePath);
        //                var fileBytes = System.IO.File.ReadAllBytes(imagePath);

        //                // Determine the content type based on the file extension
        //                string contentType;
        //                switch (Path.GetExtension(fileName).ToLower().Trim())
        //                {
        //                    case ".jpg":
        //                    case ".jpeg":
        //                        contentType = "image/jpeg";
        //                        break;
        //                    case ".png":
        //                        contentType = "image/png";
        //                        break;
        //                    case ".gif":
        //                        contentType = "image/gif";
        //                        break;
        //                    case ".bmp":
        //                        contentType = "image/bmp";
        //                        break;
        //                    case ".svg":
        //                        contentType = "image/svg+xml";
        //                        break;
        //                    default:
        //                        contentType = "application/octet-stream";
        //                        break;
        //                }

        //                imagesList.Add(new Tuple<int, byte[], string>(1, fileBytes, contentType));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log or handle the exception as required
        //    }
        //    return imagesList;
        //}
    }

}
