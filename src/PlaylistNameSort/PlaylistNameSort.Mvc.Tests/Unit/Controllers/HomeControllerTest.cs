using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PlaylistNameSort.Mvc;
using PlaylistNameSort.Mvc.Controllers;
using Xunit;
using PlaylistNameSort.Mvc.Models;
using System.Threading.Tasks;
using Moq;

namespace PlaylistNameSort.Mvc.Tests.Controllers
{
    
    public class HomeControllerTest
    {
        [Fact, Trait("Category", "Unit")]
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

            var sut = new HomeController(spotifyAuthViewModel, null);

            var result = sut.Index() as ViewResult;

            Assert.Equal(expectedAuthUri, result.ViewBag.AuthUri);
        }

        [Fact, Trait("Category", "Unit")]
        public void Should_Get_Error_View_When_error_Parameter_Is_Not_Null()
        {
            string expectedViewName = "Error";

            SpotifyAuthViewModel spotifyAuthViewModel = null;
            var spotifyApi = new SpotifyApi();

            var sut = new HomeController(spotifyAuthViewModel, spotifyApi);

            var result = sut.GenerateNameSortList(null, "Erro") as ViewResult;

            Assert.Equal(expectedViewName, result.ViewName);
        }

        [Fact, Trait("Category", "Unit")]
        public void Should_Get_Error_View_When_Token_Is_Not_Valid()
        {
            string expectedViewName = "Error";

            SpotifyAuthViewModel spotifyAuthViewModel = null;
            var spotifyApi = new SpotifyApi();

            var sut = new HomeController(spotifyAuthViewModel, spotifyApi);

            var result = sut.GenerateNameSortList("A-Invalid-Token", null) as ViewResult;

            Assert.Equal(expectedViewName, result.ViewName);
        }

        [Fact, Trait("Category", "Unit")]
        public void Should_Get_Empty_View_When_Token_Is_Null()
        {
            string expectedViewName = "";

            SpotifyAuthViewModel spotifyAuthViewModel = null;
            var spotifyApi = new SpotifyApi();

            var sut = new HomeController(spotifyAuthViewModel, spotifyApi);

            var result = sut.GenerateNameSortList(null, null) as ViewResult;

            Assert.Equal(expectedViewName, result.ViewName);
        }
    }
}
