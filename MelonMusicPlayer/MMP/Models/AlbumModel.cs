using System.Collections.Generic;

namespace MelonMusicPlayerWPF.MMP.Models
{
    internal class AlbumModel : BaseModel
    {
        private int ID;
        private string Title;
        private List<SongModel> SongList;

        public AlbumModel(int id, string title, List<SongModel> songList)
        {
            ID = id;
            Title = title;
            SongList = songList;
        }

        public override string ToString()
        {
            return $"[{ID}] {Title} - songcount: {SongList.Count}";
        }
    }
}