namespace Instrumentation
{
    public class FileLogger : ILogger
    {
        private readonly string _path;
        public FileLogger(string path)
        {
            _path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public void Log(string message)
        {
            if (File.Exists(_path))
                File.AppendAllText(_path, message);
        }
    }
}
