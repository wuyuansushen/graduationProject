﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Forms;

using System.ComponentModel;
using System.Windows.Input;

namespace graduation.ViewModels
{
    public class DownloadViewModel:INotifyPropertyChanged
    {
        public string Title
        {
            get; set;
        }

        public ICommand DownloadCommand { get; private set; }
        private string _token=String.Empty;
        private string _hash=String.Empty;
        //private bool _isEditing = default;
        
        public DownloadViewModel(string title="下载页")
        {
            Title =title;
            //PropertyChanged += OnPersonEditPropertyChanged;
            DownloadCommand = new Command(
                /*
                canExecute: () =>
                {
                    //In-code is false. It is true at start.
                    _isEditing = !_isEditing;
                    return _isEditing;
                }
                ,
                */
                execute: () =>
                {
                    //Call to this Command's canExecute method.
                    //(DownloadCommand as Command).ChangeCanExecute();
                }
                );
        }
        /*
        void OnPersonEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            (DownloadCommand as Command).ChangeCanExecute();
        }
        */
        public string Token
        {
            get { return _token; }
            set
            {
                _token = value;
            }
        }
        public string Hash
        {
            get { return _hash; }
            set
            {
                _hash = value;
                //Title = value;
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
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
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
