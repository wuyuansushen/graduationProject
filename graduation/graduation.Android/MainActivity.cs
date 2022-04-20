using System;
using graduation.Services;
using System.Net;
using System.IO;
using System.Text;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using graduation.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(WriteTorrentService))]
namespace graduation.Droid
{
    public class WriteTorrentService : ILocalDownloadService
    {
        private const string RepoUrl = @"https://fiveelementgod.xyz/repo/";

        [Obsolete]
        void ILocalDownloadService.WriteTorrent(string hash)
        {
            using var webClient = new WebClient();
            string fileName = Path.Combine(
                Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads, hash + @".torrent");
            string urlAddress = RepoUrl + hash + @".torrent";
            try
            {

                /*
                webClient.DownloadFile(RepoUrl + hash + @".torrent", Path.Combine(
                    Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads, hash + @".torrent"));
                */
                webClient.DownloadFile(urlAddress, fileName);

                /*
                            string text = "11111";
            byte[] data = Encoding.ASCII.GetBytes(text);
           
            string DownloadsPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Android.OS.Environment.DirectoryDownloads);

            string filePath = Path.Combine(DownloadsPath, hash);
            File.WriteAllBytes(filePath, data);
                */
            }
            catch (Exception ex)
            {
                // Console.WriteLine(fileName);
                Console.WriteLine(ex.Message);
            }
        }
    }
    [Activity(Label = "graduation", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}