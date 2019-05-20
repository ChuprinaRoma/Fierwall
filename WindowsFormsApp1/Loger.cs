using System;
using System.IO;

namespace WindowsFormsApp1
{
    public class Loger
    {
        public static void WriteLog(string type, string method, string time, string ip, string status, string info)
        {
            File.AppendAllText("log.txt", $"|{type}|Method: {method}|Time: {time}|Reqvest ip {ip}|Status: {status}|{info}|{Environment.NewLine}");
            Console.WriteLine($"|{type}|Method: {method}|Time: {time}|Reqvest ip {ip}|Status: {status}|{info}|");
        }
    }
}
