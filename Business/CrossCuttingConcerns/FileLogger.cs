using System;
using System.Collections.Generic;
using System.Text;

namespace Business.CrossCuttingConcerns
{
    public class FileLogger : Ilogger
    {
        public void Log()
        {
            Console.WriteLine("Dosyaya Loglandı");
        }
    }
}
