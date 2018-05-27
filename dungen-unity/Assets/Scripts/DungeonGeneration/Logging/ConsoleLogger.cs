using System;

namespace DungeonGeneration.Logging {
    public class ConsoleLogger : IXLogger {
        public void error(string v) {
            Console.WriteLine("[ERRO]: " + v);
        }

        public void info(string v) {
            Console.WriteLine("[INFO]: " + v);
        }

        public void warning(string v) {
            Console.WriteLine("[WARN]: " + v);
        }
    }
}
