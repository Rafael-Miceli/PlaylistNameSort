using Moq;
using PlaylistNameSort.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlaylistNameSort.Mvc.Tests.Unit
{
    public class SpotifyServiceTests
    {
        [Fact, Trait("Category", "Unit")]
        public void Should_Get_User_Profile()
        {
            string expectedUserId = "rafael-miceli";
            string expectedUserName = "Rafael Miceli";

            SpotifyUser expectedSpotifyUser = new SpotifyUser
            {
                DisplayName = expectedUserName,
                UserId = expectedUserId
            };

            var mockedSpotifyApi = new Mock<ISpotifyApi>();
            mockedSpotifyApi.Setup(x => x.GetSpotifyType<SpotifyUser>(It.IsAny<string>())).Returns(expectedSpotifyUser);

            var sut = new SpotifyService(mockedSpotifyApi.Object);

            SpotifyUser spotifyUser = sut.GetUserProfile();

            Assert.Equal(expectedUserName, spotifyUser.DisplayName);
            Assert.Equal(expectedUserId, spotifyUser.UserId);
        }

        [Fact, Trait("Category", "Unit")]
        public void Should_Get_User_Playlists()
        {
            string userId = "rafael-miceli";
            Playlists expectedPlaylists = BuildExpectedPlaylists();

            var mockedSpotifyApi = new Mock<ISpotifyApi>();
            mockedSpotifyApi.Setup(x => x.GetSpotifyType<Playlists>(It.IsAny<string>())).Returns(expectedPlaylists);

            var sut = new SpotifyService(mockedSpotifyApi.Object);

            Playlists playlistNames = sut.GetPlaylists(userId);

            Assert.Equal(expectedPlaylists, playlistNames);
        }

        private Playlists BuildExpectedPlaylists()
        {
            return new Playlists
            {
                Items = new List<Playlist>
                {
                    new Playlist
                    {
                        Name = "Me and Pri Pop"
                    },
                    new Playlist
                    {
                        Name = "Intense Studying"
                    },
                    new Playlist
                    {
                        Name = "Me and Pri"
                    },
                    new Playlist
                    {
                        Name = "Date Night"
                    },
                    new Playlist
                    {
                        Name = "Sexual Healing"
                    }
                }
            };
        }

        [Fact, Trait("Category", "Unit")]
        public void Should_Get_Tracks_From_One_Playlist()
        {
            List<string> expectedTracks = new List<string>
            {
                "No Ordinary Love - Remastered by Sade ",
                "Paradise - Remastered Version by Sade "
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

            Tracks tracksToReturn = BuildExpectedTracks();

            var mockedSpotifyApi = new Mock<ISpotifyApi>();
            mockedSpotifyApi.Setup(x => x.GetSpotifyType<Tracks>(It.IsAny<string>())).Returns(tracksToReturn);

            var sut = new SpotifyService(mockedSpotifyApi.Object);

            List<string> tracks = sut.GetTracksAndArtistsFromPlaylists(playlists);

            Assert.Equal(expectedTracks, tracks);
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

            var mockedSpotifyApi = new Mock<ISpotifyApi>();

            var sut = new SpotifyService(mockedSpotifyApi.Object);

            List<string> resultTracks = sut.GenerateNewPlaylist(userDisplayName, tracks);

            Assert.Equal(expectedTracks, resultTracks);
        }
    }
}
