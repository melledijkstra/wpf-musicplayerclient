using System.Collections.Generic;
using Google.Protobuf;
using Grpc;

namespace MelonMusicPlayerWPF.MMP.Models
{
    internal class AlbumModel : BaseModel
    {
        private readonly long ID;
        private string Title;
        private List<SongModel> SongList;

        public AlbumModel(long id, string title, List<SongModel> songList)
        {
            ID = id;
            Title = title;
            SongList = songList;
        }

        public AlbumModel(Album grpcAlbum) : this(grpcAlbum.Id, grpcAlbum.Title, new List<SongModel>())
        {
            foreach (var song in grpcAlbum.SongList)
            {
                SongList.Add(new SongModel(song));
            }
        }

        public override string ToString()
        {
            return $"[{ID}] {Title} - songcount: {SongList.Count}";
        }

        public override IMessage ToProto()
        {
            Album album = new Album
            {
                Id = ID,
                Title = Title
            };

            foreach (SongModel songModel in SongList)
            {
                album.SongList.Add((Song)songModel.ToProto());
            }

            return album;
        }
    }
}