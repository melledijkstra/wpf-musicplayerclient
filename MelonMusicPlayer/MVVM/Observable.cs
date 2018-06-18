using System.ComponentModel;
using System.Runtime.CompilerServices;
using MelonMusicPlayerWPF.Annotations;

namespace MelonMusicPlayerWPF.MVVM
{
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
