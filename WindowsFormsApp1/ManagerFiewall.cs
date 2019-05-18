
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using WindowsFormsApp1.DAO;
using WindowsFormsApp1.DAO.Model;

namespace WindowsFormsApp1
{
    public class ManagerFiewall
    {
        private List<IPChecked> iPCheckeds = null;
        private HttpListener listener = null;

        public ManagerFiewall()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://+/");
        }

        public void Start()
        {
            iPCheckeds = new List<IPChecked>();
            listener.Start();
            InitAsync();
        }

        private async void InitAsync()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    ThreadPool.QueueUserWorkItem(Process, listener.GetContext());
                }
            });
        }

        private void Process(object o)
        {
            string responseSt = "";
            List<IpOrWebSite> ipOrWebSites = ManagerDAO.GetIpOrWebSites();
            var context = o as HttpListenerContext;
            HttpListenerResponse response = context.Response;
            HttpListenerRequest request = context.Request;
            string ipReqvesr = request.RemoteEndPoint.Address.ToString();
            IpOrWebSite ipOrWebSite = ipOrWebSites.FirstOrDefault(ip => ip.IpSourse == ipReqvesr);
            if (ipOrWebSite != null)
            {
                if (ipOrWebSite.Status)
                {
                    responseSt = "Ok";
                }
                else
                {
                    responseSt = "Ban";
                }
            }
            else
            {
                responseSt = "Ok";
                IPChecked iPChecked = iPCheckeds.FirstOrDefault(ip => ip.Ip == ipReqvesr);
                if (iPChecked != null)
                {
                    iPChecked.CountReqvest++;
                }
                else
                {
                    TimerCallback tm = new TimerCallback(CheckIp);
                    iPChecked = new IPChecked();
                    iPChecked.CountReqvest = 1;
                    iPChecked.Ip = ipReqvesr;
                    iPChecked.timer = new Timer(tm, iPChecked, 1000*60, Timeout.Infinite);
                    iPCheckeds.Add(iPChecked);
                }
            }
            Loger.WriteLog("Info", "Process", DateTime.Now.ToLongDateString(), ipReqvesr, responseSt, "Reqvest");
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseSt);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        private void CheckIp(object o)
        {
            IPChecked iPChecked = o as IPChecked;
            if (iPChecked != null)
            {
                if (iPChecked.CountReqvest > 100)
                {
                    ManagerDAO.AddNewIp(iPChecked.Ip);
                    Loger.WriteLog("Info", "CheckIp", DateTime.Now.ToLongDateString(), iPChecked.Ip, "Ban", "Blocked for many requests");
                }
                iPCheckeds.Remove(iPChecked);
            }
        }
    }
}
