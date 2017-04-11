using System;

namespace GraphApiSamples
{
    public interface ILogger
    {
        void WriteLine(string message);
        void Write(string message);
        void WriteError(string message);
        string ReadLine();
        int Read();
    }

    public class ConsoleLogger : ILogger
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void WriteError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine(message);
            Console.ResetColor();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public int Read()
        {
            return Console.Read();
        }
    }
}
