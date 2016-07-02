namespace TelerikMvcApp1.Models
{
    public class UploadObject
    {
        public int Id { set; get; }
        public string FileUrl { set; get; }
        public string ImageUrl { set; get; }

        public UploadObject()
        {
            Id = 1;
            FileUrl = "/Uploads/Documents/test_2.docx";
            ImageUrl = "/Uploads/Images/test.png";
        }
    }
}