namespace Anyar.Helper
{
    public static class FileManager
    {
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType == "image/jpeg" || file.ContentType == "image/png")
            {
                return true;
            }
            return false;
        }

        public static bool CheckFileSize(IFormFile file)
        {
            if (file.Length < 3145728)
            {
                return true;
            }
            return false;
        }

        public static string CheckAndEditName(IFormFile file)
        {
            string name = file.FileName;

            if (name.Length > 64)
            {
                name = name.Substring(name.Length - 64);
            }

            name = Guid.NewGuid().ToString() + name;
            return name;
        }


        public static void SaveFile(IFormFile file, string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }
        }

        public static void Delete(string path)
        {
            File.Delete(path);
        }
    }
}
