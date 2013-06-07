using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Entities.Users;

/// <summary>
/// BaseMasterPage 的摘要说明
/// </summary>
public class BaseMasterPage : System.Web.UI.MasterPage
{
    public BaseMasterPage()
    {
        this.Context.Session["SessionId"] = this.Context.Session.SessionID;
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    #region Extend Property
   
    /// <summary>
    /// page current relative path. 
    /// </summary>
    public string Path
    {
        get
        {
            int i = 1;

            string url1 = this.Page.TemplateSourceDirectory;
            if (url1.EndsWith("/"))
                url1 = url1.Substring(0, url1.Length - 1);

            string url2 = this.Page.AppRelativeTemplateSourceDirectory.Remove(0, 1);
            if (url2.EndsWith("/"))
                url2 = url2.Substring(0, url2.Length - 1);

            if (url1 == url2)
                i = 1;
            else if (url1.EndsWith(url2))
                i = 2;

            string[] temp = url1.Split('/');
            string str = "../";
            string str2 = "";

            for (; i < temp.Length; i++)
            {
                str2 += str;
            }
            return str2;
        }

    }

    private string _BasePath = "http://localhost:12649/mufeng/";
    public string BasePath
    {
        get
        {
            return this._BasePath;
        }
        set
        {
            this._BasePath = value;
        }
    }

    /// <summary>
    /// System Current Sign in User or Register User;
    /// </summary>
    protected MF_User CurrentUser
    {
        set
        {
            this.Session["C_CURRENTUSER"] = value;
        }
        get
        {
            MF_User _User = null;

            if (this.Session["C_CURRENTUSER"] != null)
            {
                _User = (MF_User)this.Session["C_CURRENTUSER"];
            }
            else
            {
                _User = new MF_User();
                _User.UserID = -1;
            }
            return _User;
        }
    }
    #endregion

    #region Extend Method
    
    /// <summary>
    /// Remove User from session
    /// </summary>
    protected void RemoveUser()
    {
        this.Session.Remove("C_CURRENTUSER");
    }

    /// <summary>
    /// Check Session whether timeout.
    /// </summary>
    /// <returns>if return true then not timenot,else timeout.</returns>
    protected bool SessionStatusVerify()
    {
        bool Result = true;
        if (this.CurrentUser.UserID < 0) Result = false;
        return Result;
    }
    
    #endregion
}
