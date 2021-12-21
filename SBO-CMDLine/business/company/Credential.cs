using System;
using System.IO;
using System.Xml.Serialization;

namespace SBO_CMDLine.business.company
{
    [Serializable()]
    public class Credential
    {
        [XmlElement("ServerName")] public string ServerName { get; set; }
        [XmlElement("DbType")] public int DbType { get; set; }
        [XmlElement("CompanyDbName")] public string CompanyDbName { get; set; }
        [XmlElement("LicenseServerAndPort")] public string LicenseServerAndPort { get; set; }
        [XmlElement("UserName")] public string UserName { get; set; }
        [XmlElement("Password")] public string Password { get; set; }

        public static Credential GetInstance(string xmlFile)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(Credential));
                using (var stringReader = new StreamReader(xmlFile))
                {
                    var obj = serializer.Deserialize(stringReader);
                    return (Credential) obj;
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Object mapping error: {e.Message}");
            }
        }
    }
}