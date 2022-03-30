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

        private string _passwd;
        public string Passwd { get { return _passwd; }
            set { _passwd = value; } }

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
