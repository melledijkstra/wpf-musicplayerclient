using System;
using System.Windows;
using System.Windows.Controls;
using Google.Protobuf.Collections;
using Grpc;
using MelonMusicPlayerWPF.MMP;

namespace MelonMusicPlayerWPF.MVVM
{
    public class DashboardViewModel : Observable
    {
        private readonly MelonPlayer _melonPlayer;

        public DelegateCommand PlayCommand { get; }

        public String LblTest => _melonPlayer.ToString();

        private AlbumList _albumList;

        public RepeatedField<Album> AlbumList => _albumList.AlbumList_;

        public DashboardViewModel()
        {
            _melonPlayer = ((App) Application.Current).melonPlayer;
            PlayCommand = new DelegateCommand(PlayPause);
            _albumList = _melonPlayer.MPlayerClient.RetrieveAlbumList(new MediaData());
        }

        private void PlayPause()
        {
            _melonPlayer.MPlayerClient.Play(new MediaControl() {State = MediaControl.Types.State.Pause});
        }

        public void PlaySong(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0) return;

            if (e.AddedItems[0] is Song song)
            {
                Console.WriteLine(song);
                _melonPlayer.MPlayerClient.Play(new MediaControl
                {
                    State = MediaControl.Types.State.Play,
                    SongId = song.Id
                });
            }
        }

        public void ShowSongList(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0) return;

            if (e.AddedItems[0] is Album album)
            {
                Console.WriteLine(album);
                //SongList.ItemsSource = album.SongList;
            }
        }
    }
}