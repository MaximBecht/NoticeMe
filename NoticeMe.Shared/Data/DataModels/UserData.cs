using System.Collections.Generic;
using System.Xml.Serialization;

namespace NoticeMe.Data.DataModels
{
    [XmlType("UserData")]
    public class UserData
    {
        [XmlElement("Id")]
        public int Id { get; set; }
        [XmlElement("UserName")]
        public string UserName { get; set; }
        [XmlElement("FirstName")]
        public string FirstName { get; set; }
        [XmlElement("LastName")]
        public string LastName { get; set; }
        [XmlElement("Email")]
        public string Email { get; set; }

        public UserData() { }

        public UserData(int id, string userName, string firstName, string lastName, string email)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
    }

    [XmlRoot("UserDataXML")]
    [XmlInclude(typeof(UserData))]
    public class UserDataXML
    {
        [XmlArray("UserDataSets")]
        [XmlArrayItem("UserData")]
        public List<UserData> UserDataSets = new List<UserData>();

        public UserDataXML() { }
    }
}
