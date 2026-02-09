using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Data
{
    public class BaseStorage : IStorage
    {
        private string _path;
        private string _json;
        private string _directory;

        public void Save<T>(string path, T data, Action<bool> callback = null)
        {
            
            _path = BuildPath(path);
            
            _json = JsonConvert.SerializeObject(data);
            
            _directory = Path.GetDirectoryName(_path);
            
            if (!Directory.Exists(_directory))
            {
                Directory.CreateDirectory(_directory);
            }

            using (var streamWriter = new StreamWriter(_path))
            {
                streamWriter.WriteLine(_json);
            }

            data = JsonConvert.DeserializeObject<T>(_json);
            _json = JsonConvert.SerializeObject(data);
            
            Debug.Log(_json);

            callback?.Invoke(true);
        }

        public T Load<T>(string path, Action<bool> callback = null)
        {
            _path = BuildPath(path);

            using (var streamReader = new StreamReader(_path))
            {
                _json = streamReader.ReadLine();

                if (string.IsNullOrEmpty(_json))
                    callback?.Invoke(false);
                else
                    callback?.Invoke(true);
                
                Debug.Log(_json);

                return JsonConvert.DeserializeObject<T>(_json);
            }
        }

        private string BuildPath(string path)
        {
            return Path.Combine(Application.persistentDataPath, path);
        }
    }
}