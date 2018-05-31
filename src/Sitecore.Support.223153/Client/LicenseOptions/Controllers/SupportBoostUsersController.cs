namespace Sitecore.Support.Client.LicenseOptions.Controllers
{
  using Sitecore.Security.Accounts;
  using Sitecore.SecurityModel.License;
  using Sitecore.Text;
  using Sitecore.Web;
  using System.Net;
  using System.Web.Mvc;
  public class SupportBoostUsersController : Controller
  {
    #region Public methods

    [HttpGet]
    public void RedirectToBoost()
    {
      if (!Context.User.IsAuthenticated)
      {
        this.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return;
      }
      this.Response.Redirect(this.GetBoostUrl(), true);
    }

    #endregion

    #region Protected methods

    protected string GetBoostUrl()
    {
      var url = new UrlString(WebUtil.GetFullUrl(new StartUrlManager().GetStartUrl(Context.User)));
      url.Add("inv", "1");
      var boostUrl = new UrlString("http://www.sitecore.net/boost");
      boostUrl.Add("url", url.ToString());
      boostUrl.Add("lid", StringUtil.GetString(License.LicenseID));
      boostUrl.Add("host", StringUtil.GetString(WebUtil.GetHostName()));
      boostUrl.Add("ip", StringUtil.GetString(WebUtil.GetHostIPAddress()));
      boostUrl.Add("licensee", StringUtil.GetString(License.Licensee));
      boostUrl.Add("iisname", StringUtil.GetString(WebUtil.GetIISName()));

      return boostUrl.ToString();
    }

    #endregion
  }
}