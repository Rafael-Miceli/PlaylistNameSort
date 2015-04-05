using PlaylistNameSort.Mvc.Models;
using System;
using Xunit;

namespace PlaylistNameSort.Mvc.Tests.Unit.Models
{
    public class SpotifyAuthViewModelTest
    {
        [Fact, Trait("Category", "Unit")]
        public void Should_Format_Correct_Spotify_Auth_Uri()
        {
            string expectedClientId = "c2b415ceb2694cb29b34088a69816aea";            
            string expectedRedirectUri = "http://localhost";
            string expectedState = "";
            Scope expectedScope = Scope.PLAYLIST_MODIFY_PRIVATE;

            string expectedAuthUri = "https://accounts.spotify.com/en/authorize?client_id=" + expectedClientId +
                "&response_type=token&redirect_uri=" + expectedRedirectUri +
                "&state=&scope=" + expectedScope.GetStringAttribute(" ") +
                "&show_dialog=true";

            SpotifyAuthViewModel sut = new SpotifyAuthViewModel(expectedClientId, expectedRedirectUri, expectedState, expectedScope);

            var result = sut.GetAuthUri();

            Assert.Equal(expectedAuthUri, result);
        }
    }
}
