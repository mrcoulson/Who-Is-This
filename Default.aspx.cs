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
                DirectorySearcher search = new DirectorySearcher(new DirectoryEntry("LDAP://your.url:port/DC=your,DC=url", "user", "pass"));
                search.SearchScope = SearchScope.Subtree;
                search.Filter = "(SAMAccountName=" + strUser + ")";
                SearchResultCollection result = search.FindAll();
                int count = result.Count;

                if (count == 1)
                {
                    foreach (SearchResult res in result)
                    {
                        try
                        {
                            string strRetrievedFullName = res.Properties["displayname"][0].ToString();
                            string strNameLength = strRetrievedFullName.Length.ToString();
                            litAnswer.Text = "The user's name is " + strRetrievedFullName + ".";
                        }
                        catch
                        {
                            litAnswer.Text = "The user's full name was not found.";
                        }
                        try
                        {
                            string strDept = res.Properties["physicalDeliveryOfficeName"][0].ToString();
                            litAnswer.Text += "<br />The user's department is " + strDept + ".";
                        }
                        catch (Exception ex)
                        {
                            litAnswer.Text += "<br />The user's department was not found.";
                        }
                    }
                }
                else if (count > 1)
                {
                    litAnswer.Text = "Something strange happened.  I got more than one result.";
                }
                else
                {
                    litAnswer.Text = "The search for \"" + strUser + "\" returned no results.  Check the spelling and try again.";
                }
            }
            else
            {
                litAnswer.Text = "Enter a name.";
            }
        }
    }
}