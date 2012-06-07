using System;
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
                            litAnswer.Text = "The user's name is " + strRetrievedFullName + ".";
                        }
                        catch (Exception exName)
                        {
                            litAnswer.Text = "The user's full name was not found.";
                        }
                        try
                        {
                            string strDept = res.Properties["physicalDeliveryOfficeName"][0].ToString();
                            litAnswer.Text += "<br />The user's department is " + strDept + ".";
                        }
                        catch (Exception exDept)
                        {
                            litAnswer.Text += "<br />The user's department was not found.";
                        }
                        try
                        {
                            string strTitle = res.Properties["description"][0].ToString();
                            litAnswer.Text += "<br />The user's description is " + strTitle + ".";
                        }
                        catch (Exception exDescription)
                        {
                            litAnswer.Text += "<br />The user's description was not found.";
                        }
                        try
                        {
                            string strUserCreated = DateTime.Parse(res.Properties["whenCreated"][0].ToString()).ToLocalTime().ToString("M/d/yyyy a&#116; h:mm:ss tt");
                            litAnswer.Text += "<br />The user was created on " + strUserCreated + ".";
                        }
                        catch (Exception exCreated)
                        {
                            litAnswer.Text += "<br />The user's creation date was not found.";
                        }
                        try
                        {
                            long lngPwdSet = long.Parse(res.Properties["pwdLastSet"][0].ToString());
                            DateTime dtPwdSet = new DateTime(1601, 01, 01).AddTicks(lngPwdSet).ToLocalTime();
                            string strPwdSet = dtPwdSet.ToString("M/d/yyyy a&#116; h:mm:ss tt");
                            litAnswer.Text += "<br />The user's password was last changed on " + strPwdSet + ".";
                        }
                        catch (Exception exPwdSet)
                        {
                            litAnswer.Text += "<br />The date of the user's last password change was not found.";
                        }
                    }
                    litAnswer.Text += "<br /><br />For the user's contact information, visit the <a href=\"http://link.to.intranet/directory.aspx\">intranet directory</a>.";
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

/* 
Removed default namespaces:
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
*/