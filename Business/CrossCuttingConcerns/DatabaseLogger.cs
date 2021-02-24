using System;

namespace Business.CrossCuttingConcerns
{
    public class DatabaseLogger : Ilogger
    {
        public void Log()
        {
            Console.WriteLine("Veritabanına Loglandı");
        }
    }
}
