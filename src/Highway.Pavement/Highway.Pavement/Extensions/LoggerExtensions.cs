using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using System.Diagnostics;
using System.Globalization;

namespace Highway.Pavement
{
    public static class LoggerExtensions
    {
        #region Debug

        public static IDisposable DebugScope(this ILog logger, string format, params object[] args)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsDebugEnabled
                ? new DebugLoggingScope(logger, string.Format(format, args))
                : null;
        }

        public static IDisposable DebugScope(this ILog logger, string message)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsDebugEnabled
                ? new DebugLoggingScope(logger, message)
                : null;
        }

        private sealed class DebugLoggingScope : LoggingScope
        {
            public DebugLoggingScope(ILog logger, string message)
                : base(logger, message) { }

            protected override void LogMessage(string message)
            {
                Logger.Debug(message);
            }
        }

        #endregion

        #region Info

        public static IDisposable InfoScope(this ILog logger, string format, params object[] args)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsInfoEnabled
                ? new InfoLoggingScope(logger, string.Format(format, args))
                : null;
        }

        public static IDisposable InfoScope(this ILog logger, string message)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsInfoEnabled
                ? new InfoLoggingScope(logger, message)
                : null;
        }

        private sealed class InfoLoggingScope : LoggingScope
        {
            public InfoLoggingScope(ILog logger, string message)
                : base(logger, message) { }

            protected override void LogMessage(string message)
            {
                Logger.Info(message);
            }
        }

        #endregion

        #region Warn

        public static IDisposable WarnScope(this ILog logger, string format, params object[] args)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsWarnEnabled
                ? new WarnLoggingScope(logger, string.Format(format, args))
                : null;
        }

        public static IDisposable WarnScope(this ILog logger, string message)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsWarnEnabled
                ? new WarnLoggingScope(logger, message)
                : null;
        }

        private sealed class WarnLoggingScope : LoggingScope
        {
            public WarnLoggingScope(ILog logger, string message)
                : base(logger, message) { }

            protected override void LogMessage(string message)
            {
                Logger.Warn(message);
            }
        }

        #endregion

        #region Error

        public static IDisposable ErrorScope(this ILog logger, string format, params object[] args)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsErrorEnabled
                ? new ErrorLoggingScope(logger, string.Format(format, args))
                : null;
        }

        public static IDisposable ErrorScope(this ILog logger, string message)
        {
            if (null == logger)
                throw new ArgumentNullException("logger");

            return logger.IsErrorEnabled
                ? new ErrorLoggingScope(logger, message)
                : null;
        }

        private sealed class ErrorLoggingScope : LoggingScope
        {
            public ErrorLoggingScope(ILog logger, string message)
                : base(logger, message) { }

            protected override void LogMessage(string message)
            {
                Logger.Error(message);
            }
        }

        #endregion

        private abstract class LoggingScope : IDisposable
        {
            private readonly ILog _logger;
            private readonly string _message;
            private readonly Stopwatch _stopwatch;

            protected LoggingScope(ILog logger, string message)
            {
                _logger = logger;
                _message = message;
                _stopwatch = new Stopwatch();

                LogMessage(string.Format(
                    CultureInfo.InvariantCulture,
                    EnterTemplate,
                    _message));

                _stopwatch.Start();
            }

            public void Dispose()
            {
                _stopwatch.Stop();

                LogMessage(string.Format(
                    CultureInfo.InvariantCulture,
                    LeaveTemplate,
                    _message,
                    _stopwatch.ElapsedMilliseconds));
            }

            public ILog Logger
            {
                get { return _logger; }
            }

            protected abstract void LogMessage(string message);

            private const string
                EnterTemplate = "[Enter] {0}",
                LeaveTemplate = "[Leave] {0} [{1:D}ms]";
        }
    }
}
