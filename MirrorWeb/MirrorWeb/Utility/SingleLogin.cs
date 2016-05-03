using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRMS.MirrorWeb
{
    /// <summary>
    /// 限制重复登录   也可限制最大登录数，不一定为1
    /// 
    /// 注意：1、判断不同用户使用了SessionId、所以Session的过期时间必须要大于或者等于Form验证中Cookie的过期时间
    ///       2、修改最大登录数时，需要修改isLogin方法和isReLogin两个方法中的count参数，两个方法中的count必须相等。
    /// </summary>
    public class SingleLogin
    {
        /// <summary>
        /// 过期时间，前台每29秒向后台发送一次数据。因为js可能会出现延迟。所以，在后台采35秒来判断过期。这样尽量会降低出现错误的概率
        /// </summary>
        public static int TimeOut = 35;

        /// <summary>
        /// 判断该用户是否已经登录，如果已经登录，本次登录就应该做出限制
        /// </summary>
        /// <param name="uName">用户名</param>
        /// <param name="count">最大登录数 默认为1</param>
        /// <returns></returns>
        public static bool isLogin(string uName, int count = 1)
        {
            if (HttpContext.Current.Cache["userCache"] != null)//判断该用户的登录数量是否已达到最大登录数，如果已达到则不允许该用户再次登录
            {
                List<UserCache> list = HttpContext.Current.Cache["userCache"] as List<UserCache>;
                if (list != null)
                {
                    if (list.Any(x => x.UserName == uName && (DateTime.Now - x.CreateDate).TotalSeconds < TimeOut))
                    {
                        var sList = list.Where(x => x.UserName == uName && (DateTime.Now - x.CreateDate).TotalSeconds < TimeOut);
                        if (sList != null)
                        {
                            int sCount = GetUserCount(sList.ToList());
                            if (sCount >= count)//判断当前在线用户是否大于或等于最大登录数，如果大于或者等于，表明不允许再次登录
                            {
                                return true;
                            }
                        }
                    }
                    //更新cache里面的记录
                    SetCache(uName);
                    //清除过期的缓存信息
                    ClearOutDateCache();
                    return false;
                }
            }
            //当前没有用户登录该账号，或登录数量未超过最大登录数，可以再次登录。更新缓存，表明当前用户已登录
            List<UserCache> userList = new List<UserCache>(){
                    new UserCache(){UserName=uName,CreateDate=DateTime.Now,SessionId=HttpContext.Current.Session.SessionID}
                };
            HttpContext.Current.Cache["userCache"] = userList;
            return false;
        }

        /// <summary>
        /// 更新缓存 页面每隔一定时间会向发送一个空的请求，此时可以更新服务器中的缓存，标志该会话尚未结束
        /// </summary>
        public static void SetCache()
        {
            string uName = Util.GetUserName();
            if (!string.IsNullOrEmpty(uName))
            {
                SetCache(uName);
            }
        }

        public static void SetCache(string uName)
        {
            //将已登录的用户名和登录时间存到缓存中，再次登录时可以判断是否已经登录，如果已经登录则不允许用户继续登录
            if (HttpContext.Current.Cache["userCache"] == null)
            {
                List<UserCache> list = new List<UserCache>(){
                        new UserCache(){UserName=uName,CreateDate=DateTime.Now,SessionId=HttpContext.Current.Session.SessionID}
                    };
                HttpContext.Current.Cache["userCache"] = list;
            }
            else
            {
                List<UserCache> list = HttpContext.Current.Cache["userCache"] as List<UserCache>;
                if (list != null)
                {
                    if (list.Any(x => x.UserName == uName && x.SessionId == HttpContext.Current.Session.SessionID))//更新缓存中的时间
                    {
                        UserCache info = list.Where(x => x.UserName == uName && x.SessionId == HttpContext.Current.Session.SessionID).FirstOrDefault();
                        if (info != null)
                        {
                            info.CreateDate = DateTime.Now;
                        }
                        HttpContext.Current.Cache["userCache"] = list;
                    }
                    else
                    {
                        list.Add(new UserCache() { UserName = uName, CreateDate = DateTime.Now, SessionId = HttpContext.Current.Session.SessionID });
                        HttpContext.Current.Cache["userCache"] = list;
                    }
                }
                else
                {
                    List<UserCache> userList = new List<UserCache>(){
                            new UserCache(){UserName=uName,CreateDate=DateTime.Now,SessionId=HttpContext.Current.Session.SessionID}
                        };
                    HttpContext.Current.Cache["userCache"] = userList;
                }
            }
        }

        /// <summary>
        /// 登出时清除登录信息中的缓存
        /// </summary>
        public static void ClearCache()
        {
            string uName = Util.GetUserName();
            if (HttpContext.Current.Cache["userCache"] != null && !string.IsNullOrEmpty(uName))
            {
                List<UserCache> list = HttpContext.Current.Cache["userCache"] as List<UserCache>;
                if (list != null)
                {
                    UserCache info = list.Where(x => x.UserName == uName && x.SessionId == HttpContext.Current.Session.SessionID).FirstOrDefault();
                    if (info != null)
                    {
                        list.Remove(info);
                        HttpContext.Current.Cache["userCache"] = list;
                    }
                }
            }
        }

        /// <summary>
        /// 判断用户登录信息是否已经过期，已经过期后将其踢出系统
        /// <param name="count">最大登录数 默认为1</param>
        /// </summary>
        public static bool isReLogin(int count = 1)
        {
            //获取cookie 如果issingle为false则表明为ip登录则不对其做限制
            if (HttpContext.Current.Request.Cookies["issingle"] != null && HttpContext.Current.Request.Cookies["issingle"].Value == "false")
            {
                return false;
            }
            string uName = Util.GetUserName();
            if (!string.IsNullOrEmpty(uName))
            {
                //从缓存中读取数据  查看该用户信息是否已经过期
                if (HttpContext.Current.Cache["userCache"] != null)
                {
                    List<UserCache> list = HttpContext.Current.Cache["userCache"] as List<UserCache>;
                    if (list != null)
                    {
                        //在缓存中存在用户的数据，并且sessionid不同，则踢掉当前的登录
                        var sList = list.Where(x => x.UserName == uName && (DateTime.Now - x.CreateDate).TotalSeconds < TimeOut && x.SessionId != HttpContext.Current.Session.SessionID);
                        if (sList != null)
                        {
                            int sCount = GetUserCount(sList.ToList());
                            if (sCount >= count)//判断当前在线用户是否大于或等于最大登录数，如果大于或者等于，表明不允许再次登录
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 清除已经过期的缓存
        /// </summary>
        public static void ClearOutDateCache()
        {
            //清除已经过期的用户登录信息
            List<UserCache> list = HttpContext.Current.Cache["userCache"] as List<UserCache>;
            List<UserCache> newList = new List<UserCache>();
            if (list != null)
            {
                foreach (UserCache info in list)
                {
                    if ((DateTime.Now - info.CreateDate).TotalSeconds >= TimeOut)
                    {
                        continue;
                    }
                    newList.Add(info);
                }
                if (newList != null && newList.Count != list.Count)
                {
                    list = newList;
                    HttpContext.Current.Cache["userCache"] = list;
                }
            }
        }

        /// <summary>
        /// 根据SessionID分组  获得当前用户登录数
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static int GetUserCount(List<UserCache> list)
        {
            int count = 0;
            string sessionid = "";
            foreach (UserCache info in list)
            {
                if (sessionid != info.SessionId)
                {
                    count++;
                    sessionid = info.SessionId;
                }
            }
            return count;
        }
    }

    //用户登录的缓存类
    public class UserCache
    {
        public string UserName { get; set; }//用户名
        public DateTime CreateDate { get; set; }//创建时间
        public string SessionId { get; set; }//SessionID 判断是否为同一用户
    }
}