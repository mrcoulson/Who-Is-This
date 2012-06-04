using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.DirectoryServices;

namespace WhoIsThis
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFind_Click(object sender, EventArgs e)
        {
            string strUser = Server.HtmlEncode(txtUser.Text.Trim());
            if (strUser.Length > 0)
            {
                DirectorySearcher search = new DirectorySearcher(new DirectoryEntry("LDAP://url.for.your.organization:port/DC=whatever", "user", "pass"));
                search.SearchScope = SearchScope.Subtree;
                search.Filter = "(SAMAccountName=" + strUser + ")";
                SearchResultCollection result = search.FindAll();
                int count = result.Count;

                if (count == 1)
                {
                    foreach (SearchResult res in result)
                    {
                        string strRetrievedFullName = res.Properties["displayname"][0].ToString();
                        string strNameLength = strRetrievedFullName.Length.ToString();
                        litAnswer.Text = "The user's name is " + strRetrievedFullName + ".";
                    }
                }
                else if (count > 1)
                {
                    litAnswer.Text = "Something strange happened.  I got more than one result.";
                }
                else
                {
                    litAnswer.Text = "The search for \"" + strUser + "\" returned no results.  Sorry.";
                }
            }
            else
            {
                litAnswer.Text = "Enter a name.";
            }
        }
    }
}
