using System;
using System.Windows;
using System.Windows.Input;
using Grpc.Core;
using MelonMusicPlayerWPF.MMP;
using MelonMusicPlayerWPF.MVVM;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace MelonMusicPlayerWPF.UI
{
    public partial class ConnectWindow
    {
        private readonly App _application;
        private readonly Notifier _notifier;

        public DelegateCommand ConnectCommand { get; }

        public ConnectWindow()
        {
            InitializeComponent();
            ConnectCommand = new DelegateCommand(Connect);
            DataContext = this;
            TbIP.Text = (string)Properties.Settings.Default["HostIP"];
            _application = (App) Application.Current;
            _application.melonPlayer.OnStateChange += OnStateChange;

            // test notifications
            _notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    Application.Current.MainWindow,
                    Corner.BottomCenter,
                    0,
                    0
                );

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    TimeSpan.FromSeconds(3),
                    MaximumNotificationCount.FromCount(5)
                );

                cfg.Dispatcher = Application.Current.Dispatcher;
            });
        }

        private void OnStateChange(object sender, StateChangeEventArgs args)
        {
            _notifier.ClearMessages();
            _notifier.ShowInformation($"State changed to {args.State}");
            switch (args.State)
            {
                case ChannelState.Ready:
                    // open dashboard window
                    _notifier.Dispose();
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
            Console.WriteLine("Connecting...");
            _application.melonPlayer.Connect(
                (string)Properties.Settings.Default["HostIP"],
                (int)Properties.Settings.Default["Port"]
            );
        }

    }
}
