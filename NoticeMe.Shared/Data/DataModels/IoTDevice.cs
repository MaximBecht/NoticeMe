using Microsoft.UI;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace NoticeMe.Data.DataModels
{
    public class IoTDevice : INotifyPropertyChanged
    {
        private string _name = "default";
        private int _typingSpeed = 0;
        private int _keyStrokes = 0;

        public string Name 
        { 
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Id { get; set; }
        public string Type { get; set; }
        public BitmapImage PreviewImage { get; set;}

        public Status Status { get; set; }
        public int TypingSpeed 
        { 
            get => _typingSpeed;
            set
            {
                if (_typingSpeed != value) 
                {
                    _typingSpeed = value;
                    OnPropertyChanged("TypingSpeed");
                }
            }
        }
        public int Keystrokes
        {
            get => _keyStrokes;
            set
            {
                if(_keyStrokes != value)
                {
                    _keyStrokes = value;
                    OnPropertyChanged("Keystrokes");
                }
            }
        }

        public IoTDevice() 
        {
            Status = new Status();
        }
        public IoTDevice(string name, string id, string type, BitmapImage previewImage) 
        {
            Name = name;
            Id = id;
            Type = type;
            PreviewImage = previewImage;
            Status = new Status();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    public enum StatusCategory
    {
        Offline,
        Online,
        Afk
    }
    public class Status : INotifyPropertyChanged
    {
        private static List<string> OfflineStatusStrings = new List<string>();
        private static List<string> OnlineStatusStrings = new List<string>();
        private static List<string> AfkStatusStrings = new List<string>();

        private StatusCategory _category = StatusCategory.Offline;
        public StatusCategory Category
        {
            get => _category;
            set
            {
                if (_category != value)
                {
                    _category = value;
                    OnPropertyChanged("Category");
                    OnPropertyChanged("DisplayStatus");
                    OnPropertyChanged("DisplayStatusColor");
                }
            }
        }
        public string DisplayStatus
        {
            get 
            {
                return $"( {GetRandomStatusString()} )";
            }
        }
        public SolidColorBrush DisplayStatusColor
        {
            get
            {
                if (Category == StatusCategory.Offline)
                {
                    return new SolidColorBrush(Colors.Red);
                }
                else if(Category == StatusCategory.Online)
                {
                    return new SolidColorBrush(Colors.Green);
                }
                else
                {
                    return new SolidColorBrush(Colors.Yellow);
                }
            }
        }

        private static bool LoadedStatusStrings
        {
            get
            {
                return OfflineStatusStrings.Count > 0 && OnlineStatusStrings.Count > 0 && AfkStatusStrings.Count > 0;
            }
        }


        public Status() 
        {
            LoadStatusStringsAsync();
        }

        private async void LoadStatusStringsAsync()
        {
            if(!LoadedStatusStrings) 
            {
                OfflineStatusStrings = await LoadStatusStringAsync("offline");
                OnlineStatusStrings = await LoadStatusStringAsync("online");
                AfkStatusStrings = await LoadStatusStringAsync("afk");
            }
        }
        private async Task<List<string>> LoadStatusStringAsync(string status)
        {
            List<string> strings = new List<string>();

            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri($"ms-appx:///Assets/Status/{status}.txt"));
            using (var inputStream = await file.OpenReadAsync())
            using (var classicStream = inputStream.AsStreamForRead())
            using (var streamReader = new StreamReader(classicStream))
            {
                while (streamReader.Peek() >= 0)
                {
                    strings.Add(streamReader.ReadLine());
                }
            }

            return strings;
        }
        private string GetRandomStatusString()
        {
            var random = new Random();
            int count, index = 0;

            switch (Category)
            {
                case StatusCategory.Offline: 
                    count = OfflineStatusStrings.Count;
                    if (count == 0)
                        return "Offline";
                    index = random.Next(0, count);
                    return OfflineStatusStrings[index];
                case StatusCategory.Online: 
                    count = OnlineStatusStrings.Count;
                    if (count == 0)
                        return "Online";
                    index = random.Next(0, count);
                    return OnlineStatusStrings[index]; 
                case StatusCategory.Afk: 
                    count = AfkStatusStrings.Count;
                    if (count == 0)
                        return "Afk";
                    index = random.Next(0, count);
                    return AfkStatusStrings[index];
            }

            return "Error 404";
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
