using Newtonsoft.Json;
using System.Runtime.CompilerServices;

namespace Instrumentation
{
    public static class InstrumentationExtension
    {
        public const string ProjectName = "TMS";
        public static (ILogger, string) SetMessage(this ILogger logger,
            string callerType,
            [CallerMemberName] string callerMemberName = null)
        {
            return (logger, $"{ProjectName}-{callerType}-{callerMemberName}");
        }

        public static ((ILogger, string), (string, object)[]) SetMonitoringItems(
            this (ILogger, string) instrumentation,
            params (string, object)[] args)
        {
            return (instrumentation, args);
        }

        public static T Handle<T>(
            this ((ILogger, string), (string, object)[]) instrumentation,
            Func<T> function)
        {
            try
            {
                instrumentation.Item2?.ToList().ForEach(arg => instrumentation.Item1.Item1.Log($"{arg.Item1}:{arg.Item2}"));
                var result = function();
                instrumentation.Item1.Item1.Log($"{nameof(result)}:{JsonConvert.SerializeObject(result, Formatting.Indented)}");
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                instrumentation.Item1.Item1.Log(instrumentation.Item1.Item2);
            }
        }
    }
}