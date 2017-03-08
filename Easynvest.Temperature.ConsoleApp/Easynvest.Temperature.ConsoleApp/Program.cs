using Easynvest.Temperature.ConsoleApp.Arduino;
using Easynvest.Temperature.ConsoleApp.Bot;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Topshelf;

namespace Easynvest.Temperature.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x => {
                x.Service<SerialPortManager>(s =>
                {
                    s.ConstructUsing(name => new SerialPortManager("COM6"));
                    s.WhenStarted(tc => {
                        tc.AddDataReceivedHandler(new SerialDataReceivedEventHandler((object sender, SerialDataReceivedEventArgs e) => 
                        {
                            //Tempo para o arduino ler algumas temperaturas
                            Thread.Sleep(2000);

                            RobotDataSender.Send(
                                    (((SerialPort)sender).ReadExisting())
                                        .Split(new string[] { "\r\n" }, StringSplitOptions.None)
                                        .FirstOrDefault());

                        }));
                        tc.Start();
                    });
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("Serviço de obtenção e envio de temperatura");
                x.SetDisplayName("Easynvest.Temperature.Service");
                x.SetServiceName("Easynvest.Temperature.Service");
            });
        }
    }
}
