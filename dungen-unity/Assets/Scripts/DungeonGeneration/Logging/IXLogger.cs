using System;

namespace DungeonGeneration.Logging {
    public interface IXLogger {
        void warning(string v);
        void error(string v);
        void info(string v);
    }
}