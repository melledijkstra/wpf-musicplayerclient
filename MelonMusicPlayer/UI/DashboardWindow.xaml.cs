using System.Windows;
using System.Windows.Controls;
using Grpc;
using MelonMusicPlayerWPF.MVVM;

namespace MelonMusicPlayerWPF.UI
{
    /// <summary>
    /// Interaction logic for DashboardWindow.xaml
    /// </summary>
    public partial class DashboardWindow
    {
        private DashboardViewModel VM;

        public DashboardWindow()
        {
            InitializeComponent();
            System.Console.WriteLine("Dashboard View created");
            VM = new DashboardViewModel();
            DataContext = VM;
        }

        private void ShowSongList(object sender, SelectionChangedEventArgs e)
        {
            VM.ShowSongList(sender, e);
        }

        private void PlaySong(object sender, SelectionChangedEventArgs e)
        {
            VM.PlaySong(sender, e);
        }
    }
}