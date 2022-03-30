using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Forms;

using System.ComponentModel;

namespace graduation.ViewModels
{
    internal class DownloadViewModel:INotifyPropertyChanged
    {
        public string Title { get; private set; }

        public string _hash = string.Empty;
        public string Hash { get { return _hash; } 
            private set { 
                _hash = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hash"));
            } }
        public DownloadViewModel()
        {
            Title = "下载页";
        }
        public DownloadViewModel(string title)
        {
            Title =title;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
