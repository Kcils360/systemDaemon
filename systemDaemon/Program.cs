﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace systemDaemon
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = new Builder().builder(args);
            var cts = new CancellationTokenSource();
            var config = LoadAppSettings();
            var ct = new CheckTime();

            RecuringTask(() => ct.RunCheck(config), 60, cts.Token);

            // This is the daemon start function.  This goes at the bottom
            await builder.RunConsoleAsync();
        }

        //**************** -Helper Functions- ******************************
        ///<summary> 
        /// This method creates an object from the appsettings.json to be used as config values
        ///</summary>
        ///<returns>IConfigurationRoot</returns>
        private static IConfigurationRoot LoadAppSettings()
        {
            try
            {
                var config = new ConfigurationBuilder()
                                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json", false, true)
                                //.AddJsonFile(@"C:\Users\Kcils\Desktop\syncdaemon\syncdaemon\syncdaemon\appsettings.json", false, true) //*use with VS IIS
                                .Build();

                //** Use this if-check to ensure no values in Appsetting.json are empty **
                //if (string.IsNullOrEmpty(config["{keyword}"]) || )
                //{
                //    return null;
                //}
                return config;
            }
            catch (System.IO.FileNotFoundException)
            {
                return null;
            }
        }

        /// <summary>
        /// This method will run while there is no cancellation token present. The Task.Delay method is used to specify the amount of time to elapse between function executions.
        ///</summary>
        /// <param name="action">a function to be executed
        ///<example>Console.Writeline("Hello World");</example>
        ///</param>
        ///<param name="seconds">int The number of seconds to wait between function executions</param>
        /// <param name="token">a CancellationToken object required for the Task to run</param>
        static void RecuringTask(Action action, int seconds, CancellationToken token)
        {
            if (action == null)
                return;
            Task.Run(async () => {
                while (!token.IsCancellationRequested)
                {
                    action();
                    seconds = getSeconds(); //**optional**
                    await Task.Delay(TimeSpan.FromSeconds(seconds), token);
                }
            }, token);
        }

        ///<summary>
        /// This function grabs the current dateTime and determines the number of seconds that have passed since the top of the hour. It will then return a number of seconds so the next time check happens exactly on the next hour.
        ///</summary>
        ///<returns>int seconds </returns>
        static int getSeconds()
        {
            var t = DateTime.Now;
            int tm = t.Minute;
            if (tm > 0) tm = (tm * 60);
            int ts = t.Second + tm;
            return 3600 - ts;
        }
    }
}
