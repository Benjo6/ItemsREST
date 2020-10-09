using System;

namespace ConsumeRest
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsumeWorker worker = new ConsumeWorker();
            worker.start();
            Console.ReadLine();
        }
    }
}
