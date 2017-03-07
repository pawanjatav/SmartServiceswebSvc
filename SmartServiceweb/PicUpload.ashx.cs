using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
 
using System.Web.Script.Serialization;
using System.Drawing;
using SmartServiceweb.Model;

namespace SmartServiceweb
{
    /// <summary>
    /// Summary description for PicUpload
    /// </summary>
    public class PicUpload : IHttpHandler
    {


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Expires = -1;
            try
            {
                HttpPostedFile postedFile = context.Request.Files[0];

                string savepath = "";
                string tempPath = "";
                tempPath = "Uploads/ProfilePic";
                savepath = context.Server.MapPath(tempPath);
                string filename = Guid.NewGuid() + postedFile.FileName;
                if (!Directory.Exists(savepath))
                    Directory.CreateDirectory(savepath);
                Guid objguid = Guid.NewGuid();
                postedFile.SaveAs(savepath + @"\" + filename.Replace(" ", ""));

                context.Response.StatusCode = 200;
                string[] keys = context.Request.Form.AllKeys;
                Service s = new Service();

                UserRegister ur = new UserRegister();
                ur.FirstName = context.Request.Form["FirstName"];
                ur.LastName = context.Request.Form["LastName"];
                ur.Email = context.Request.Form["Email"];
                ur.UserName = context.Request.Form["UserName"];
                ur.Password = context.Request.Form["Password"];
                ur.Mobile = context.Request.Form["Mobile"];
                ur.FilePathName = filename;

                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(context.Request.InputStream))
                {
                    fileData = binaryReader.ReadBytes(context.Request.Files[0].ContentLength);
                }
                ur.FileName = fileData;
                s.RegisterUser(ur);
                Repository.ResizeImage ri = new Repository.ResizeImage();
                string base64 = ri.SaveImage(context.Request.Files[0].InputStream, 250, 250, "Uploads/ProfilePic", context, filename);

                context.Response.Write(base64);
            }
            catch (Exception ex)
            {
                context.Response.Write("Error: " + ex.Message);
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