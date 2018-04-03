using System.Windows;
using System.Windows.Controls;
using Grpc;

namespace MelonMusicPlayerWPF.UI
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow
    {
        private App _application;

        AlbumList albumList;

        public DashboardWindow()
        {
            InitializeComponent();
            System.Console.WriteLine("Dashboard View created");

            _application = (App) Application.Current;
            DataContext = _application;
            albumList = _application.melonPlayer.Client.RetrieveAlbumList(new MediaData());

            AlbumList.ItemsSource = albumList.AlbumList_;
        }

        private void PlaySong(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0) return;

            if (e.AddedItems[0] is Song song)
            {
                System.Console.WriteLine(song);
                _application.melonPlayer.Client.Play(new MediaControl
                {
                    State = MediaControl.Types.State.Play,
                    SongId = song.Id
                });
            }
        }

        private void ShowSongList(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0) return;
            
            if (e.AddedItems[0] is Album album)
            {
                System.Console.WriteLine(album);
                SongList.ItemsSource = album.SongList;
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _application.melonPlayer.Client.Play(new MediaControl {State = MediaControl.Types.State.Pause});
        }
    }
}