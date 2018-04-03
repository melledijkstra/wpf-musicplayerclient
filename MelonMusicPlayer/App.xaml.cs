using MelonMusicPlayerWPF.MMP;

namespace MelonMusicPlayerWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public MelonPlayer melonPlayer;

        public App()
        {
            melonPlayer = MelonPlayer.Instance;
        }
    }
}
