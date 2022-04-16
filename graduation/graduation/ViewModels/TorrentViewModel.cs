﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.Windows.Input;
using System.IO;
using System.Net;

using System.Linq;
using System.Threading.Tasks;
using graduation.Models;
using graduation.Services;

namespace graduation.ViewModels
{
    public class TorrentViewModel:INotifyPropertyChanged
    {
        public ICommand OpenBrowserCommand { get;private set; }
        public string Title { get; set; }

        private const string RepoUrl = @"https://fiveelementgod.xyz/repo/";
        public ObservableCollection<Torrent> Items { get; set; }
        public TorrentViewModel()
        {
            OpenBrowserCommand = new Command(execute:
                async () =>
                {
                    await Browser.OpenAsync(RepoUrl);
                });
            Title = @"种子列表";
            Items = new ObservableCollection<Torrent>(ReadTorrents());
        }

        public List<Torrent> ReadTorrents()
        {
            var torrents = new List<Torrent>();
            using (var torrentContext=new TorrentContext())
            {
                torrents=torrentContext.Torrents.ToList();
            }
            torrents.Reverse();
            return torrents;
        }

        public async Task DeleteRecord(int id)
        {
            using(var torrentContext=new TorrentContext())
            {
                torrentContext.Remove<Torrent>(torrentContext.Find<Torrent>(id));
                await torrentContext.SaveChangesAsync();
            }
        }
        public void RefreshList()
        {
            Items = new ObservableCollection<Torrent>( ReadTorrents());
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Items"));
        }

        public void LocalDownload(string hash)
        {
            //Invoke DI Service.
            DependencyService.Get<ILocalDownloadService>().WriteTorrent(hash);
            /*Only download file in internal Storage
            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(RepoUrl + hash + @".torrent", Path.Combine(FileSystem.CacheDirectory, hash + @".torrent"));
            }
            */
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
