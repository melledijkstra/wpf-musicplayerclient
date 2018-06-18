using Google.Protobuf;
using MelonMusicPlayerWPF.MVVM;

namespace MelonMusicPlayerWPF.MMP.Models
{
    internal abstract class BaseModel : Observable, IProtoble
    {
        protected long ID;

        public abstract IMessage ToProto();
    }
}