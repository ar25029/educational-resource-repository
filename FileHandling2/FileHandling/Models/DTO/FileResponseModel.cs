namespace FileHandling.Models.DTO
{
    public class FileResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public string PdfName { get; set; }
        public byte[] PdfContent { get; set; }
        public string ContentType { get; set; }
    }
}
