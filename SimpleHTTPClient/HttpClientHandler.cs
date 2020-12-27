using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace SimpleHTTPClient
{
    public static class HttpClientHandler
    {
        public static bool InvalidURL { get; private set; }
        public static string GetIpAddressList(string host)
        {
            InvalidURL = false;
            string selectedIPs = "";
            try
            {
                var uri = new Uri(host);
                IPHostEntry hostEntry = Dns.GetHostByName(uri.Host);
                for (int i = 0; i < hostEntry.AddressList.Length; i++)
                {
                    selectedIPs += hostEntry.AddressList[i].ToString() + ";";
                }
            }
            catch(SocketException e)
            {
                InvalidURL = true;
                selectedIPs = "SocketException caught! " + e.Message;
            }
            catch(ArgumentNullException)
            {
                InvalidURL = true;
                selectedIPs = "Field is probably empty, fill it and try again!";
            }
            catch(Exception e)
            {
                InvalidURL = true;
                selectedIPs = "Unknown exception! " + e.Message;
            }
            return selectedIPs;
        }

        public static string UrlConnect(string url)
        {
            string result = "";
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                ServicePointManager.DefaultConnectionLimit = 9999;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();
                result += "HttpHeaders are: \n" + webRequest.Headers;
                result += "\n";
                Stream streamResponse = webResponse.GetResponseStream();
                StreamReader sr = new StreamReader(streamResponse);
                char[] readBuffer = new char[256];
                int count = sr.Read(readBuffer, 0, 256);
                result += "HTML Contents: \n";
                while (count > 0)
                {
                    string outputData = new string(readBuffer, 0, count);
                    result += outputData;
                    count = sr.Read(readBuffer, 0, count);
                }
                streamResponse.Close();
                sr.Close();
                webResponse.Close();
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
