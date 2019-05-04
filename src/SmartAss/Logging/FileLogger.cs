using System;
using System.IO;
using System.Text;

namespace SmartAss.Logging
{
    public class FileLogger : TextWriter
    {
        public FileLogger(string directory)
        {
            file = new FileInfo($"{directory}/{DateTime.Now:yyyy-MM-dd_HH_mm_ss}.log");
        }
        private readonly FileInfo file;

        public override Encoding Encoding => Encoding.UTF8;

        public override void WriteLine(string value)
        {
            if (!file.Directory.Exists) { file.Directory.Create(); }

            using (var writer = file.AppendText())
            {
                writer.WriteLine(value);
            }
        }
    }
}
