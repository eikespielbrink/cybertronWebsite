using Microsoft.AspNetCore.Mvc;
using ServeOrDie.JSON_Handling;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System;
using System.Threading;
using Newtonsoft.Json;


namespace ServeOrDie.Controllers
{

    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {

        //api/User/PostInfo
        [HttpPost("PostInfo")]
        public IActionResult PostInfo([FromBody] User user)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://" + user.IP + ":53937/api/User");
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";
                httpWebRequest.UseDefaultCredentials = true;
                httpWebRequest.PreAuthenticate = true;
                httpWebRequest.Credentials = CredentialCache.DefaultCredentials;

                string result;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }

                User evoUser = JsonConvert.DeserializeObject<User>(result);

                if(!evoUser.HasEvoOpen &&
                    new LicenseCheck.Usage3DEvolution().Output().ToUpper() == evoUser.Name)
                {
                    new RestartService("CodeMeter.exe").Do();
                }

                return Ok();
            }
            catch(Exception e) { return Json(e); }
            finally
            {
                string userConfigPath = @"C:\Program Files\CybertronData\EvoUserConfig.json";
                string dir = @"C:\Program Files\CybertronData";
                new UpdateJSON(userConfigPath, user, dir).Do();
            }
        }

        //api/User/PostCheck/
        [HttpPost("PostCheck")]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                string userConfigPath = @"C:\Program Files\CybertronData\" + user.Name + "Config.json";
                string dir = @"C:\Program Files\CybertronData";

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                if (System.IO.File.Exists(userConfigPath))
                {
                    string userBlockingEvo = "";
                    if (!String.IsNullOrEmpty(new LicenseCheck.Usage3DEvolution().Output()))
                        userBlockingEvo = new LicenseCheck.Usage3DEvolution().Output().ToUpper();

                    // Evo läuft nicht lokal und Lizenz ist ausgeliehen -> Lizenz muss zurück durch Neustart
                    if (new ValueFromJSON(userConfigPath, user, "HasEvoOpen").IntoUser() == "false" &&
                       ((Convert.ToDateTime(user.DateTime) - Convert.ToDateTime(new ValueFromJSON(userConfigPath, user, "LastUpdateTime").IntoUser())).Minutes < 2) &&    //letzters Post von user weniger als 2 min her ist
                       !(user.HasEvoOpen) && new LicenseCheck.Usage3DEvolution().Output().ToUpper() == user.Name)
                    {
                        new RestartService("CodeMeter.exe").Do();
                    }

                    // Derjenige der Evo Lizenz hat kann nicht gepingt werden -> Lizenz muss zurück durch Neustart
                    if (!String.IsNullOrEmpty(userBlockingEvo))
                    {
                        if (!(new Ping().Send(new ValueFromJSON(ConfigPathFor(userBlockingEvo.ToUpper()), userBlockingEvo.ToUpper(), "IP").IntoUser()).Status == IPStatus.Success) &&
                                                      (new LicenseCheck.Usage3DEvolution().Output().ToUpper() == userBlockingEvo))
                        {
                            new RestartService("CodeMeter.exe").Do();
                        }
                    }
                    new UpdateJSON(userConfigPath, user, dir).Do();
                }
                else
                {
                    new UpdateJSON(userConfigPath, user, dir).Do();
                }
                return Ok();
            }
            catch(Exception e) { return Json(e.Message); }
        }

        private  string ConfigPathFor(string name)
        {
            return @"C:\Program Files\CybertronData\" + name + "Config.json";
        }

    }
}
