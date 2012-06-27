using System;
using System.DirectoryServices;
using System.Configuration;

namespace WhoIsThis
{
    public partial class Default : System.Web.UI.Page
    {
        protected void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                findUser(Server.HtmlEncode(txtUser.Text.Trim()));
            }
            catch (DirectoryServicesCOMException exLogon)
            {
                litAnswer.Text = "<p class=\"error\">Error.  Your AD credentials may be incorrect.</p>";
            }
            catch (Exception ex)
            {
                litAnswer.Text = "<p class=\"error\">Error.  Check your server's event viewer.</p>";
            }
        }

        void findUser(string strUser)
        {
            if (strUser.Length > 0)
            {
                string strAdUrl = ConfigurationManager.AppSettings["adUrl"];
                string strAdUser = ConfigurationManager.AppSettings["adUser"];
                string strAdPass = ConfigurationManager.AppSettings["adPass"];
                string strIntranet = ConfigurationManager.AppSettings["intranet"];

                DirectorySearcher search = new DirectorySearcher(new DirectoryEntry(strAdUrl, strAdUser, strAdPass));
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
                            litAnswer.Text = "<p>The user's name is " + strRetrievedFullName + ".";
                        }
                        catch (Exception exName)
                        {
                            litAnswer.Text = "<p>The user's full name was not found.";
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
                            litAnswer.Text += "<br />The user's password was last changed on " + strPwdSet + ".</p>";
                        }
                        catch (Exception exPwdSet)
                        {
                            litAnswer.Text += "<br />The date of the user's last password change was not found.</p>";
                        }
                        try
                        {
                            litAnswer.Text += "<p>Group membership:</p><ul>";
                            if (res.Properties["memberOf"].Count > 0)
                            {
                                foreach (string strProperty in res.Properties["memberOf"])
                                {
                                    string strMemberOf = strProperty;
                                    int intCommaIndex = strMemberOf.IndexOf(",");
                                    if (intCommaIndex > 0)
                                    {
                                        strMemberOf = strMemberOf.Substring(0, intCommaIndex);
                                    }
                                    int intEqualIndex = strMemberOf.IndexOf("=") + 1;
                                    if (intEqualIndex > 0)
                                    {
                                        strMemberOf = strMemberOf.Substring(intEqualIndex);
                                    }
                                    litAnswer.Text += "<li>" + strMemberOf + "</li>";
                                }
                            }
                            else
                            {
                                litAnswer.Text += "<li>The user does not seem to be a member of any groups.</li>";
                            }
                            litAnswer.Text += "</ul>";
                        }
                        catch (Exception exMemberOf)
                        {
                            litAnswer.Text += "<p>The user's group membership was not found.</p>";
                        }
                    }
                    litAnswer.Text += "<p>For the user's contact information, visit the <a href=\"" + strIntranet + "\">intranet directory</a>.</p>";
                }
                else if (count > 1)
                {
                    litAnswer.Text = "<p>Something strange happened.  I got more than one result.</p>";
                }
                else
                {
                    litAnswer.Text = "<p>The search for \"" + strUser + "\" returned no results.  Check the spelling and try again.</p>";
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