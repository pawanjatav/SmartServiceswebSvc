using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
using SmartServiceweb.Model;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Transactions;

namespace SmartServiceweb
{
    /// <summary>
    /// Summary description for PicBlog
    /// </summary>
    public class PicBlog : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                context.Response.ContentType = "text/plain";
                context.Response.Expires = -1;
                try
                {
                    HttpPostedFile postedFile = context.Request.Files[0];

                    string savepath = "";
                    string tempPath = "";
                    tempPath = "Uploads/BlogDoc";
                    savepath = context.Server.MapPath(tempPath);
                    string filename = Guid.NewGuid() + postedFile.FileName;
                    if (!Directory.Exists(savepath))
                        Directory.CreateDirectory(savepath);
                    Guid objguid = Guid.NewGuid();
                    postedFile.SaveAs(savepath + @"\" + filename.Replace(" ", ""));

                    context.Response.StatusCode = 200;
                    string[] keys = context.Request.Form.AllKeys;
                    Service s = new Service();
                    string Blogid = (context.Request.Form["BlogIDs"] != null ? context.Request.Form["BlogIDs"] : "0");

                    Repository.ResizeImage ri = new Repository.ResizeImage();
                    string base64 = ri.SaveImage(context.Request.Files[0].InputStream, 250, 250, tempPath, context, filename);
                    s.AddBlogsdocs(filename, int.Parse(Blogid));
                    context.Response.Write(base64);
                    trans.Complete();
                }
                catch (Exception ex)
                {
                    context.Response.Write("Error: " + ex.Message);
                    trans.Dispose();
                }
                finally
                {
                    trans.Dispose();
                }
            }
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