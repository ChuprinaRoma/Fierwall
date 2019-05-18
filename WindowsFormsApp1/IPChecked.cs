using System.Threading;
using WindowsFormsApp1.DAO;

namespace WindowsFormsApp1
{
    public class IPChecked
    {
        public string Ip { get; set; }
        public int CountReqvest { get; set; }

        public Timer timer { get; set; }
    }
}
