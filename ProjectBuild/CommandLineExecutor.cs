using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectBuild
{
    public static class CommandLineExecutor
    {
        public static async Task ExecuteCommandAsync(string command, string args, Func<string, Task> stdOutputCallback, Dictionary<string, string>? envVariables = null)
        {
            using var process = new Process();

            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = args;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            if (envVariables != null)
            {
                foreach (var envVariable in envVariables)
                {
                    process.StartInfo.EnvironmentVariables.Add(envVariable.Key, envVariable.Value);
                }
            }

            process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Result not awaited deliberately
                Task.Run(async () => {
                    try
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            await stdOutputCallback(e.Data);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}"); // TODO: log error
                    }
                });
            });

            // TODO: reuse std out
            process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            {
                // Result not awaited deliberately
                Task.Run(async () => {
                    try
                    {
                        if (!String.IsNullOrEmpty(e.Data))
                        {
                            await stdOutputCallback(e.Data);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Exception: {ex.Message}"); // TODO: log error
                    }
                });
            });

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            await process.WaitForExitAsync();
            process.Close();
        }
    }
}
