using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using graduation.Models;
using graduation.ViewModels;

namespace graduation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TorrentPage : ContentPage
    {
        //public ObservableCollection<string> Items { get; set; }
        public TorrentViewModel DefaultViewModel { get; set; }
        public TorrentPage()
        {
            InitializeComponent();
            DefaultViewModel = new TorrentViewModel();
          /*  
            Items = new ObservableCollection<string>
            {
                "Item 1",
                "Item 2",
                "Item 3",
                "Item 4",
                "Item 5"
            };
          */
            //MyListView.ItemsSource = Items;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DefaultViewModel.RefreshList();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            Torrent torrentTapped = (Torrent)e.Item;
            //int torrentId= torrentTapped.Id;
            string action=await DisplayActionSheet("执行操作","取消",null,"下载","删除");
            if (action == "删除")
            {
                await DefaultViewModel.DeleteRecord(torrentTapped.Id);
                DefaultViewModel.RefreshList();
            }
            else if (action == "下载")
            {
                DefaultViewModel.LocalDownload(torrentTapped.Hash);
            }
            else { }
            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
