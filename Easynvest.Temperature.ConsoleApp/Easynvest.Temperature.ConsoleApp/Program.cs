using Easynvest.Temperature.ConsoleApp.Arduino;
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
                x.Service<SerialPortHelper>(s =>
                {
                    s.ConstructUsing(name => new SerialPortHelper("COM6"));
                    s.WhenStarted(tc => {
                        tc.AddDataReceivedHandler(new SerialDataReceivedEventHandler((object sender, SerialDataReceivedEventArgs e) => 
                        {
                            //Tempo para o arduino ler algumas temperaturas
                            Thread.Sleep(2000);

                            string[] values = (((SerialPort)sender).ReadExisting()).Split(new string[] { "\r\n" }, StringSplitOptions.None);

                            //TODO: Enviar dados para ruby bot

                        }));
                        tc.Start();
                    });
                });
            });
        }
    }
}
