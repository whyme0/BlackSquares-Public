namespace BlackSquares.Managers
{
    public abstract class FileManager
    {
        private readonly string? _base64File;
        private readonly string _fileUploadPath;
        private readonly string _fileName;
        private readonly IFormFile? _file;

        public FileManager(IFormFile file, string fileName, string fileUploadPath)
        {
            _file = file;
            _fileName = fileName;
            _fileUploadPath = fileUploadPath;
        }

        public FileManager(string fileData, string fileName, string fileUploadPath)
        {
            _base64File = fileData;
            _fileName = fileName;
            _fileUploadPath = fileUploadPath;
        }

        abstract public void SaveFile();

        abstract public void SaveBase64File();
    }
}
