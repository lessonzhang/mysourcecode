using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFramework.Components;
using System.Net;
using Entities.Knowledgemap;
using MyFramework.Data.Query;


/// <summary>
/// Summary description for mySearch
/// </summary>
public class mySearch
{

    private const string AccountKey = "GdmA+fXz+S/GAxuSziw3jE36FLIugcST08bcg+usbww=";

    public class SearchResult
    {
        public string ID { get; set; }
        public string URL { get; set; }
        public string URLID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isRemove { get; set; }
    }

    public class SearchVideo
    {
        public string ID { get; set; }
        public string URL { get; set; }
        public string URLID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

	public mySearch()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static List<mySearch.SearchResult> GetSearchResult(string keyword,string knowledgeid,string course)
    {
        string rootUrl = "https://api.datamarket.azure.com/Bing/Search";
        BingSearchContainer bingContainer = new BingSearchContainer(new Uri(rootUrl));
        string market = "zh-cn";
        bingContainer.Credentials = new NetworkCredential(AccountKey, AccountKey);
        if (course == "math1") { keyword = keyword + " 小学"; }
        if (course == "math2") { keyword = keyword + " 初中"; }
        var webQuery =  bingContainer.Web(keyword, null, null, market, "Strict", null, null, null);

        webQuery = webQuery.AddQueryOption("$top", 50);
        var webResults = webQuery.Execute();

        List<SearchResult> myresults = new List<SearchResult>();
        foreach (var result in webResults)
        {
            SearchResult sr = new SearchResult();
            sr.Title = result.Title;
            sr.URL = result.Url;
            sr.URLID = "0";
            sr.Description = result.Description;
            sr.isRemove = false;
            myresults.Add(sr);
        }
        myresults = FillterSearchResult(myresults);

        List<MF_URL> URLs = new List<MF_URL>();
        ORMQuery<MF_URL> Query = new ORMQuery<MF_URL>();
        URLs = Query.Query(MF_URL.M_KnowledgeID + " = ('" + knowledgeid + "')").ToList();
        URLs = URLs.OrderByDescending(p => p.Count).ToList();

        List<SearchResult> results = new List<SearchResult>();
        foreach (MF_URL item in URLs)
        {
            if (results.Count < 5)
           {
               SearchResult sr = new SearchResult();
               sr.URLID = item.URLID.ToString();
               sr.URL = item.URL;
               sr.Title = item.Title;
               sr.Description = item.Description;
               results.Add(sr);
           }
        }
        foreach (SearchResult item in myresults)
        {
            if (results.Count == 10)
            {
                break;
            }
            if (results.Where(p => p.URL == item.URL).ToList().Count == 0)
            {
                results.Add(item);
            }
        }
        return results;
    }

    private static List<mySearch.SearchResult> FillterSearchResult(List<mySearch.SearchResult> SR)
    {
        List<MF_BlackList> bl = new List<MF_BlackList>();
        ORMQuery<MF_BlackList> Query = new ORMQuery<MF_BlackList>();
        bl = Query.Query("1=1").ToList();
        foreach (mySearch.SearchResult item in SR)
        {
            foreach (MF_BlackList i in bl)
            {
                if (item.URL.IndexOf(i.URL)>=0)
                {
                    item.isRemove = true;
                    break;
                }
            }
        }
        SR = SR.Where(p => p.isRemove == false).ToList();
        return SR;
    }
}