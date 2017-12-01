using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace ServeOrDie.JSON_Handling
{
    public sealed class UpdateJSON
    {
        private readonly User _user;
        private readonly string _jsonPath;
        private readonly string _dir;

        public UpdateJSON(string jsonPath, User user, string dir)
        {
            _user = user;
            _jsonPath = jsonPath;
            _dir = dir;
        }

        public void Do()
        {
            if (!Directory.Exists(_dir))
                Directory.CreateDirectory(_dir);
            if (!File.Exists(_jsonPath))
            {
                CreateFile();
            }
            File.WriteAllText(_jsonPath, JsonConvert.SerializeObject(_user, Formatting.Indented));
        }

        private void CreateFile()
        {
            new FileInfo(_jsonPath).Refresh();
            using (StreamWriter stream = new StreamWriter(_jsonPath))
            { }
        }
    }
}
