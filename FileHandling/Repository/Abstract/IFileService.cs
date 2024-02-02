namespace FileHandling.Repository.Abstract
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);

        public bool DeleteImage(string imageFileName);

        public Tuple<int, byte[], string> GetImage(string fileName);
    }
}
