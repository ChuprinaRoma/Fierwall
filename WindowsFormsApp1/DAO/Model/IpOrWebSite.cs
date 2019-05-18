using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.DAO.Model
{
    public class IpOrWebSite
    {
        public int ID { get; set; }
        public string IpSourse { get; set; }
        public string IpPurpose { get; set; }
        public bool Status { get; set; }
        public string Comment { get; set; }
    }
}
