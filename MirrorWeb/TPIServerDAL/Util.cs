using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DRMS.TPIServerDAL
{
    public class Util
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public static string SubNote(ref string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }

            string note = "";
            int noteIndex = title.IndexOf("<note>");
            if (noteIndex > 0)
            {
                note = title.Substring(noteIndex);
                title = title.Substring(0, noteIndex);
            }
            title = title.Replace("subscript", "sub");
            title = title.Replace("superscript", "sup");

            //处理图片
            Util util = new Util();
            title = util.ReplacePicUrlForBook(title);

            //处理文本
            title = util.ReplaceText(title);

            return note;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private string ReplaceText(string title)
        {
            title = title.Replace("<#text>", "");
            title = title.Replace("</#text>", "");
            return title;
        }

        /// <summary>
        /// 替换采集数据中正文的图片路径
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private string ReplacePicUrlForBook(string html)
        {
            return Regex.Replace(html, @"\{(?<id>[^\{\}\.]*)(?<type>\.[^\{\}\.]*)\}", new MatchEvaluator(picurlforbook), RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 替换具体的图片路径
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private string picurlforbook(Match m)
        {
            string filetype = m.Groups["type"].Value.ToLower();
            if (filetype == ".jpg" || filetype == ".gif" || filetype == ".jpeg" || filetype == ".png" || filetype == ".bmp")
            {
                return "<img src=\"getpicdata.aspx?key=" + m.Groups["id"].Value + "&type=" + m.Groups["type"].Value + "\" border=\"0\" style=\"height:21px;\" >";
            }
            else
            {
                return "<a href=\"Showpic.aspx?key=" + m.Groups["id"].Value + "&type=" + m.Groups["type"].Value + "\" target=\"blank\" class=\"more\">文件下载</a>";
            }
        }
    }
}
