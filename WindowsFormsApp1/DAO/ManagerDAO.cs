using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using WindowsFormsApp1.DAO.Model;

namespace WindowsFormsApp1.DAO
{
    public class ManagerDAO
    {
        private static List<IpOrWebSite> ipOrWebSites = null;
        
        public ManagerDAO()
        {
            if (File.Exists("Ip.txt"))
            {
                ipOrWebSites = JsonConvert.DeserializeObject<List<IpOrWebSite>>(File.ReadAllText("Ip.txt"));
            }
            else
            {
                ipOrWebSites = new List<IpOrWebSite>();
            }
        }

        public static List<IpOrWebSite> GetIpOrWebSites()
        {
            return ipOrWebSites;
        }

        public static void AddNewIp(string ipSourse)
        {
            IpOrWebSite ipOrWebSite = null;
            if (ipSourse != null && ipSourse != "")
            {
                ipOrWebSite = new IpOrWebSite();
                ipOrWebSite.ID = new Random().Next(100, 10000);
                ipOrWebSite.IpSourse = ipSourse;
                ipOrWebSite.IpPurpose = "";
                ipOrWebSite.Status = false;
                ipOrWebSite.Comment = "Many requests";
                ipOrWebSites.Add(ipOrWebSite);
            }
            File.WriteAllText("Ip.txt", JsonConvert.SerializeObject(ipOrWebSites));
        }

        public void AddNewIp(int iD, string ipPurpose, bool status, string comment, string ipSourse = null, string webSite = null)
        {
            IpOrWebSite ipOrWebSite = null;
            if (ipSourse != null && ipSourse != "")
            {
                ipOrWebSite = new IpOrWebSite();
                ipOrWebSite.ID = iD;
                ipOrWebSite.IpSourse = ipSourse;
                ipOrWebSite.IpPurpose = ipPurpose;
                ipOrWebSite.Status = status;
                ipOrWebSite.Comment = comment;
                ipOrWebSites.Add(ipOrWebSite);
            }
            if (webSite != null && webSite != "")
            {
                IPHostEntry entry = Dns.GetHostEntry(webSite.Replace("http://", ""));
                foreach(var addres in entry.AddressList)
                {
                    ipOrWebSite = new IpOrWebSite();
                    ipOrWebSite.ID = iD;
                    ipOrWebSite.IpSourse = addres.ToString();
                    ipOrWebSite.IpPurpose = ipPurpose;
                    ipOrWebSite.Status = status;
                    ipOrWebSite.Comment = comment;
                    ipOrWebSites.Add(ipOrWebSite);
                }
            }
            File.WriteAllText("Ip.txt", JsonConvert.SerializeObject(ipOrWebSites));
        }

        public void RemoveIp(int id)
        {
            ipOrWebSites.RemoveAll(ip => ip.ID == id);
            File.WriteAllText("Ip.txt", JsonConvert.SerializeObject(ipOrWebSites));
        }
    }
}
