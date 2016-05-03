using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

using DRMS.Model;
using DRMS.BLL;


namespace DRMS.MirrorWeb.ajax
{
    /// <summary>
    /// 统计数据库中各类资源数量
    /// </summary>
    public class GetDBResCount : IHttpHandler
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            context.Response.Write(GetData());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected string GetData()
        {
            List<DBCount> mylist = new List<DBCount>();

            for (int i = 1; i < 100; i++)
            {
                DataBaseType mydbtype = (DataBaseType)i;
                switch (mydbtype)
                {
                    case DataBaseType.BOOKTDATA:
                        {
                            Book bookdal = new Book();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");//审核通过
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");//审核未通过
                            dbcountItem.DBtype = i.ToString();
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));//获取枚举描述信息
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.ENTRYDATA:
                        {
                            Terminology bookdal = new Terminology();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount("METATYPE=2");//审核通过
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");//审核未通过
                            dbcountItem.DBtype = i.ToString();
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));//获取枚举描述信息
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.REFERENCEBOOK:
                        {
                            ToolBook bookdal = new ToolBook();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;

                    case DataBaseType.CRITERION:
                        {
                            StdData bookdal = new StdData();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;

                    case DataBaseType.CONFERENCEPAPER:
                        {
                            ConferencePaper bookdal = new ConferencePaper();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;

                    case DataBaseType.JOURNAL:
                        {
                            JournalYear bookdal = new JournalYear();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.JOURNALARTICLE:
                        {
                            JournalArticle bookdal = new JournalArticle();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount("");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.ENGLISHRES:
                        {
                            JournalYear bookdal = new JournalYear("english");
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.ENGLISHARTICLE:
                        {
                            JournalArticle bookdal = new JournalArticle("english");
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount("");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.MAGAZINE:
                        {
                            Magazine bookdal = new Magazine();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.NEWSPAPER:
                        {
                            NewsPaper bookdal = new NewsPaper();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.PICDATA:
                        {
                            Pic bookdal = new Pic();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount("(name=* not name is null)");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.VIDEODATA:
                        {
                            Video bookdal = new Video();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;

                    case DataBaseType.AUDIODATA:
                        {
                            Audio bookdal = new Audio();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.YEARBOOK:
                        {
                            YearBookYear bookdal = new YearBookYear();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.THESIS:
                        {
                            Thesis bookdal = new Thesis();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.CONTRACT:
                        {
                            Contract bookdal = new Contract();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount("");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.AUTHOR:
                        {
                            Author bookdal = new Author();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.ORG:
                        {
                            Org bookdal = new Org();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.ORIGINALDATA:
                        {
                            OriginalData bookdal = new OriginalData();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount(" SYS_FLD_CHECK_STATE=-1");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    case DataBaseType.BOOKCHAPTER:
                        //case DataBaseType.STDDATACHAPTER:
                        {
                            Chapter bookdal = new Chapter();
                            DBCount dbcountItem = new DBCount();
                            dbcountItem.Count = bookdal.GetCount("SYS_FLD_ISPART=0");
                            dbcountItem.CountUndeal = bookdal.GetCount(" SYS_FLD_CHECK_STATE=0");
                            dbcountItem.Description = EnumDescription.GetFieldText(Enum.Parse(typeof(DataBaseType), i.ToString()));
                            dbcountItem.DBtype = i.ToString();
                            mylist.Add(dbcountItem);
                        }
                        break;
                    default:
                        break;
                }
            }

            JavaScriptSerializer json = new JavaScriptSerializer();
            string result = json.Serialize(mylist);
            return result;
        }
        /// <summary>
        /// 返回到前台的类。
        /// </summary>
        [Serializable]
        public class DBCount
        {
            public string DBtype { get; set; }
            public int Count { get; set; }
            public int CountUndeal { get; set; }
            public string Description { get; set; }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}