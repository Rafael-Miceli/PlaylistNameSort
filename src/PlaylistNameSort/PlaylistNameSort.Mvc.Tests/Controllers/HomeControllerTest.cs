using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PlaylistNameSort.Mvc;
using PlaylistNameSort.Mvc.Controllers;
using Xunit;
using PlaylistNameSort.Mvc.Models;

namespace PlaylistNameSort.Mvc.Tests.Controllers
{
    
    public class HomeControllerTest
    {
        [Fact]
        public void Should_Get_Correct_Spotify_Auth_Uri()
        {
            string expectedClientId = "c2b415ceb2694cb29b34088a69816aea";
            string expectedRedirectUri = "http://localhost";
            string expectedState = "";
            Scope expectedScope = Scope.PLAYLIST_MODIFY_PRIVATE;

            string expectedAuthUri = "https://accounts.spotify.com/en/authorize?client_id=" + expectedClientId +
                "&response_type=token&redirect_uri=" + expectedRedirectUri +
                "&state=&scope=" + expectedScope.GetStringAttribute(" ") +
                "&show_dialog=False";

            SpotifyAuthViewModel spotifyAuthViewModel = new SpotifyAuthViewModel(expectedClientId, expectedRedirectUri, expectedState, expectedScope);

            var sut = new HomeController(spotifyAuthViewModel);

            var result = sut.Index() as ViewResult;

            Assert.Equal(expectedAuthUri, result.ViewBag.AuthUri);
        }

        [Fact]
        public void Should_Get_Generated_Playlist_Model()
        {

        }        
    }
}
