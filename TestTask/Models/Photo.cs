using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Desription { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string ImageSrc { get; set; }
        public DateTime uploadTime { get; set; }

        public bool isCopied 
        {
            get { return isCopied; }
            set { this.isCopied = false; }
        }

        public string getSrc()
        {
            string path = Directory.GetCurrentDirectory() + "wwwroot/Photos";
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            FileInfo fileInfo = new FileInfo(Image.FileName);
            string fileName = Image.FileName;
            string src = path + fileName;
            using (var stream = new FileStream(src, FileMode.Create))
            {
                this.Image.CopyTo(stream);
            }
            return src;
        }
    }
}
