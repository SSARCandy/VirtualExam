using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace VirtualExam
{
    [Serializable]
    class VEClient
    {
        private string userName;
        private string password;
        private string computerName;
        private string ipAddress;

        public VEClient()
        {
            this.computerName = Dns.GetHostName();
            IPAddress[] ipa = Array.FindAll(Dns.GetHostEntry(string.Empty).AddressList, a => a.AddressFamily == AddressFamily.InterNetwork);
            this.ipAddress = ipa[0].ToString();

        }

        public string getUserName()
        {
            return this.userName;
        }
        public void setUserName(string userName)
        {
            this.userName = userName;
        }

        public string getPassword()
        {
            return this.password;
        }
        public void setPassword(string password)
        {
            this.password = password;
        }

        public string getComputerName()
        {
            return computerName;
        }

        public string getIpAddress()
        {
            return ipAddress;
        }
    }
}
