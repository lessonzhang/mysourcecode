using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Users;
using Entities.Knowledgemap;
using MyFramework.Data.Query;
using MyFramework.Utility;
using System.Web.Services;
using MyFramework.Components;
using MyFramework.Utility.Serialization;
using MyFramework;
using System.Net;
using System.Data.Services.Client;
using System.IO;

public partial class _Default : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (this.CurrentUser != null)
            {
                userid = this.CurrentUser.UserID.ToString();
            }
        }
    }
    public static string userid = "";

    [WebMethod]
    public static string getFileName(string course, string sizex, string sizey)
    {
        KnowledgeMap.getFileName(userid, course, sizex, sizey);
        return userid + course;
    }

    [WebMethod]
    public static List<mySearch.SearchResult> GetSearchResult(string keyword, string knowledgeid, string course)
    {
        return mySearch.GetSearchResult(keyword, knowledgeid, course);
    }

    [WebMethod]
    public static List<MF_Video> GetVideo(string knowledgeid)
    {
        List<MF_Video> vs = new List<MF_Video>();
        ORMQuery<MF_Video> Query = new ORMQuery<MF_Video>();
        vs = Query.Query(MF_Video.M_KnowledgeID + " = ('" + knowledgeid + "')").ToList();

        return vs;
    }

    [WebMethod]
    public static int SaveClickURL(string URL,string Title,string Description,string KnowledgeID)
    {
        MF_URL clickURL = new MF_URL();
        clickURL.URLID = -1;
        clickURL.FillSelf(MF_URL.M_URL+ "='" + URL + "'");
        if (clickURL.URLID > 0)
        {
            clickURL.Count = clickURL.Count + 1;
            clickURL.OPTag = EDBOperationTag.Update;
            clickURL.DB_SaveEntity();
        }
        else
        {
            clickURL.URL = URL;
            clickURL.Title = Title;
            clickURL.Description = Description;
            clickURL.KnowledgeID = int.Parse(KnowledgeID);
            clickURL.Count = 1;
            clickURL.TotalViewTime = 0;
            clickURL.OPTag = EDBOperationTag.AddNew;
            clickURL.DB_InsertEntity();
            clickURL.FillSelf(MF_URL.M_URL + "='" + URL + "'");
        }
        return clickURL.URLID;
    }

    [WebMethod]
    public static void SaveClickVideo(string VideoID)
    {
        MF_Video clickVideo = new MF_Video();
        clickVideo.VideoID = -1;
        clickVideo.FillSelf(MF_Video.M_VideoID + "='" + VideoID + "'");
        if (clickVideo.VideoID > 0)
        {
            clickVideo.Count = clickVideo.Count + 1;
            clickVideo.OPTag = EDBOperationTag.Update;
            clickVideo.DB_SaveEntity();
        }
    }
}