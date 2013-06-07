using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// 
/// </summary>
public class CheckOnlineStatusTimer
{
    private static CheckOnlineStatusTimer Timer;
    public static CheckOnlineStatusTimer GetInstance()
    {
        if (Timer == null)
        {
            Timer = new CheckOnlineStatusTimer();
        }
        return Timer;
    }
    private HttpApplication _Context;

    private System.Timers.Timer CheckOnlineTimer;

    public void Ready(HttpApplication Context)
    {
        _Context = Context;
        int CheckTime = 5;
        //int CheckTime = Configure.OnlineCheckTime;

        CheckOnlineTimer = new System.Timers.Timer();
        CheckOnlineTimer.Interval = CheckTime *60 * 1000;
        CheckOnlineTimer.Elapsed += new System.Timers.ElapsedEventHandler(CheckOnlineTimer_Elapsed);
    }

    public void Start()
    {
        if (CheckOnlineTimer!=null)
            CheckOnlineTimer.Enabled= true;
    }

    public void Stop()
    {
        if (CheckOnlineTimer != null)
            CheckOnlineTimer.Enabled = false;
    }

    void CheckOnlineTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        //lock (UserOnlineInfo.GetInstance())
        //{
        //    UserOnlineInfo.GetInstance().OffLine(_Context);
        //}
    }
}
