using NLog;

namespace SBO_CMDLine.system
{
    public static class Logger
    {
        private static string _prefix = "";
        private static NLog.Logger _nlog;

        public static void Init(string prefix)
        {
            _prefix = prefix + ": ";
            _nlog = LogManager.GetLogger(prefix);
        }

        public static void ShutdownLogger()
        {
            LogManager.Shutdown();
        }

        public static NLog.Logger GetLogger()
        {
            if (_nlog == null)
                Init("LOG");

            return _nlog;
        }
    }
}