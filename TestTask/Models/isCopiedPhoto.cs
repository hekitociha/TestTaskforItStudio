namespace TestTask.Models
{
    public class isCopiedPhoto
    {
        public int Id { get; set; }
        public int photoId { get; set; }
        public string photoDescription { get; set; }
        public byte[] photoData { get; set; }
        public static byte[] GetPhoto(string filePath)
        {
            filePath = Directory.GetCurrentDirectory() + "/wwwroot/Photos/" + filePath;
            FileStream stream = new FileStream(
                filePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream);

            byte[] photo = reader.ReadBytes((int)stream.Length);

            reader.Close();
            stream.Close();

            return photo;
        }
    }
}
