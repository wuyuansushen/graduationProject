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
                //await DisplayAlert(@"提示框Title", @"这是message信息", @"这个是按钮信息");

                //It will return null if you click Cancel.
                string deletePasswd = await DisplayPromptAsync(@"删除验证",
                    @"请输入云端删除密码。密码验证通过后，云端方可删除",
                    accept: @"确定", @"取消",
                    @"请输入删除密码");
                //Console.WriteLine(deletePasswd);
                if (deletePasswd != null)
                {

                    var deleteResultBool = await DefaultViewModel.SendDeleteRequest(deletePasswd, torrentTapped.Hash);
                    if (deleteResultBool)
                    {
                        await DefaultViewModel.DeleteRecord(torrentTapped.Id);
                        await DisplayAlert(@"删除成功", @"输入密码正确，云端种子文件已删除。", @"确定");
                        DefaultViewModel.RefreshList();
                    }
                    else
                    {
                        await DisplayAlert(@"删除失败", @"您输入的删除密码有误，请比对后重新输入。", @"确定");
                    }
                }
                else { }
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
