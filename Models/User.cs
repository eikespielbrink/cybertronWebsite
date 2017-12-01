using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServeOrDie
{
    public class User
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public bool HasEvoOpen { get; set; }
        public DateTime DateTime {get;set;}


        public User()
        {

        }

        [JsonConstructor]
        public User(string name, string ip, string hasEvoOpen, string dateTime)
        {
            Name = name.ToUpper();
            this.IP = ip;
            this.HasEvoOpen = System.Convert.ToBoolean(hasEvoOpen);
            DateTime = Convert.ToDateTime(dateTime);
        }

        public User(Dictionary<string, string> dic)
        {
            Name = dic["Name"].ToUpper();
            IP = dic["IP"];
            HasEvoOpen = System.Convert.ToBoolean(dic["HasEvoOpen"]);
            DateTime = Convert.ToDateTime(dic["LastUpdateTime"]);
        }

        public User(string[] user)
        {
            Name = user[1].ToUpper();
            IP = user[2];
            HasEvoOpen = System.Convert.ToBoolean(user[3]);
            DateTime = Convert.ToDateTime(user[4]);
        }

        public override string ToString()
        {
            var returnValue = @"{ Name:" + Name + "," + "IP:" + IP + "," + "HasEvoOpen:" + HasEvoOpen.ToString() + "LastUpdateTime:" + DateTime.ToString() + "}";
            return returnValue;
        }

        public Dictionary<string,string> ToDictionary()
        {
            return new Dictionary<string, string>() { { "Name", Name },
                                                        { "IP", IP },
                                                        { "HasEvoOpen", HasEvoOpen.ToString() },
                                                        { "LastUpdateTime", DateTime.ToString()} };
        }
    }
}
