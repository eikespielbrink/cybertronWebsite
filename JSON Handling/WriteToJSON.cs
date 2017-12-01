using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Cybertron.Client;

namespace ServeOrDie
{
    public sealed class WriteToJSON
    {
        private readonly string _jsonPath;
        private readonly Dictionary<string,string> _dic;
        private readonly Dictionary<string, Dictionary<string, string>> _dic2;
        private readonly User _user;
        private readonly List<User> _users;

        public WriteToJSON(string jsonPath, Dictionary<string,string> dic)
        {
            _jsonPath = jsonPath;
            _dic = dic;
        }

        public WriteToJSON(string jsonPath, Dictionary<string,Dictionary<string,string>> dic)
        {
            _jsonPath = jsonPath;
            _dic2 = dic;
        }

        public WriteToJSON(string jsonPath, User user)
        {
            _jsonPath = jsonPath;
            _user = user;
        }

        public WriteToJSON(string jsonPath, List<User> users)
        {
            _jsonPath = jsonPath;
            _users = users;
        }

        public void Do()
        {
            string data = "";
            if (_dic != null)
            {
                data = JsonConvert.SerializeObject(_dic, Formatting.Indented);
            }
            else if(_user != null)
            {
                data = JsonConvert.SerializeObject(_user, Formatting.Indented);
            }
            else if(_users != null)
            {
                data = JsonConvert.SerializeObject(_users, Formatting.Indented);
            }
            File.WriteAllText(_jsonPath, data);
        }
    }
}
