using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.Windows.Input;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Net;
using System.Net.Http;

using System.Linq;
using System.Threading.Tasks;
using graduation.Models;
using graduation.Services;
using graduation.Data;

namespace graduation.ViewModels
{
    public class TorrentViewModel:INotifyPropertyChanged
    {
        public class DeleteResult
        {
            public bool Successed { get; set; }
        }
        public ICommand OpenBrowserCommand { get;private set; }
        public string Title { get; set; }

        private const string RepoUrl = @"https://fiveelementgod.xyz/repo/";
        private const string DeleteUrl = @"https://fiveelementgod.xyz/deletetorrent/";

        public ObservableCollection<Torrent> Items { get; set; }
        public TorrentViewModel()
        {
            OpenBrowserCommand = new Command(execute:
                async () =>
                {
                    await Browser.OpenAsync(RepoUrl);

                    /*
                    var shell = Shell.Current;
                    await shell.GoToAsync(@"//graduation/firsttab/download");
                    */
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

        public async Task<bool> SendDeleteRequest(string deletePasswd, string torrentHash)
        {
            var httpClient =new HttpClient();
            var payload = new HashRequest
            {
                Token = deletePasswd,
                Hash = torrentHash,
            };
            var jsonPayload = JsonSerializer.Serialize(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(DeleteUrl, httpContent);
            string deleteResult = await httpResponse.Content.ReadAsStringAsync();
            var resultBool=(JsonSerializer.Deserialize<DeleteResult>(deleteResult)).Successed;
            return resultBool;
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
