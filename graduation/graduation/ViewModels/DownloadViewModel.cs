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
        public string Title
        {
            get; set;
        }

        /*
        private string _hash = String.Empty;
        public string Hash { get { return _hash; } 
            //Every an short time modification in Entry prompt(value is Text) will call this setter immediatly.
            set {
                if (_hash != value)
                {
                    //_hash no change，Still be empty so it will not change when clear to empty
                    _hash = value;
                    Title = value;
                    //Invoke event to nofity must be after the assigned.
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                    //Core concept: this event invoke means to declare this property has been changed, every property 
                    //in XAML bind to this property in ViewModel should immediately come to ViewModel to determine the value here
                    //and change the display in XAML.
                }
            } }
        */
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
