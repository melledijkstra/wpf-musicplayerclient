using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc;
using Grpc.Core;
using MelonMusicPlayerWPF.MMP.Models;
using DateTime = System.DateTime;

namespace MelonMusicPlayerWPF.MMP
{
    public class MelonPlayer
    {
        public event OnStateChange OnStateChange;

        private List<AlbumModel> _albums;
        private Channel _channel;
        public MusicPlayer.MusicPlayerClient Client;

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
                Client = new MusicPlayer.MusicPlayerClient(_channel);
            }
        }

        private async void ListenForStateChange(ChannelState state, Channel channel)
        {
            while (true)
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