using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LukeVo.MemoryMonitor
{

    public class MemoryLogger : IDisposable
    {
        private const string LogFormat = "{0},{1}";

        private static readonly int IntervalMillisecond = int.Parse(ConfigurationManager.AppSettings["IntervalMillisecond"]);
        private static readonly string OutputFolder = ConfigurationManager.AppSettings["OutputFolder"];

        private CancellationTokenSource cancellationTokenSource;

        public MemoryLogger()
        {
            Directory.CreateDirectory(OutputFolder);
        }

        public void Start()
        {
            this.cancellationTokenSource = new CancellationTokenSource();

            this.AppendLine("Memory Logger Started");
            Task.Run(this.Work, this.cancellationTokenSource.Token);
        }

        private async Task Work()
        {
            while (!this.cancellationTokenSource.IsCancellationRequested)
            {
                var totalRam = PerformanceInfo.GetTotalMemoryInMB();
                var freeRam = PerformanceInfo.GetPhysicalAvailableMemoryInMB();

                var percent = freeRam * 100 / totalRam;

                this.AppendLine(string.Format("{0},{1},{2}",
                    freeRam,
                    totalRam,
                    percent));

                await Task.Delay(IntervalMillisecond);
            }
        }

        private void Append(string content)
        {
            File.AppendAllText(
                this.GetOutputFilePath(),
                string.Format(LogFormat, DateTime.Now.ToString("o"), content),
                Encoding.UTF8);
        }

        private void AppendLine(string content)
        {
            Append(content + Environment.NewLine);
        }

        private string GetOutputFilePath()
        {
            return Path.Combine(OutputFolder, $"{DateTime.Now.ToString("yyyy-MM-dd")}.log");
        }

        public void Stop()
        {
            this.AppendLine("Memory Logger Stop Requested");
            if (!this.cancellationTokenSource.IsCancellationRequested)
            {
                this.cancellationTokenSource.Cancel();

                this.AppendLine("Memory Logger Stopped");
            }
            else
            {
                this.AppendLine("Memory Logger is already Stopped");
            }
        }

        public void Dispose()
        {
            this.Stop();
        }
    }

}
