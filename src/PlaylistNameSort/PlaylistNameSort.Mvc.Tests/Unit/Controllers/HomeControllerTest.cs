using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using PlaylistNameSort.Mvc.Controllers;
using Xunit;
using PlaylistNameSort.Mvc.Models;
using Moq;

namespace PlaylistNameSort.Mvc.Tests.Unit.Controllers
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
                "&show_dialog=true";

            SpotifyAuthViewModel spotifyAuthViewModel = new SpotifyAuthViewModel(expectedClientId, expectedRedirectUri, expectedState, expectedScope);

            var sut = new HomeController(spotifyAuthViewModel, null);

            var result = sut.Index() as ViewResult;

            Assert.Equal(expectedAuthUri, result.ViewBag.AuthUri);
        }

        [Fact, Trait("Category", "Unit")]
        public void Should_Generate_New_Playlist_When_Token_Is_Not_Null()
        {
            SpotifyAuthViewModel spotifyAuthViewModel = null;
            SpotifyUser spotifyUser = new SpotifyUser { DisplayName = "Rafael Miceli", UserId = "rafael-miceli" }; 
            Playlists playlists = BuildExpectedPlaylists();
            Tracks tracks = BuildExpectedTracks();

            var mockedSpotifyApi = new Mock<ISpotifyApi> { DefaultValue = DefaultValue.Mock };
            mockedSpotifyApi.Setup(x => x.GetSpotifyType<SpotifyUser>(It.IsAny<string>())).Returns(spotifyUser);
            mockedSpotifyApi.Setup(x => x.GetSpotifyType<Playlists>(It.IsAny<string>())).Returns(playlists);
            mockedSpotifyApi.Setup(x => x.GetSpotifyType<Tracks>(It.IsAny<string>())).Returns(tracks);

            var sut = new HomeController(spotifyAuthViewModel, mockedSpotifyApi.Object);

            var result = (sut.GenerateNameSortList("A-Valid-Token", null) as ViewResult).Model;

            Assert.True((result as List<string>).Count > 0);
        }        

        private Playlists BuildExpectedPlaylists()
        {
            return new Playlists
            {
                Items = new List<Playlist>
                {
                    new Playlist
                    {
                        Name = "Me and Pri Pop",
                        Owner = new PublicProfile
                        {
                            Id = "rafael-micel"
                        }
                    },
                    new Playlist
                    {
                        Name = "Intense Studying",
                        Owner = new PublicProfile
                        {
                            Id = "spotify"
                        }
                    },
                    new Playlist
                    {
                        Name = "Me and Pri",
                        Owner = new PublicProfile
                        {
                            Id = "rafael-micel"
                        }
                    },
                    new Playlist
                    {
                        Name = "Date Night",
                        Owner = new PublicProfile
                        {
                            Id = "spotify"
                        }
                    },
                    new Playlist
                    {
                        Name = "Sexual Healing",
                        Owner = new PublicProfile
                        {
                            Id = "spotify"
                        }
                    }
                }
            };
        }

        private Tracks BuildExpectedTracks()
        {
            return new Tracks
            {
                Items = new List<Track>
                {
                    new Track
                    {
                        FullTrack = new FullTrack
                        {
                            Name = "No Ordinary Love - Remastered",
                            Artists = new List<Artist>
                            {
                                new Artist
                                {
                                    Name = "Sade"
                                }
                            }
                        }
                    },
                    new Track
                    {
                        FullTrack = new FullTrack
                        {
                            Name = "Paradise - Remastered Version",
                            Artists = new List<Artist>
                            {
                                new Artist
                                {
                                    Name = "Sade"
                                }
                            }
                        }
                    }
                }
            };
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
