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
            DefaultViewModel.OnAppearing();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            Torrent torrentTapped = (Torrent)e.Item;
            int torrentId= torrentTapped.Id;
            await DisplayAlert("删除提示", $"确定要删除Hash为{torrentTapped.Hash}的种子吗?", "OK");

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }
    }
}
