namespace BlackSquares.DataCleaners
{
    public class UploadedFilesCleaner
    {
        public string FilesPath { get; init; }
        private int _maxAvailableFilesSize = 104857600;
        
        public UploadedFilesCleaner(string filesPath)
        {
            FilesPath = filesPath;
        }
        
        private void CleanFiles()
        {
            DirectoryInfo di = new DirectoryInfo(FilesPath);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }

        public void CleanFilesSafely()
        {
            if (GetTotalFileSize() > _maxAvailableFilesSize)
            {
                CleanFiles();
            }
        }

        private long GetTotalFileSize()
        {
            long totalSize = 0;
            DirectoryInfo di = new DirectoryInfo(FilesPath);
            foreach (FileInfo file in di.GetFiles())
            {
                totalSize += file.Length;
            }
            return totalSize;
        }
    }
}
