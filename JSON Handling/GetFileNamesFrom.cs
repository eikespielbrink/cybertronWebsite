using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cybertron.Client
{
    public sealed class GetFileNamesFrom : IEnumerable<string>
    {
        readonly List<string> _filelist;

        public GetFileNamesFrom(List<string> filelist)
        {
            _filelist = filelist;
        }

        public IEnumerator<string> GetEnumerator()
        {
            List<string> returnlist = new List<string>();
            foreach(var file in _filelist)
            {
                returnlist.Add(file.Substring(file.LastIndexOf("\\")+1));
            }
            return returnlist.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
