using System;

namespace Data
{
    public interface IStorage
    {
        public void Save<T>(string path, T data, Action<bool> callback = null);

        public T Load<T>(string path, Action<bool> callback = null);
    }
}