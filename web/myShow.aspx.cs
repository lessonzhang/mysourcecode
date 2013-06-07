using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Entities.Users;
using Entities.Knowledgemap;

public partial class myShow : BasePage
{
    private string _URL;
    public string URL
    {
        get
        {
            return this._URL;
        }
        set
        {
            this._URL = value;
        }
    }

    private string _URLID;
    public string URLID
    {
        get
        {
            return this._URLID;
        }
        set
        {
            this._URLID = value;
        }
    }

    private string _UserID;
    public string UserID
    {
        get
        {
            return this._UserID;
        }
        set
        {
            this._UserID = value;
        }
    }

    private string _KnowledgeID;
    public string KnowledgeID
    {
        get
        {
            return this._KnowledgeID;
        }
        set
        {
            this._KnowledgeID = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["url"] != null)
            {
                this.URL = Request.QueryString["url"];
            }
            else
            {
                this.URL = "#";
            }
            ifm.Src = this.URL;

            if (Request.QueryString["urlid"] != null)
            {
                this.URLID = Request.QueryString["urlid"];
            }
            else
            {
                this.URLID = "0";
            }

            if (Request.QueryString["knowledgeid"] != null)
            {
                this.KnowledgeID = Request.QueryString["knowledgeid"];
            }
            else
            {
                this.KnowledgeID = "0";
            }
            if(this.CurrentUser.UserID>0)
            {
                this.UserID = this.CurrentUser.UserID.ToString();
                MF_Action action = new MF_Action();
                action.UserID = this.CurrentUser.UserID;
                action.UserName = this.CurrentUser.Username;
                action.KnowledgeID = int.Parse(this.KnowledgeID);
                action.ActionType = 3;
                action.ActionDate = DateTime.Now;
                action.ActionTime = DateTime.Now;
                action.ActionContent = this.URL;
                action.ActionContentID = this.URLID;
                action.OPTag = MyFramework.EDBOperationTag.AddNew;
                action.DB_InsertEntity();
            }
        }
    }

    [WebMethod]
    public static bool SaveFavorite(string URLID, string KnowledgeID, string UserID)
    {
        MF_URL URL = new MF_URL();
        URL.FillSelf(MF_URL.M_URLID + "=" + URLID );
        if (URL.URLID > 0)
        {
            MF_Favorite favorite = new MF_Favorite();
            favorite.URL = URL.URL;
            favorite.UserID = int.Parse(UserID);
            favorite.Title = URL.Title;
            favorite.KnowledgeID = int.Parse(KnowledgeID);
            favorite.Description = URL.Description;
            favorite.OPTag = MyFramework.EDBOperationTag.AddNew;
            return favorite.DB_InsertEntity();
        }
        else
        {
            return false;
        }
    }
}