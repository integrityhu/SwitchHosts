using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SwitchHosts
{
    class HostInfo
    {
        readonly char[] separator = { ' ' };
        string ip;
        List<String> names;
        string comment;
        public HostInfo(string ip, List<String> names, string comment)
        {
            this.ip = ip;
            this.names = names;
            this.comment = comment;
        }

        public HostInfo(String confLine)
        {
            names = new List<String>();
            int commentMarkIdx = confLine.IndexOf("#");
            if (commentMarkIdx > -1)
            {
                comment = confLine.Substring(commentMarkIdx);
                confLine = confLine.Substring(0, commentMarkIdx);
            }
            else
            {
                comment = "";
            }
            confLine = confLine.Trim();
            if (!"".Equals(confLine))
            {
                string[] fields = confLine.Split(separator);
                if (fields.Length > 0)
                {
                    ip = fields[0];
                    for (int idx = 1; idx < fields.Length; idx++)
                    {
                        names.Add(fields[idx]);
                    }
                }
            }
        }

        public void addComment(string value)
        {
            if (this.comment != null && !("".Equals(this.comment.Trim())))
            {
                this.comment += ", ";
            }
            this.comment += value;
        }

        public string getComment()
        {
            return this.comment;
        }

        public List<String> getNames()
        {
            return this.names;
        }

        public string getIp()
        {
            return this.ip;
        }

        public override string ToString()
        {
            string hostLine = "";
            foreach (string hName in names)
            {
                hostLine += " " + hName;
            }
            return (ip + hostLine + " " + comment).Trim();
        }
    }

    class Program
    {
        static List<HostInfo> getHostInfoList(string fileName)
        {
            List<HostInfo> result = new List<HostInfo>();
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("UTF-8"), false);
            string confLine = sr.ReadLine();
            while (confLine != null)
            {
                HostInfo info = new HostInfo(confLine);
                result.Add(info);
                confLine = sr.ReadLine();
            }
            sr.Close();
            fs.Close();
            return result;
        }

        static Dictionary<String, HostInfo> getDictFromList(List<HostInfo> infoList)
        {
            Dictionary<String, HostInfo> result = new Dictionary<String, HostInfo>();
            foreach (HostInfo item in infoList)
            {
                HostInfo infoItem;
                if (item.getIp() != null)
                {
                    result.TryGetValue(item.getIp(), out infoItem);
                    if (infoItem != null)
                    {
                        foreach (string hostName in infoItem.getNames())
                        {
                            item.getNames().Add(hostName);
                        }
                        item.addComment(infoItem.getComment());
                        result.Remove(item.getIp());
                        result.Add(item.getIp(), item);
                    }
                    else
                    {
                        result.Add(item.getIp(), item);
                    }
                }
            }
            return result;
        }

        static void removeNamesFromIP(List<String> copy, List<String> removable)
        {
            foreach (String name in removable)
            {
                copy.Remove(name);
            }
        }

        static void removeIP(Dictionary<String, HostInfo> copy, HostInfo info)
        {
            copy.Remove(info.getIp());
        }

        static List<HostInfo> combine(List<HostInfo> src, Dictionary<String, HostInfo> conf)
        {
            List<HostInfo> result = new List<HostInfo>();
            Dictionary<String, HostInfo> removable = new Dictionary<String, HostInfo>();

            foreach (HostInfo info in src)
            {
                if (info.getIp() == null)
                {
                    result.Add(info);
                }
                else if (!conf.ContainsKey(info.getIp()))
                {
                    result.Add(info);
                }
                else if (!removable.ContainsKey(info.getIp()))
                {
                    removable.Add(info.getIp(), new HostInfo(info.getIp(), new List<String>(info.getNames()), info.getComment()));
                }
            }

            Dictionary<String, HostInfo> copy = new Dictionary<String, HostInfo>(conf);
            foreach (String key in removable.Keys)
            {
                removeIP(copy, removable[key]);
            }

            foreach (String key in copy.Keys)
            {
                result.Add(copy[key]);
            }
            return result;
        }

        static Dictionary<String, HostInfo> combine(Dictionary<String, HostInfo> src, Dictionary<String, HostInfo> conf)
        {
            Dictionary<String, HostInfo> result = new Dictionary<String, HostInfo>(src);
            foreach (string ip in conf.Keys)
            {
                if (result.ContainsKey(ip))
                {
                    result.Remove(ip);
                }
                else
                {
                    result.Add(ip, conf[ip]);
                }
            }
            return result;
        }

        static void Dictionary2Console(Dictionary<String, HostInfo> dict)
        {
            foreach (String ip in dict.Keys)
            {
                Console.WriteLine(dict[ip]);
            }

        }

        private static void List2Console(List<HostInfo> combinedList)
        {
            foreach (HostInfo row in combinedList)
            {
                Console.WriteLine(row);
            }
        }

        private static void saveHostInfoList(String fileName, List<HostInfo> list)
        {
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter sr = new StreamWriter(fs, Encoding.GetEncoding("UTF-8"));
            foreach (HostInfo hostInfo in list)
            {
                sr.WriteLine(hostInfo);
            }
            sr.Close();
            fs.Close();
        }

        static void Main(string[] args)
        {
            System.Diagnostics.EventLog appLog =
            new System.Diagnostics.EventLog();
            appLog.Source = "SwitchHost";
            try
            {
                List<HostInfo> confInfoList = getHostInfoList("./hosts.conf");
                List<HostInfo> hostsInfoList = getHostInfoList(Environment.GetEnvironmentVariable("SystemRoot") + "/system32/drivers/etc/hosts");

                Dictionary<String, HostInfo> confDict = getDictFromList(confInfoList);
                Dictionary<String, HostInfo> hostDict = getDictFromList(hostsInfoList);
                List<HostInfo> combinedList = combine(hostsInfoList, confDict);

                if (args.Count() > 0 && (args[0] == "-t"))
                {
                    List2Console(combinedList);
                }
                else
                {
                    saveHostInfoList(Environment.GetEnvironmentVariable("SystemRoot") + "/system32/drivers/etc/hosts", combinedList);
                }
                appLog.WriteEntry("SwitchHost ready.");

                Console.WriteLine("ready.");
                Console.ReadKey();
            }
            catch (System.UnauthorizedAccessException ex)
            {
                appLog.WriteEntry("SwitchHost error:" + ex.Message);
                Console.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                appLog.WriteEntry("Config file not found");
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
            appLog.Close();
        }

    }
}
