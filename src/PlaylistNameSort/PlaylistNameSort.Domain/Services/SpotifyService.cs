using PlaylistNameSort.Domain.Interfaces;
using PlaylistNameSort.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace PlaylistNameSort.Domain.Services
{
    public class SpotifyService
    {
        private ISpotifyApi _spotifyApi;

        public SpotifyService(ISpotifyApi spotifyApi)
        {
            _spotifyApi = spotifyApi;
        }

        public List<string> GetPlaylistsName(string userId)
        {
            Playlists playLists = GetPlaylists(userId);

            List<string> playlistNames = new List<string>();

            foreach (var playlist in playLists.Items)
            {
                playlistNames.Add(playlist.Name);
            }    

            return playlistNames;
        }

        public SpotifyUser GetUserProfile()
        {
            string url = "https://api.spotify.com/v1/me";
            SpotifyUser spotifyUser = _spotifyApi.GetSpotifyType<SpotifyUser>(url);
            return spotifyUser;
        }

        public Playlists GetPlaylists(string userId)
        {
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists", userId);
            Playlists playlists = _spotifyApi.GetSpotifyType<Playlists>(url);            

            return playlists;
        }

        public List<string> GetTracksAndArtistsFromPlaylists(Playlists playlists)
        {
            List<string> listOfTracAndArtistsName = new List<string>();

            foreach (var playlist in playlists.Items)
            {
                string url = string.Format("https://api.spotify.com/v1/users/" + playlist.Owner.Id + "/playlists/" + playlist.Id + "/tracks");
                Tracks tracks = _spotifyApi.GetSpotifyType<Tracks>(url);

                if (tracks == null)
                    continue;

                foreach (var track in tracks.Items)
                {
                    string music = track.FullTrack.Name;
                    string artists = "";

                    foreach (var artist in track.FullTrack.Artists)
                    {
                        artists += artist.Name + " ";
                    }

                    listOfTracAndArtistsName.Add(music + " by " + artists);
                }
            }

            return listOfTracAndArtistsName;
        }        

        public List<string> GenerateNewPlaylist(string displayName, List<string> tracksAndArtists)
        {
            var trimedName = displayName.Replace(" ", "").ToLower();
            var distinctTracksAndArtists = tracksAndArtists.Distinct();          

            List<string> newPlaylistByNameLetter = new List<string>();

            foreach (var letter in trimedName)
            {
                string trackName = "";

                foreach (var track in distinctTracksAndArtists)
                {
                    if (track.ToLower().StartsWith(letter.ToString()))
                    {
                        trackName = track;                        
                        tracksAndArtists.Remove(track);
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(trackName))
                    newPlaylistByNameLetter.Add(trackName);
                else
                    newPlaylistByNameLetter.Add("Null and Void by Detroit");
            }            

            return newPlaylistByNameLetter;
        }
    }

    
}