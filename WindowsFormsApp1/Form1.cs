using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.DAO;
using WindowsFormsApp1.DAO.Model;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ManagerDAO ManagerDAO = null;
        ManagerFiewall managerFiewall = null;

        public Form1()
        {
            InitializeComponent();
            ManagerDAO = new ManagerDAO();
            managerFiewall = new ManagerFiewall();
            GetOrInitDatagrid();
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            ManagerDAO.AddNewIp(Convert.ToInt32(textBox1.Text), textBox6.Text, Convert.ToBoolean(textBox4.Text), textBox3.Text, textBox2.Text, textBox8.Text);
            dataGridView1.Rows.Clear();
            GetOrInitDatagrid();
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            ManagerDAO.RemoveIp(Convert.ToInt32(textBox1.Text));
            dataGridView1.Rows.Clear();
            GetOrInitDatagrid();
        }

        private void GetOrInitDatagrid()
        {
            List<IpOrWebSite> ipOrWebSites = ManagerDAO.GetIpOrWebSites();
            foreach(var iporweb in ipOrWebSites)
            {
                dataGridView1.Rows.Add(iporweb.ID, iporweb.IpSourse, iporweb.IpPurpose, iporweb.Status, iporweb.Comment);
            }
        }

        private void Button9_Click(object sender, System.EventArgs e)
        {
            managerFiewall.Start();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            GetOrInitDatagrid();
        }
    }
}
