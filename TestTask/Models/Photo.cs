using System.ComponentModel.DataAnnotations.Schema;

namespace TestTask.Models
{
    public class Photo
    {
        public Photo()
        {
            this.isCopied = false;
        }
        public int Id { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        public string? ImageSrc { get; set; }
        public DateTime uploadTime { get; set; }
        public bool isCopied { get; set; }

        public string getSrc(Photo photo)
        {
            string path = Directory.GetCurrentDirectory() + "/wwwroot/Photos";
            if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }
            string fileName = photo.Image.FileName;
            path = Path.Combine(path, fileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                this.Image.CopyTo(stream);
            }
            return fileName;
        }
    }
}
