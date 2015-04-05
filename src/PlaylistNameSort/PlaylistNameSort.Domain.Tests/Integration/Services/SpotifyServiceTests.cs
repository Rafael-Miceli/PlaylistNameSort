using PlaylistNameSort.Domain.Models;
using PlaylistNameSort.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlaylistNameSort.Domain.Tests.Integration.Services
{
    public class SpotifyServiceTests
    {
        private string _token = "Place-A-Token-Here";             
        
        [Fact]
        public void Should_Get_User_Profile()
        {
            string expectedUserId = "rafael-miceli";
            string expectedUserName = "Rafael Miceli";

            var spotifyApi = new SpotifyApi(_token);
            var sut = new SpotifyService(spotifyApi);

            SpotifyUser spotifyUser = sut.GetUserProfile();

            Assert.Equal(expectedUserName, spotifyUser.DisplayName);
            Assert.Equal(expectedUserId, spotifyUser.UserId);
        }

        [Fact]
        public void Should_Get_User_Playlists()
        {
            string userId = "rafael-miceli";
            List<string> expectedPlaylistNames = new List<string>
            {
                "Me and Pri Pop",
                "Intense Studying",
                "Me and Pri",
                "Date Night",
                "Sexual Healing",
                "Pure Seduction",
                "Cozy Time",
                "Jazzy Romance",
                "Classical Essentials",
                "Windows Media Player",
                "iTunes"
            };

            var spotifyApi = new SpotifyApi(_token);
            var sut = new SpotifyService(spotifyApi);

            List<string> playlistNames = sut.GetPlaylistsName(userId);

            Assert.Equal(expectedPlaylistNames, playlistNames);
        }

        [Fact]
        public void Should_Get_Tracks_From_One_Playlist()
        {
            List<string> expectedTracks = new List<string>
            {
                "No Ordinary Love - Remastered by Sade ",
                "Paradise - Remastered Version by Sade ",
                "Smooth Operator - Remastered by Sade ",
                "The Sweetest Taboo - Remastered by Sade "
            };

            Playlists playlists = new Playlists
            {
                Items = new List<Playlist>
                {
                    new Playlist {
                        Id = "0ExbFrTy6ypLj9YYNMTnmd",
                        Owner = new PublicProfile
                        {
                            Id = "spotify"
                        }
                    }
                }
            };

            var spotifyApi = new SpotifyApi(_token);
            var sut = new SpotifyService(spotifyApi);

            List<string> tracks = sut.GetTracksAndArtistsFromPlaylists(playlists);

            Assert.Equal(expectedTracks, tracks);
        }

        [Fact]
        public void Should_Get_Tracks_From_More_Than_One_Playlists()
        {
            List<string> expectedTracks = new List<string>
            {
                "No Ordinary Love - Remastered by Sade ",
                "Paradise - Remastered Version by Sade ",
                "Smooth Operator - Remastered by Sade ",
                "The Sweetest Taboo - Remastered by Sade ",
                "Aesthetics Of Hate by Machine Head ",
                "Day One by Hans Zimmer "
            };

            Playlists playlists = new Playlists
            {
                Items = new List<Playlist>
                {
                    new Playlist {
                        Id = "0ExbFrTy6ypLj9YYNMTnmd",
                        Owner = new PublicProfile
                        {
                            Id = "spotify"
                        }
                    },
                    new Playlist {
                        Id = "0hkdKTHJoshkeh4v6leqwZ",
                        Owner = new PublicProfile
                        {
                            Id = "rafael-miceli"
                        }
                    }
                }
            };

            var spotifyApi = new SpotifyApi(_token);
            var sut = new SpotifyService(spotifyApi);

            List<string> tracks = sut.GetTracksAndArtistsFromPlaylists(playlists);

            Assert.Equal(expectedTracks, tracks);
        }

        [Fact]
        public void Should_Generate_New_Playlist_From_User_DisplayName_Letters()
        {
            List<string> expectedTracks = new List<string>
            {
                "R - by b",                  //R
                "Au - by dog",               //a
                "Fireworks - by Katy Perry", //f
                "Null and Void by Detroit",  //a
                "ember - by fire",           //e
                "lorota - by lie",           //l  
                "Null and Void by Detroit",  //M
                "ill - by sickness",         //i
                "Null and Void by Detroit",  //c
                "ever - by never",           //e
                "Null and Void by Detroit",  //l
                "Null and Void by Detroit"   //i     
            };

            List<string> tracks = new List<string>
            {
                "R - by b",
                "D - by D",
                "S - by E",
                "RA - by u",
                "Au - by dog",
                "ill - by sickness",
                "lorota - by lie",
                "ember - by fire",
                "ever - by never",
                "Fireworks - by Katy Perry"
            };

            string userDisplayName = "Rafael Miceli";

            var spotifyApi = new SpotifyApi(_token);
            var sut = new SpotifyService(spotifyApi);

            List<string> resultTracks = sut.GenerateNewPlaylist(userDisplayName, tracks);

            Assert.Equal(expectedTracks, resultTracks);
        }
    }
}
