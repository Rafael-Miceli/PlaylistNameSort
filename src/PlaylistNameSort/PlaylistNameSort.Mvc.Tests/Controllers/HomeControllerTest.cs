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
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_Index_Model_Return_Correct_Values()
        {
            string expectedClientId = "c2b415ceb2694cb29b34088a69816aea";
            string expectedRedirectUri = "http://localhost";
            string expectedState = "";
            Scope expectedScope = Scope.PLAYLIST_MODIFY_PRIVATE;

            var controller = new HomeController();
                        
            var result = (controller.Index() as ViewResult).Model;           

            Assert.Equal(expectedClientId, (result as SpotifyAuthViewModel).ClientId);
            Assert.Equal(expectedRedirectUri, (result as SpotifyAuthViewModel).RedirectUri);
            Assert.Equal(expectedState, (result as SpotifyAuthViewModel).State);
            Assert.Equal(expectedScope, (result as SpotifyAuthViewModel).Scope);
        }        
    }
}
