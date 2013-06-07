using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entities.Users;
using MyFramework.Utility.Cryptography;
using System.Web.Services;

public partial class Account_Register :BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        MF_User register = new MF_User();
        DESEncrypt dese = new DESEncrypt();
        register.Username = this.txtUserName.Text;
        register.Password = dese.EncryptString(this.txtPassword.Text);
        register.Email = this.txtEmail.Text.ToUpper();
        register.Note = this.txtNote.Text;
        register.Name = this.txtName.Text;
        register.Grade = int.Parse(this.ddlGrade.SelectedValue);
        register.OPTag = MyFramework.EDBOperationTag.AddNew;
        if (register.DB_InsertEntity())
        {
            register.FillSelf(MF_User.M_Username + "='" + register.Username + "'");
            this.CurrentUser = register;
            MF_Group group = new MF_Group();
            group.GroupName = "我的好友";
            group.UserID = register.UserID;
            group.OPTag = MyFramework.EDBOperationTag.AddNew;
            group.DB_InsertEntity();

            this.Page.Response.Redirect("../Default.aspx"); 
        }
        else 
        {
            errorMessage.InnerText = "抱歉，未知错误，请稍后再试试。";
        }
    }

    [WebMethod]
    public static bool UserNameisExist(string username)
    {
        MF_User register = new MF_User();
        register.UserID = -1;
        register.FillSelf(MF_User.M_Username + "='" + username + "'");
        if (register.UserID>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    [WebMethod]
    public static bool EmailisExist(string email)
    {
        MF_User register = new MF_User();
        register.UserID = -1;
        register.FillSelf(MF_User.M_Email + "='" + email.ToUpper() + "'");
        if (register.UserID > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}