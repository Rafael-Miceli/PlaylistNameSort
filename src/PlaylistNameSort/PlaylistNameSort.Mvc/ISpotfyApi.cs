namespace PlaylistNameSort.Mvc
{
    public interface ISpotifyApi
    {
        string Token { get; set; }

        T GetSpotifyType<T>(string url);
    }
}