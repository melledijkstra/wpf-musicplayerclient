using System.ComponentModel;
using System.Runtime.CompilerServices;
using MelonMusicPlayerWPF.Annotations;

namespace MelonMusicPlayerWPF.MVVM
{
    class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
