using PlaylistNameSort.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlaylistNameSort.Mvc.Tests
{
    public class SpotifyServiceTests
    {
        private string _token = "BQA-mPFhxqGFP1Ws-R8Kpmy5U509DMmg3tfq2SOLNQWwIWfDqGx_cUxaelE-MaUXc1ccvd02Fqm8cGELgsAbOXEmyHZ_H1HE-HPE1TKASR8DU5v5AqiMquCJk9qUF_g_ncwnJUAVf_mjcZfYiO5tfToZRZrJx8NnzeA";
                
        [Fact]
        public void Should_Get_User_Profile()
        {
            string expectedUserId = "rafael-miceli";
            string expectedUserName = "Rafael Miceli";

            var sut = new SpotifyService(_token);

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

            var sut = new SpotifyService(_token);

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

            List<string> playlistsId = new List<string> { "0ExbFrTy6ypLj9YYNMTnmd" };
            string userId = "rafael-miceli";

            var sut = new SpotifyService(_token);

            List<string> tracks = sut.GetTracksAndArtistsFromPlaylists(userId, playlistsId);

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

            List<string> playlistsId = new List<string> { "0hkdKTHJoshkeh4v6leqwZ", "3pKt7A5eKCSuD7AXIbQNQh" };
            string userId = "rafael-miceli";

            var sut = new SpotifyService(_token);

            List<string> tracks = sut.GetTracksAndArtistsFromPlaylists(userId, playlistsId);

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

            var sut = new SpotifyService(_token);

            List<string> resultTracks = sut.GenerateNewPlaylist(userDisplayName, tracks);

            Assert.Equal(expectedTracks, resultTracks);
        }
    }
}
