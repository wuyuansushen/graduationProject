using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using graduation.ViewModels;

namespace graduation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadPage : ContentPage
    {
        public DownloadViewModel DefaultViewModel { get; set; }
        public DownloadPage()
        {
            InitializeComponent();
            DefaultViewModel=new DownloadViewModel(@"主页");
        }
    }
}