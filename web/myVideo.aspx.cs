using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Users;
using Entities.Knowledgemap;

public partial class myVideo : BasePage
{
    private string _myPath;
    public string myPath
    {
        get
        {
            return this._myPath;
        }
        set
        {
            this._myPath = value;
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
    private string _VideoID;
    public string VideoID
    {
        get
        {
            return this._VideoID;
        }
        set
        {
            this._VideoID = value;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["url"] != null)
            {
                this.myPath = Request.QueryString["url"];
            }
            else
            {
                this.myPath = "#";
            }

            if (Request.QueryString["videoid"] != null)
            {
                this.VideoID = Request.QueryString["videoid"];
            }
            else
            {
                this.VideoID = "0";
            }
            if (Request.QueryString["knowledgeid"] != null)
            {
                this.KnowledgeID = Request.QueryString["knowledgeid"];
            }
            else
            {
                this.KnowledgeID = "0";
            }
            if (this.CurrentUser.UserID > 0)
            {
                this.UserID = this.CurrentUser.UserID.ToString();
                MF_Action action = new MF_Action();
                action.UserID = this.CurrentUser.UserID;
                action.UserName = this.CurrentUser.Username;
                action.KnowledgeID = int.Parse(this.KnowledgeID);
                action.ActionType = 1;
                action.ActionDate = DateTime.Now;
                action.ActionTime = DateTime.Now;
                action.ActionContent = this.myPath;
                action.ActionContentID = this.VideoID;
                action.OPTag = MyFramework.EDBOperationTag.AddNew;
                action.DB_InsertEntity();
            }
        }
    }
}