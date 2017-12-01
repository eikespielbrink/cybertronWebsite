using System;
using System.IO;
using System.ServiceProcess;

namespace ServeOrDie
{
    public class RestartService
    {
        private readonly string _serviceName;

        public RestartService(string serviceName)
        {
            _serviceName = serviceName;
        }

        public void Do()
        {
            var services = ServiceController.GetServices();
            foreach (var service in services)
            {
                if (service.ServiceName == _serviceName)
                {
                    ServiceController serviceToStart = new ServiceController(service.ServiceName);

                    if (service.Status == ServiceControllerStatus.Running)
                        service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromMilliseconds(1000));
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromMilliseconds(1000));
                    using (StreamWriter stream = new StreamWriter((@"C:\Program Files\CybertronData\Log.txt")))
                    { }
                    System.IO.File.WriteAllText(@"C:\Program Files\CybertronData\Log.txt", "IcarusLicenseServer changed at " + DateTime.Now.ToString());
                }
            }
        }

    }
}
