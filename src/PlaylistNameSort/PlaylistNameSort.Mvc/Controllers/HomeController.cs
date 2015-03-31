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
    }
}