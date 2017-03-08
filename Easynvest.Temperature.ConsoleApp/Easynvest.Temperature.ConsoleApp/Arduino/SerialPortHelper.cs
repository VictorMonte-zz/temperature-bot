using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easynvest.Temperature.ConsoleApp.Arduino
{
    public class SerialPortHelper
    {
        private string PortName;
        private SerialPort Port;
        public SerialPortHelper(string port)
        {
            PortName = port;

            if (Port != null)
            {
                //For this project this is the commum resources
                Port.BaudRate = 9600;
                Port.Parity = Parity.None;
                Port.StopBits = StopBits.One;
                Port.DataBits = 8;
                Port.Handshake = Handshake.None;
                Port.RtsEnable = true;

                Port.Open();
            }            

        }

        public void ClosePort()
        {
            if (Port != null && Port.IsOpen)
            {
                Port.Close();
            }
        }
    }
}
