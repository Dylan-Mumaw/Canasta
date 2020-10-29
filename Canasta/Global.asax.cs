using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Canasta
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Application["userCount"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["userCount"] = (int)Application["userCount"] + 1;
            Session["playerNumber"] = Application["userCount"];
            List<bool> isTurn = new List<bool>();
            if (Application["isTurn"] != null)
            {
                isTurn = (List<bool>)Application["isTurn"];
                isTurn.Add(false);
            }
            else
            {
                isTurn.Add(true);
            }

            Application["isTurn"] = isTurn;

            Application.UnLock();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["userCount"] = (int)Application["userCount"] - 1;
            List<bool> isTurn = new List<bool>();
            isTurn = (List<bool>)Application["isTurn"];
            isTurn.RemoveAt((int)Session["playerNumber"] - 1);
            Application["isTurn"] = isTurn;
            Application.UnLock();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}