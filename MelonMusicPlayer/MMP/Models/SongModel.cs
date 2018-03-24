namespace MelonMusicPlayerWPF.MMP.Models
{
    internal class SongModel : BaseModel
    {
        private int ID;
        private string Title;
        private int AlbumID;
        private int Duration;

        public SongModel(int id, string title, int albumId, int duration)
        {
            ID = id;
            Title = title;
            AlbumID = albumId;
            Duration = duration;
        }

        public override string ToString()
        {
            return $"[{ID}] - {Title} - {Duration}";
        }
    }
}