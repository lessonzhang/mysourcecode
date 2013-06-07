using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Entities.Users;
using Entities.Knowledgemap;
using System.Text;

public partial class Note_AddNote : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    public static bool SaveNote(string Note)
    {
        MF_Note newnote = new MF_Note();

        newnote.UserID = 1;
        newnote.KnowledgeID = 21;
        newnote.NoteDate = DateTime.Now;
        newnote.NoteTime = DateTime.Now;
        newnote.Title = "mytest";
        newnote.Note = Encoding.Default.GetBytes(Note);
        newnote.OPTag = MyFramework.EDBOperationTag.AddNew;
        newnote.DB_InsertEntity();

        return true;
    }

    [WebMethod]
    public static string ReadNote()
    {
        MF_Note newnote = new MF_Note();
        newnote.FillSelf(MF_Note.M_NoteID + "=19");
        return Encoding.Default.GetString(newnote.Note);
    }
}