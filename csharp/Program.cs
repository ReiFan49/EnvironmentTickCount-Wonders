using System;

namespace TickCount {
    class Program {
        static void Main(string[] args) {
            foreach(string arg in args) {
                switch(arg) {
                case "/test":
                case "--test":
                    ProgramTest.PerformTest();
                break;
                default:
                continue;
                }
                return;
            }

            Console.WriteLine(String.Format(
                "{{\"tick_count\": {0}, \"tick_count_64\": {1}}}",
                Environment.TickCount, Environment.TickCount64
            ));
        }
    }
}
