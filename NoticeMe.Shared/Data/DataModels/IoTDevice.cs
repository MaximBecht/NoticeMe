using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace NoticeMe.Data.DataModels
{
    public class IoTDevice
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public BitmapImage PreviewImage { get; set;}

        public string Status { get; set; }
        public int TypingSpeed { get; set; }
        public int Keystrokes { get; set; }

        public IoTDevice() { }
        public IoTDevice(string name, string id, string type, BitmapImage previewImage) 
        {
            Name = name;
            Id = id;
            Type = type;
            PreviewImage = previewImage;
        }
    }
}
