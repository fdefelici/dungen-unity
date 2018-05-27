using System;

namespace DungeonGeneration.Logging {
    public class NullLogger : IXLogger {
        public void error(string v) {
        }

        public void info(string v) {
        }

        public void warning(string v) {
        }
    }
}
