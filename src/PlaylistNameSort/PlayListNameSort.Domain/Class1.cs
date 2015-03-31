using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlayListNameSort.Domain.Util;
using System.Threading.Tasks;

namespace PlayListNameSort.Domain
{
    public class SpotifyAuthentication
    {
        public bool ShowDialog { get; private set; }
        public Scope Scope { get; set; }

        public bool Login(string username, string password)
        {
            //Need to implement login

            return true;
        }


        //private String GetLoginUri()
        //{
        //    StringBuilder builder = new StringBuilder("https://accounts.spotify.com/authorize/?");
        //    builder.Append("client_id=" + ClientId);
        //    builder.Append("&response_type=token");
        //    builder.Append("&redirect_uri=" + RedirectUri);
        //    builder.Append("&state=" + State);
        //    builder.Append("&scope=" + Scope.GetStringAttribute(" "));
        //    builder.Append("&show_dialog=" + ShowDialog.ToString());
        //    return builder.ToString();
        //}

        public void Authorize()
        {
            StringBuilder builder = new StringBuilder("https://accounts.spotify.com/authorize/?");
            builder.Append("client_id=c2b415ceb2694cb29b34088a69816aea");
            builder.Append("&response_type=token");
            builder.Append("&redirect_uri=http://localhost");
            builder.Append("&state=");
            builder.Append("&scope=" + Scope.GetStringAttribute(" "));
            builder.Append("&show_dialog=" + ShowDialog.ToString());
        }
    }

    
    
    public class StringAttribute : Attribute
    {
        public String Text { get; set; }
        public StringAttribute(String text)
        {
            this.Text = text;
        }
    }
}
