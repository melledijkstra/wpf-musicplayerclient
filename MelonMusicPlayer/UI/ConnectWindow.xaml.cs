using System;
using System.Windows;
using System.Windows.Input;
using Grpc.Core;
using MelonMusicPlayerWPF.MMP;
using MelonMusicPlayerWPF.MVVM;

namespace MelonMusicPlayerWPF.UI
{
    public partial class ConnectWindow
    {
        private readonly App _application;

        public DelegateCommand ConnectCommand { get; }

        public ConnectWindow()
        {
            InitializeComponent();
            ConnectCommand = new DelegateCommand(Connect);
            DataContext = this;
            TbIP.Text = (string)Properties.Settings.Default["HostIP"];
            _application = (App) Application.Current;
            _application.melonPlayer.OnStateChange += OnStateChange;
        }

        private void OnStateChange(object sender, StateChangeEventArgs args)
        {
            TxtStatus.Text = $"State: {args.State}";
            switch (args.State)
            {
                case ChannelState.Ready:
                    // open dashboard window
                    _application.melonPlayer.OnStateChange -= OnStateChange; // unsubscribe the event
                    var dashboard = new DashboardWindow();
                    dashboard.Show();
                    Close();
                    return;
                case ChannelState.Connecting:
                case ChannelState.Idle:
                case ChannelState.TransientFailure:
                case ChannelState.Shutdown:
                    BtnConnect.IsEnabled = true;
                    Cursor = Cursors.Arrow;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Connect()
        {
            // disable button & show waiting cursor to prevent more calls
            Cursor = Cursors.Wait;
            BtnConnect.IsEnabled = false;
            // store current filled in IP
            Properties.Settings.Default["HostIP"] = TbIP.Text;
            Properties.Settings.Default.Save();
            // try to connect to the musicplayer
            TxtStatus.Text = "Connecting...";
            TxtStatus.Visibility = Visibility.Visible;
            _application.melonPlayer.Connect(
                (string)Properties.Settings.Default["HostIP"],
                (int)Properties.Settings.Default["Port"]
            );
        }

    }
}
