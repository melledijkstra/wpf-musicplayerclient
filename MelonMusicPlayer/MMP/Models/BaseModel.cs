using Google.Protobuf;

namespace MelonMusicPlayerWPF.MMP.Models
{
    internal abstract class BaseModel : IProtoble
    {
        protected long ID;

        public abstract IMessage ToProto();
    }
}