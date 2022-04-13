using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Windows.Input;

namespace graduation.ViewModels
{
    public class TorrentViewModel:INotifyPropertyChanged
    {
        public ICommand OpenBrowserCommand { get;private set; }
        private const string RepoUrl = @"https://fiveelementgod.xyz/repo/";
        public TorrentViewModel()
        {
            OpenBrowserCommand = new Command(execute:
                async () =>
                {
                    await Browser.OpenAsync(RepoUrl);
                });
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
