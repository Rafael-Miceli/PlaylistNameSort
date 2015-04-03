using PlaylistNameSort.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlaylistNameSort.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly SpotifyAuthViewModel _spotifyAuthViewModel;

        public HomeController(SpotifyAuthViewModel spotifyAuthViewModel)
        {            
            _spotifyAuthViewModel = spotifyAuthViewModel;
        }

        public ActionResult Index()
        {
            ViewBag.AuthUri = _spotifyAuthViewModel.GetAuthUri();

            return View();
        }

        public ActionResult GenerateNameSortList(string access_token, string token_type, string expires_in, string state)
        {
            if (string.IsNullOrEmpty(access_token))
                return View();

            var spotifyService = new SpotifyService(access_token);
            //Get user_id and user displayName
            SpotifyUser spotifyUser = spotifyService.GetUserProfile();

            //Get user playlists ids
            List<string> playlistsIds = spotifyService.GetPlaylistsIds(spotifyUser.UserId);

            //Get all tracks from user
            List<string> tracks = spotifyService.GetTracksAndArtistsFromPlaylists(spotifyUser.UserId, playlistsIds);

            //Generate the new playlist 
            List<string> newPlayList = spotifyService.GenerateNewPlaylist(spotifyUser.DisplayName, tracks);


            return View(newPlayList);
        }
    }
}