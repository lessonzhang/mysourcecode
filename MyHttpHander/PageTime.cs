using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Entities.Users;
using Entities.Knowledgemap;

namespace MyHttpHander
{
    public class PageTime : System.Web.IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public PageTime()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            HttpRequest Request = context.Request;
            context.Response.Clear(); 
            string url = string.Empty;
            if (Request.QueryString["url"] != null)
            {
                url = Request.QueryString["url"];
            }
            else
            {
                url = "#";
            }
            int knowledgeid = 0;
            if (Request.QueryString["KnowledgeID"] != null)
            {
                knowledgeid = int.Parse(Request.QueryString["KnowledgeID"]);
            }
            int URLID = 0;
            if (Request.QueryString["URLID"] != null)
            {
                URLID = int.Parse(Request.QueryString["URLID"]);
            }
            int UserID = 0;
            if (Request.QueryString["UserID"] != null)
            {
                UserID = int.Parse(Request.QueryString["UserID"]);
            }
            switch (Request.QueryString["action"])
            {
                case "Enter":
                    MF_ViewLog VLEnter = new MF_ViewLog();
                    VLEnter.ViewLogID = -1;
                    VLEnter.FillSelf(MF_ViewLog.M_SessionID + "='" + context.Session.SessionID + "' and " + MF_ViewLog.M_URL + "='" + url + "'");
                    if (VLEnter.ViewLogID > 0)
                    {
                        VLEnter.ViewTime = DateTime.Now;
                        VLEnter.OPTag = MyFramework.EDBOperationTag.Update;
                        VLEnter.DB_SaveEntity();
                    }
                    else
                    {
                        VLEnter.SessionID = context.Session.SessionID;
                        VLEnter.KnowledgeID = knowledgeid;
                        VLEnter.URL = url;
                        VLEnter.URLID = URLID;
                        VLEnter.UserID = UserID;
                        VLEnter.ViewDate = DateTime.Now;
                        VLEnter.ViewTime = DateTime.Now;
                        VLEnter.TotalTime = 0;
                        VLEnter.OPTag = MyFramework.EDBOperationTag.AddNew;
                        VLEnter.DB_InsertEntity();
                    }
                    break;
                case "Exit":
                    MF_ViewLog VLExit = new MF_ViewLog();
                    VLExit.FillSelf(MF_ViewLog.M_SessionID + "='" + context.Session.SessionID + "' and " + MF_ViewLog.M_URL + "='" + url + "'");
                    if (VLExit.ViewLogID > 0)
                    {
                        TimeSpan ts = DateTime.Now - VLExit.ViewTime;
                        VLExit.TotalTime = VLExit.TotalTime + ts.Hours * 60 * 60 + ts.Minutes * 60 + ts.Seconds;

                        if (VLExit.TotalTime > 60)
                        {
                            MF_URL usefullurl = new MF_URL();
                            usefullurl.URLID = -1;
                            usefullurl.FillSelf(MF_URL.M_URLID + "='" + URLID + "'");
                            if (usefullurl.URLID > 0)
                            {
                                usefullurl.TotalViewTime = usefullurl.TotalViewTime + VLExit.TotalTime;
                                usefullurl.OPTag = MyFramework.EDBOperationTag.Update;
                                usefullurl.DB_SaveEntity();
                            }
                        }

                        VLExit.OPTag = MyFramework.EDBOperationTag.Update;
                        VLExit.DB_SaveEntity();
                    }
                    break;

            }
            context.Response.End();
        }
    }
}
