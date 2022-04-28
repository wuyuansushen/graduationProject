using System;
using System.Collections.Generic;
using System.Text;
using Xamarin;
using Xamarin.Forms;

using System.Text.Json;
using System.Text.Json.Serialization;

using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Windows.Input;

using graduation.Models;
using graduation.Data;

namespace graduation.ViewModels
{
    public class DownloadViewModel : INotifyPropertyChanged
    {
        public string Title
        {
            get; set;
        }
        public string ForDebug
        {
            get; set;
        }

        public ICommand DownloadCommand { get; private set; }
        private string _token = String.Empty;
        private string _hash = String.Empty;
        //private bool _isEditing = default;
        private readonly HttpClient _httpClient;


        private const string _torrentUrl = @"https://fiveelementgod.xyz/downloadtorrent/";

        public async Task<HttpContent> StartTorrentDownload(HttpClient httpClient, string torrentUrl, string tokenValue, string hashValue)
        {
            var payload = new HashRequest()
            {
                Token = tokenValue,
                Hash = hashValue
            };
            var jsonPayload = JsonSerializer.Serialize(payload);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var httpResponse = await httpClient.PostAsync(torrentUrl, httpContent);
            if (httpResponse.StatusCode == HttpStatusCode.OK)
            {
                await AddRecord(hashValue);
            }
            else { }
            return httpResponse.Content;
        }

        public async Task AddRecord(string torrentHash)
        {
            DateTime dateNow = DateTime.Now;
            var torrent = new Torrent()
            {
                Date = dateNow.ToString(),
                Hash = torrentHash
            };
            using (var torrentContext = new TorrentContext())
            {
                torrentContext.Add(torrent);
                await torrentContext.SaveChangesAsync();
            }
        }
        public DownloadViewModel(string title = "下载页")
        {
            Title = title;
            ForDebug = "提示信息输出处";
            _httpClient = new HttpClient();
            //PropertyChanged += OnPersonEditPropertyChanged;
            //PropertyChanged += OnPersonEditPropertyChanged2;
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
                execute: async () =>
                {
                    //Call to this Command's canExecute method.
                    //(DownloadCommand as Command).ChangeCanExecute();
                    ForDebug = @"发送至服务器处理中...";
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForDebug"));
                    var responseContent = await StartTorrentDownload(_httpClient, _torrentUrl, Token, Hash);
                    ForDebug = await responseContent.ReadAsStringAsync();
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ForDebug"));
                    Hash = String.Empty;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Hash"));
                    await Shell.Current.DisplayAlert(@"下载成功", @"Hash转种子文件下载成功，Hash输入框字段已清除。", @"知道了");
                }
                );
        }
        /*
        void OnPersonEditPropertyChanged2(object sender, PropertyChangedEventArgs args)
        {
            //(DownloadCommand as Command).ChangeCanExecute();
            Title="+++++++++Second add delegate";
        }
        void OnPersonEditPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            //(DownloadCommand as Command).ChangeCanExecute();
            Token="******Fiest add delegate";
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
                /* Debug usage
                Title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
                //Console.WriteLine(PropertyChanged.GetInvocationList().Length);
                var handleList = PropertyChanged.GetInvocationList();
                */
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
