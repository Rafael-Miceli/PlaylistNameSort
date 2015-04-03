using Newtonsoft.Json;
using PlaylistNameSort.Mvc.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PlaylistNameSort.Mvc
{
    public class SpotifyService
    {
        private string _token;

        public SpotifyService(string token)
        {
            _token = token;
        }

        public List<string> GetPlaylistsName(string userId)
        {
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists", userId);
            Playlists playLists = GetSpotifyType<Playlists>(url);

            List<string> playlistNames = new List<string>();

            foreach (var playlist in playLists.Items)
            {
                playlistNames.Add(playlist.Name);
            }    

            return playlistNames;
        }

        public List<string> GetPlaylistsIds(string userId)
        {
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists", userId);
            Playlists playLists = GetSpotifyType<Playlists>(url);

            List<string> playlistIds = new List<string>();

            foreach (var playlist in playLists.Items)
            {
                playlistIds.Add(playlist.Id);
            }

            return playlistIds;
        }

        public Playlists GetPlaylists(string userId)
        {
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists", userId);
            Playlists playlists = GetSpotifyType<Playlists>(url);            

            return playlists;
        }

        public SpotifyUser GetUserProfile()
        {
            string url = "https://api.spotify.com/v1/me";
            SpotifyUser spotifyUser = GetSpotifyType<SpotifyUser>(url);
            return spotifyUser;
        }

        public List<string> GetTracksAndArtistsFromPlaylists(string ownerId, List<string> playlistsId)
        {
            List<string> listOfTracAndArtistsName = new List<string>();

            foreach (var playlistId in playlistsId)
            {
                string url = string.Format("https://api.spotify.com/v1/users/" + ownerId + "/playlists/" + playlistId + "/tracks");
                Tracks tracks = GetSpotifyType<Tracks>(url);

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

        public List<string> GetTracksAndArtistsFromPlaylists(Playlists playlists)
        {
            List<string> listOfTracAndArtistsName = new List<string>();

            foreach (var playlist in playlists.Items)
            {
                string url = string.Format("https://api.spotify.com/v1/users/" + playlist.Owner.Id + "/playlists/" + playlist.Id + "/tracks");
                Tracks tracks = GetSpotifyType<Tracks>(url);

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

        private T GetSpotifyType<T>(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Method = "GET";
                request.Headers.Set("Authorization", "Bearer" + " " + _token);
                request.ContentType = "application/json; charset=utf-8";

                T type = default(T);

                using (WebResponse response = request.GetResponse())
                {
                    using (Stream dataStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(dataStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            type = JsonConvert.DeserializeObject<T>(responseFromServer);
                        }
                    }
                }
                return type;
            }
            catch (WebException ex)
            {
                return default(T);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<string> GenerateNewPlaylist(string displayName, List<string> tracksAndArtists)
        {
            var trimedName = displayName.Replace(" ", "").ToLower();            

            List<string> newPlaylistByNameLetter = new List<string>();

            foreach (var letter in trimedName)
            {
                string trackName = "";

                foreach (var track in tracksAndArtists)
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