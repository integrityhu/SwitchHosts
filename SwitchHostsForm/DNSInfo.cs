using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SwitchHostsForm
{
    //xml descriptor
    public class DNSInfo
    {
        [XmlElement("name")]
        public string Name { get; set; }
        [XmlElement("dns1")]
        public string DNS1 { get; set; }
        [XmlElement("dns2")]
        public string DNS2 { get; set; }
    }
}
