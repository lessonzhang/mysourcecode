using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFramework.Components;
using System.IO;
using Entities.Knowledgemap;
using MyFramework.Utility;
using MyFramework.Data.Query;
using MyFramework.Utility.Serialization;
using System.Text;

/// <summary>
/// Summary description for KnowlegeMap
/// </summary>
public class KnowledgeMap
{
	public KnowledgeMap()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public class Knowledge
    {
        public string ID { get; set; }
        public string PID { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
    }

    public class KnowledgeRef
    {
        public string ID { get; set; }
        public string PID { get; set; }
        public string Title { get; set; }
    }

    public static string getFileName(string userid, string course, string sizex, string sizey)
    {
        if (userid == "")
        {
            DirectoryInfo dir = new DirectoryInfo("~/TempFile/Graphviz/" + userid + course+".svg");
            if (!dir.Exists)
            {
                GraphvizHelper.GetNodeMap(course, GetKnowledgeRef(course, sizex, sizey));
            }
        }
        else
        {
            GraphvizHelper.GetNodeMap(userid + course, GetKnowledgeRef(course, sizex, sizey));
        }
        return userid + course;
    }

    public static string GetKnowledgeRef(string course, string sizex, string sizey)
    {
        List<MF_Knowledge> ks = new List<MF_Knowledge>();
        ORMQuery<MF_Knowledge> Query = new ORMQuery<MF_Knowledge>();
        if (course == "math1")
        {
            ks = Query.Query(MF_Knowledge.M_Course + " = ('1')").ToList();
        }
        else if (course == "math2")
        {
            ks = Query.Query(MF_Knowledge.M_Course + " = ('2')").ToList();
        }

        List<MF_KnowledgeRef> ksr = new List<MF_KnowledgeRef>();
        ORMQuery<MF_KnowledgeRef> Query2 = new ORMQuery<MF_KnowledgeRef>();
        if (course == "math1")
        {
            ksr = Query2.Query(MF_KnowledgeRef.M_Course + " = ('1')").ToList();
        }
        else if (course == "math2")
        {
            ksr = Query2.Query(MF_KnowledgeRef.M_Course + " = ('2')").ToList();
        }
        List<KnowledgeRef> res = new List<KnowledgeRef>();

        StringBuilder sb = new StringBuilder();
        sb.Append("digraph G {\n");
        //sb.Append("size=\"" + sizex + "," + sizey + "\";\n");
        sb.Append("center=true;\n");
        //sb.Append("ranksep=1;\n");
        //sb.Append("nodesep=0.3;\n");
        sb.Append("rankdir=BT;\n");
        sb.Append("graph [splines=spline,splines=true]\n");
        sb.Append("node [shape=box,style=rounded,color=\"#084081\",fontsize=13];\n");
        sb.Append("edge [shape=vee,color=grey,weight=2];\n");

        string JSONstring = string.Empty;

        foreach (MF_Knowledge item in ks)
        {
            JSONstring += item.KnowledgeID + "[fillcolor=\"#084081\", style=\"rounded,filled\",label = " + item.KnowledgeName + " ];";
        }

        foreach (MF_KnowledgeRef item in ksr)
        {

            JSONstring += item.PreKnowledgeID + "->";
            JSONstring += item.KnowledgeID + ";";
        }
        JSONstring += "}";
        return sb.ToString() + JSONstring;
    }
}