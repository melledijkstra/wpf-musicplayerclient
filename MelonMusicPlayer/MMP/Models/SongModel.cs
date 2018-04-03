using Google.Protobuf;
using Grpc;

namespace MelonMusicPlayerWPF.MMP.Models
{
    internal class SongModel : BaseModel
    {
        private string Title;
        private long AlbumID;
        private long Duration;

        public SongModel(long id, string title, long albumId, long duration)
        {
            ID = id;
            Title = title;
            AlbumID = albumId;
            Duration = duration;
        }

        public SongModel(Song song) : this(song.Id, song.Title, song.AlbumId, song.Duration)
        {

        }

        public override string ToString()
        {
            return $"[{ID}] - {Title} - {Duration}";
        }

        public override IMessage ToProto()
        {
            return new Song
            {
                Id = ID,
                AlbumId = AlbumID,
                Duration = Duration,
                Title = Title
            };
        }
    }
}