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
using MyFramework.Data.Query;

public partial class Account_MyFriend:BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!this.IsPostBack)
        {
            if (this.CurrentUser.UserID < 0)
            {
                this.Page.Response.Redirect("../Account/login.aspx");
            }
            else
            {
            }
        }
    }

    [WebMethod]
    public static List<MF_Group> GetGroups(string userid)
    {
        List<MF_Group> Groups = new List<MF_Group>();
        ORMQuery<MF_Group> Query = new ORMQuery<MF_Group>();
        Groups = Query.Query(MF_Group.M_UserID + " = ('"+userid+"')").ToList();
        return Groups;
    }

    [WebMethod]
    public static List<MF_GroupMember> GetGroupMembers(string GroupID)
    {
        List<MF_GroupMember> GroupMembers = new List<MF_GroupMember>();
        ORMQuery<MF_GroupMember> Query = new ORMQuery<MF_GroupMember>();
        GroupMembers = Query.Query(MF_GroupMember.M_GroupID + " = ('"+GroupID+"') AND " + MF_GroupMember.M_Status + " = (1)").ToList();
        return GroupMembers;
    }

    [WebMethod]
    public static void DoInvite(string GroupMemberID, string type)
    {
        MF_GroupMember GroupMember = new MF_GroupMember();
        GroupMember.GroupMemberID = -1;
        GroupMember.FillSelf(MF_GroupMember.M_GroupMemberID + "=(" + GroupMemberID + ")");
        if (GroupMember.GroupMemberID > 0)
        {
            if (type == "ok")
            {
                GroupMember.Status = 1;
                GroupMember.OPTag = MyFramework.EDBOperationTag.Update;
                GroupMember.DB_UpdateEntity();

                MF_Group myGroup = new MF_Group();
                myGroup.FillSelf(MF_Group.M_UserID + "=(" + GroupMember.UserID + ")");
                MF_GroupMember myGroupmember = new MF_GroupMember();
                myGroupmember.UserID = GroupMember.MyID;
                myGroupmember.UserName = GroupMember.MyName;
                myGroupmember.MyID = GroupMember.UserID;
                myGroupmember.MyName = GroupMember.UserName;
                myGroupmember.Status = 1;
                myGroupmember.GroupID = myGroup.GroupID;
                myGroupmember.OPTag = MyFramework.EDBOperationTag.AddNew;
                myGroupmember.DB_InsertEntity();
            }
            else
            {
                GroupMember.OPTag = MyFramework.EDBOperationTag.Delete;
                GroupMember.DB_DeleteEntity();
            }
        }
    }

    [WebMethod]
    public static List<MF_GroupMember> GetInvite(string userid)
    {
        List<MF_GroupMember> GroupMembers = new List<MF_GroupMember>();
        ORMQuery<MF_GroupMember> Query = new ORMQuery<MF_GroupMember>();
        GroupMembers = Query.Query(MF_GroupMember.M_UserID + " = ('" + userid + "') AND " + MF_GroupMember.M_Status + " = (2)").ToList();
        return GroupMembers;
    }

    protected void addBtn_Click(object sender, EventArgs e)
    {
        MF_User friend = new MF_User();
        friend.UserID = -1;
        friend.FillSelf(MF_User.M_Username + "=(" +this.txtUserName.Text + ")");
        if (friend.UserID > 0)
        {
             MF_GroupMember mgm = new MF_GroupMember();
            mgm.GroupMemberID = -1;
             mgm.FillSelf(MF_GroupMember.M_UserID + "=(" + friend.UserID.ToString() + ")");
             if (mgm.GroupMemberID > 0)
             {
                 if (mgm.Status == 1)
                 {
                     Response.Write("<script language=javascript>alert('" + this.txtUserName.Text + "已经是您的好友！');</script>");
                 }
                 else
                 {
                     Response.Write("<script language=javascript>alert('" + this.txtUserName.Text + "已经被您的好友，请等待好友反馈！');</script>");
                 }
             }
             else
             {
                 MF_Group Group = new MF_Group();
                 Group.GroupID = -1;
                 Group.FillSelf(MF_Group.M_UserID + "=(" + this.CurrentUser.UserID.ToString() + ")");

                 MF_GroupMember gm = new MF_GroupMember();
                 gm.Status = 2;
                 gm.UserID = friend.UserID;
                 gm.UserName = friend.Username;
                 gm.GroupID = Group.GroupID;
                 gm.MyID = this.CurrentUser.UserID;
                 gm.MyName = this.CurrentUser.Username;
                 gm.OPTag = MyFramework.EDBOperationTag.AddNew;
                 gm.DB_InsertEntity();
                 Response.Write("<script language=javascript>alert('添加成功，等待好友反馈！');</script>");
             }
        }
        else
        { 
            Response.Write("<script language=javascript>alert('请输入正确的用户名');</script>");
        }
    }
}