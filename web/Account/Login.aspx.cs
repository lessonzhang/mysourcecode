using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Users;
using MyFramework.Utility.Cryptography;

public partial class Account_Login : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentUser =  new MF_User();
        this.CurrentUser.UserID = -1;
    }
    protected void Unnamed6_Click(object sender, EventArgs e)
    {
        MF_User loginuser = new MF_User();
        DESEncrypt dese = new DESEncrypt();
        if (loginuser.Login(this.UserName.Text, dese.EncryptString(this.Password.Text)))
        {
            this.CurrentUser = loginuser;
            this.Page.Response.Redirect("../Default.aspx"); 

        }
        else
        {
            this.errorMessage.Visible = true;
        }
    }
}