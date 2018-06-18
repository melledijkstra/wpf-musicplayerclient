using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc;
using Grpc.Core;
using MelonMusicPlayerWPF.MMP.Models;
using MelonMusicPlayerWPF.MVVM;
using DateTime = System.DateTime;

namespace MelonMusicPlayerWPF.MMP
{
    public class MelonPlayer : Observable
    {
        public event OnStateChange OnStateChange;

        private List<AlbumModel> _albums;
        private Channel _channel;
        public MusicPlayer.MusicPlayerClient MPlayerClient;

        /// <summary>
        /// Flag to keep checking if the state changes
        /// </summary>
        private bool _keepStateListening = true;

        private static MelonPlayer _instance;
        public static MelonPlayer Instance => _instance ?? (_instance = new MelonPlayer());

        /// <summary>
        /// Prevents a default instance of the <see cref="MelonPlayer"/> class from being created.
        /// Use <see cref="Instance"/> for retrieving the singleton instance of this class
        /// </summary>
        private MelonPlayer() {}

        public async void Connect(string hostIp, int hostPort)
        {
            Console.WriteLine(@"Connecting to {0}:{1}", hostIp, hostPort);
            // create new communication channel for grpc when IP is not the same as already connecting channel
            if (_channel == null || !_channel.Target.Split(':')[0].Equals(hostIp))
            {
                _channel = new Channel(hostIp, hostPort, ChannelCredentials.Insecure);
            }

            ListenForStateChange(_channel.State, _channel);
            try
            {
                // try to connect within deadline (5 seconds)
                await _channel.ConnectAsync(DateTime.UtcNow.AddSeconds(5));
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine(@"Not connected within time!");
            }

            if (_channel.State == ChannelState.Ready)
            {
                MPlayerClient = new MusicPlayer.MusicPlayerClient(_channel);
            }
        }

        private async void ListenForStateChange(ChannelState state, Channel channel)
        {
            while (_keepStateListening)
            {
                await channel.WaitForStateChangedAsync(state);
                var changedState = channel.State;
                OnStateChange?.Invoke(this, new StateChangeEventArgs(changedState));
                state = changedState;
            }
        }
    }

    public delegate void OnStateChange(object sender, StateChangeEventArgs args);

    public class StateChangeEventArgs : EventArgs
    {
        public ChannelState State;

        public StateChangeEventArgs(ChannelState state)
        {
            State = state;
        }
    }
}