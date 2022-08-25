﻿using System;
using System.IO;
using System.Threading;

namespace Scorpion_LOG
{
    public class Scorpion_LOG
    {
        private FileStream fs_log;
        private StreamWriter sr_log;
        private string log_path = "";
        private readonly string logfile_name = "/scorpion.log";

        public void startLoggingPath(string path)
        {
            log_path = path + logfile_name;
            if (!File.Exists(log_path))
            {
                File.Create(log_path).Close();
                Console.WriteLine("Created new log file at {0}", log_path);
            }
            return;
        }

        public void log(string message)
        {
            Thread ths = new Thread(new ParameterizedThreadStart(write_log));
            ths.Start(message);
            return;
        }

        private void write_log(object message)
        {
            fs_log = new FileStream(log_path, FileMode.Append, FileAccess.Write, FileShare.Write);
            sr_log = new StreamWriter(fs_log);

            sr_log.WriteLine("LOG >> [{0}]-[TIME:{1}]", message, DateTime.UtcNow);

            sr_log.Flush();
            fs_log.Flush();
            sr_log.Close();
            fs_log.Close();
        }

        private void write_to_cui(string message)
        {
            Console.WriteLine("[LOG] >> {0}", message);
            return;
        }
    }
}
