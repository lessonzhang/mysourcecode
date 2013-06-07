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

public partial class Account_Manage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            if (this.CurrentUser.UserID > 0)
            {
                this.lblUserName.Text = this.CurrentUser.Username;
                this.txtEmail.Text = this.CurrentUser.Email;
                this.txtName.Text = this.CurrentUser.Name;
                this.ddlGrade.SelectedValue = this.CurrentUser.Grade.ToString();
                this.txtNote.Text = this.CurrentUser.Note;
            }
            else
            {
                this.Page.Response.Redirect("../Account/login.aspx");
            }
        }
    }

    protected void SubmitBtn_Click(object sender, EventArgs e)
    {
        MF_User register = new MF_User();
        register.UserID = this.CurrentUser.UserID;
        register.Username = this.CurrentUser.Username;
        register.Password = this.CurrentUser.Password;
        register.Email = this.txtEmail.Text.ToUpper();
        register.Note = this.txtNote.Text;
        register.Name = this.txtName.Text;
        register.Grade = int.Parse(this.ddlGrade.SelectedValue);
        register.OPTag = MyFramework.EDBOperationTag.Update;
        if (register.DB_SaveEntity())
        {
            register.FillSelf(MF_User.M_Username + "='" + register.Username + "'");
            this.CurrentUser = register;
            errorMessage.InnerText = "修改成功。";
        }
        else
        {
            errorMessage.InnerText = "抱歉，未知错误，请稍后再试试。";
        }
    }

    [WebMethod]
    public static bool EmailisExist(string email, string userid)
    {
        MF_User register = new MF_User();
        register.UserID = -1;
        register.FillSelf(MF_User.M_Email + "='" + email.ToUpper() + "'");
        if ((register.UserID > 0) && (register.UserID.ToString() != userid))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}