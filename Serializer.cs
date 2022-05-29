using System;
using System.IO;
using Serilog;

namespace Processors
{
    public abstract class Serializer
    {
        private protected abstract void Serialize<T>(T serializable, string path);

        private protected abstract T Deserialize<T>(string path);

        private static void Message
            (string message, Exception exception)
        {
            Log.Error(message + exception.Message);
        }

        public T Read<T>(string path)
        {
            Log.Information($"{Messages.Read}: '{path}'");
            T deserilizeable = default;
            try
            {
                deserilizeable = Deserialize<T>(path);
            }
            catch (ArgumentException exception)
            {
                Message(Exceptions.Read, exception);
            }
            catch (IOException exception)
            {
                Message(Messages.Read, exception);
            }
            return deserilizeable;
        }

        public void Write<T>(string path, T serializable)
        {
            Log.Information($"{Messages.Write}: '{path}'");
            try
            {
                Serialize(serializable, path);
            }
            catch (IOException exception)
            {
                Message(Messages.Write, exception);
            }
        }
    }
}