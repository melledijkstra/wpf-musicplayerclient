using Google.Protobuf;

namespace MelonMusicPlayerWPF.MMP.Models
{
    public interface IProtoble
    {
        IMessage ToProto();
    }
}