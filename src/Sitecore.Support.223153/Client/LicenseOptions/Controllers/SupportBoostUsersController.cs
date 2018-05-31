namespace Sitecore.Support.Client.LicenseOptions.Controllers
{
  using Sitecore.Security.Accounts;
  using Sitecore.SecurityModel.License;
  using Sitecore.Text;
  using Sitecore.Web;
  using System.Net;
  using System.Web;
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
      HttpContextWrapper httpContext = System.Web.HttpContext.Current == null ? null : new HttpContextWrapper(System.Web.HttpContext.Current);

      string redirectUrl = (httpContext == null || httpContext.Request == null || httpContext.Request.UrlReferrer == null || string.IsNullOrEmpty(httpContext.Request.UrlReferrer.Scheme) || string.IsNullOrEmpty(httpContext.Request.UrlReferrer.Host)) ?
          WebUtil.GetServerUrl() :
          string.Format("{0}://{1}:{2}", HttpContext.Request.UrlReferrer.Scheme, HttpContext.Request.UrlReferrer.Host, HttpContext.Request.UrlReferrer.Port);

      var url = new UrlString(WebUtil.GetFullUrl(new Sitecore.Security.Accounts.StartUrlManager().GetStartUrl(Context.User), redirectUrl));
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