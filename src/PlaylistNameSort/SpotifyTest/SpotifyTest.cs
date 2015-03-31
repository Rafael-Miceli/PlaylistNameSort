using PlayListNameSort.Domain;
using System;
using Xunit;

namespace PlaylistNameSortTests
{
    
    public class SpotifyTest
    {
        [Fact]
        public void Test_Spotify_Connection()
        {
            var spotifyAuthentication = new SpotifyAuthentication();

            string username = "rafaelmiceli";
            string password = "12345678";
            var isLoggedIn = spotifyAuthentication.Login(username, password);

            Assert.True(isLoggedIn);
        }

        [Fact]
        public void Test_Spotify_Authorization_Page_Call()
        {
            var spotifyAuthentication = new SpotifyAuthentication();

            spotifyAuthentication.Authorize();
        }
    }
}
