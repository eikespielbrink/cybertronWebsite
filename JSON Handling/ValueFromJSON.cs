using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;
using System.Linq;
using System.IO;

namespace ServeOrDie
{
    public sealed class ValueFromJSON
    {
        private readonly string _jsonPath;
        private readonly string _key;
        private readonly User _user;
        private readonly string _userName;

        public ValueFromJSON(string jsonPath,User user ,string key)
        {
            _jsonPath = jsonPath;
            _key = key;
            _user = user;
        }

        public ValueFromJSON(string jsonPath, string userName, string key)
        {
            _jsonPath = jsonPath;
            _key = key;
            _userName = userName;
        }

        public string IntoUser()
        {
            try
            {
                if (_user != null && File.Exists(_jsonPath))
                {
                    string returnValue = "";
                    var pathInput = new TextOf(new InputOf(new Uri(_jsonPath))).AsString();
                    User data = JsonConvert.DeserializeObject<User>(pathInput, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    data.ToDictionary().TryGetValue(_key, out returnValue).ToString();
                    return returnValue;
                }
                else if(_userName != null && File.Exists(_jsonPath))
                {
                    string returnValue = "";
                    var pathInput = new TextOf(new InputOf(new Uri(_jsonPath))).AsString();
                    User data = JsonConvert.DeserializeObject<User>(pathInput, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All });
                    data.ToDictionary().TryGetValue(_key, out returnValue).ToString();
                    return returnValue;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}