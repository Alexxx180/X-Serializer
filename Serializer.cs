using System;
using System.IO;

namespace Processors
{
    public abstract class Serializer
    {
        private protected abstract void Serialize<T>(T serializable, string path);

        private protected abstract T Deserialize<T>(string path);

        public T Read<T>(string path)
        {
            Status = $"{Messages.Read}: '{path}'\n\n";
            T deserilizeable = default;
            try
            {
                deserilizeable = Deserialize<T>(path);
            }
            catch (ArgumentException exception)
            {
                Status += $"{Exceptions.Read}{exception.Message}\n";
            }
            catch (IOException exception)
            {
                Status += $"{Exceptions.Read}{exception.Message}\n";
            }
            return deserilizeable;
        }

        public void Write<T>(string path, T serializable)
        {
            Status = $"{Messages.Write}: '{path}'\n\n";
            try
            {
                Serialize(serializable, path);
            }
            catch (IOException exception)
            {
                Status += $"{Exceptions.Write}{exception.Message}\n";
            }
        }

        public string Status { get; set; }
    }
}