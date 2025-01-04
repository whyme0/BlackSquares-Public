using BlackSquares.DataCleaners;

namespace BlackSquares.Managers
{
    public class ImageFileManager : FileManager
    {
        // Difference between ImageFullPath and _imageUploadPath is that first contains _imageUploadPath + _imageName
        private readonly string _imageUploadPath;
        private readonly string _imageName;
        private readonly IFormFile? _image;
        private readonly string? _imageBase64;
        public string ImageFullPath { get; init; }

        public ImageFileManager(IFormFile image, string imageName, string imageUploadPath)
            : base(image, imageName, imageUploadPath)
        {
            _image = image;
            _imageName = imageName;
            _imageUploadPath = imageUploadPath;

            ImageFullPath = Path.Combine(imageUploadPath, _imageName);
        }

        public ImageFileManager(string imageData, string imageName, string imageUploadPath)
            : base(imageData, imageName, imageUploadPath)
        {
            _imageBase64 = imageData;
            _imageName = imageName;
            _imageUploadPath = imageUploadPath;

            ImageFullPath = Path.Combine(imageUploadPath, _imageName);
        }

        public override void SaveFile()
        {
            UploadedFilesCleaner ufc = new(_imageUploadPath);
            ufc.CleanFilesSafely();
            
            using (FileStream fs = new FileStream(ImageFullPath, FileMode.OpenOrCreate))
            {
                _image.CopyTo(fs);
            }
        }

        public override void SaveBase64File()
        {
            File.WriteAllBytes(ImageFullPath, Convert.FromBase64String(_imageBase64));
        }

        public static string GenerateFileName(string fileName)
        {
            return $"{DateTime.Now.ToString("yyyyMMddHHmmss")}_{fileName}";
        }
    }
}
