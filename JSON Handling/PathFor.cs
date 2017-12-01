using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybertron.Client
{
    public sealed class PathFor
    {
        private readonly string _fileName;
        private const string keyBase = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths";

        public PathFor(string fileName)
        {
            _fileName = fileName;
        }

        public string Get()
        {
            RegistryKey localMachine = Registry.LocalMachine;
            RegistryKey fileKey = localMachine.OpenSubKey(string.Format(@"{0}\{1}", keyBase, _fileName));
            var test = Registry.GetValue(@"HKEY_CURRENT_USER\Software\CT CoreTechnologie\evolution4\General", "BrowserHomePort", "");
            object result = null;
            if (fileKey != null)
            {
                result = fileKey.GetValue(string.Empty);
            }
            fileKey.Close();

            return (string)result;
        }
    }
}
