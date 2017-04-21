using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SmartServiceweb.Model;
using System.Transactions;

using System.ServiceModel.Web;
using System.Text;
using SmartServiceweb.Repository;

namespace SmartServiceweb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service.svc or Service.svc.cs at the Solution Explorer and start debugging.
    public class Service : IService
    {
        #region ["Add_Update_Comment_Likes"]
        public List<BlogComments> UserComment(string BlogID)
        {
            RepsistoryEF<BlogComments> _BlogComm = new global::RepsistoryEF<BlogComments>();
            int blgid = int.Parse(BlogID);
            return _BlogComm.GetListBySelector(z => z.BlogId == blgid).ToList();
        }
        public ReturnValues DeleteBlogComment(string CommentId)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<BlogComments> _BlogComm = new global::RepsistoryEF<BlogComments>();
                    BlogComments _updateblog = new BlogComments();
                    int blgid = int.Parse(CommentId);
                    _updateblog = _BlogComm.GetListBySelector(z => z.CommentId == blgid).FirstOrDefault();
                    _BlogComm.Delete(_updateblog);

                    ReturnValues objReturn = new ReturnValues
                    {
                        Success = "Delete Successfully",
                        Status = true
                    };
                    trans.Complete();
                    return objReturn;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public ReturnValues AddUpdateBlogComment(BlogComments obj)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<BlogComments> _BlogdocF = new global::RepsistoryEF<BlogComments>();
                    BlogComments _updateblog = new BlogComments();
                    _updateblog = _BlogdocF.GetListBySelector(z => z.CommentId == obj.CommentId && z.UserID == obj.UserID).FirstOrDefault();
                    if (_updateblog != null)
                    {
                        _updateblog.Comment = obj.Comment;
                        obj.CreatedDate = DateTime.Now;
                        _BlogdocF.Update(_updateblog);
                    }
                    else
                    {
                        obj.CreatedDate = DateTime.Now;
                        _BlogdocF.Save(obj);
                    }
                    trans.Complete();
                    ReturnValues objReturn = new ReturnValues
                    {
                        Success = "Success",
                        Status = true
                    };
                    return objReturn;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public ReturnValues UserLikes(string BlogID, string UserID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<AddBlogs> _BlogdocF = new global::RepsistoryEF<AddBlogs>();
                    AddBlogs _updateblog = new AddBlogs();
                    int BlgID = int.Parse(BlogID);
                    _updateblog = _BlogdocF.GetListBySelector(z => z.BlogId == BlgID).FirstOrDefault();
                    if (_updateblog != null)
                    {
                        _updateblog.UserLikes += UserID + ",";
                        _BlogdocF.Update(_updateblog);
                    }
                    trans.Complete();
                    ReturnValues objReturn = new ReturnValues
                    {
                        UserLikes = _updateblog.UserLikes,
                        Success = "Success",
                        Status = true

                    };
                    return objReturn;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        #endregion

        #region ["GetCategoryList"]
        public List<Category> GetCategoryList(string CategoryID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<Category> _o = new global::RepsistoryEF<Model.Category>();
                    int catID = 0;
                    List<Category> lst = new List<Category>();
                    if (CategoryID.Trim() != "L")
                    {
                        catID = int.Parse(CategoryID); lst = _o.GetListBySelector(z => z.CategoryID == catID).ToList();
                    }
                    else
                    {
                        lst = _o.GetList().OrderBy(z => z.CatOrderBy).ToList();
                    }
                    trans.Complete();
                    return lst;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    throw;
                }
                finally
                {
                    trans.Dispose();
                }
            }

        }
        #endregion

        #region [GetPrivacyTypeList]
        public List<PrivacyType> GetPrivacyTypeList(string PrivacyTypeID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<PrivacyType> _o = new global::RepsistoryEF<Model.PrivacyType>();
                    int ID = 0;
                    List<PrivacyType> lst = new List<PrivacyType>();
                    if (PrivacyTypeID.Trim() != "L")
                    {
                        ID = int.Parse(PrivacyTypeID);
                        lst = _o.GetListBySelector(z => z.PrivacyID == ID).ToList();
                    }
                    else
                    {
                        lst = _o.GetList().OrderBy(z => z.PrivacyOrderBy).ToList();
                    }
                    trans.Complete();
                    return lst;
                }
                catch (Exception)
                {
                    trans.Dispose();
                    throw;
                }
                finally
                {
                    trans.Dispose();
                }
            }

        }
        #endregion

        #region [Registration/Login]
        public void sendnote(string messages)
        {
            //   ReposNotification.sendAndroidPush(messages);
            var getBlogData = GetBlogListByID(messages);
            if (getBlogData != null)
            {
                ReposNotification.sendAndroidPush(getBlogData);
            }
        }
        public void RegisterUser(UserDataRegister obj)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<UserRegister> _o = new global::RepsistoryEF<UserRegister>();
                    UserRegister resultValue = new UserRegister();
                    obj.CreateDate = DateTime.Now;
                    if (obj.RegistrationID > 0)
                    {
                        resultValue = _o.GetListBySelector(z => z.RegistrationID == obj.RegistrationID).FirstOrDefault();
                        if (resultValue != null)
                        {
                            resultValue.FirstName = obj.FirstName;
                            resultValue.LastName = obj.LastName;
                            resultValue.Email = obj.Email;
                            resultValue.Mobile = obj.Mobile;
                            resultValue.GCMId = obj.GCMId;
                            var es = _o.Update(resultValue);
                        }
                    }
                    else
                    {
                        UserRegister _obj = new UserRegister
                        {
                            RegistrationID = obj.RegistrationID,
                            FirstName = obj.FirstName,
                            LastName = obj.LastName,
                            UserName = obj.UserName,
                            Email = obj.Email,
                            Password = obj.Password,
                            Mobile = obj.Mobile,
                            CreateDate = DateTime.Now,
                            GCMId = obj.GCMId
                        };

                        resultValue = _o.Save(_obj);
                    }
                    if (obj.FileName != null)
                    {
                        RepsistoryEF<FileSettings> _F = new global::RepsistoryEF<FileSettings>();
                        FileSettings objf = new FileSettings { FileType = FileType.UserProfile.ToString(), SourceID = resultValue.RegistrationID, FilePath = obj.FilePathName };
                        _F.Save(objf);
                    }
                    ReturnValues result = null;
                    if (resultValue != null)
                    {
                        result = new ReturnValues
                        {
                            Success = "Registeration Successfully Done ",
                        };
                    }
                    trans.Complete();
                    //  return result;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public ReturnValues LoginUser(Login obj)
        {

            try
            {
                RepsistoryEF<Model.UserRegister> _o = new global::RepsistoryEF<Model.UserRegister>();

                var resultValue = _o.GetListBySelector(z => z.UserName == obj.UserName && z.Password == obj.Password).FirstOrDefault();
                ReturnValues result = null;
                if (resultValue != null)
                {
                    if (obj.GCMId != null && obj.GCMId != string.Empty)
                    {
                        UserDataRegister _obj = new UserDataRegister
                        {
                            RegistrationID = resultValue.RegistrationID,
                            FirstName = resultValue.FirstName,
                            LastName = resultValue.LastName,
                            UserName = resultValue.UserName,
                            Email = resultValue.Email,
                            Password = resultValue.Password,
                            Mobile = resultValue.Mobile,
                            GCMId = obj.GCMId
                        };

                        resultValue.GCMId = obj.GCMId;
                         RegisterUser(_obj);
                    }
                    result = new ReturnValues
                    {
                        Success = "Login Successfully",
                        Source = resultValue.RegistrationID.ToString(),
                    };
                }
                else
                {
                    result = new ReturnValues
                    {
                        Success = "Login Failed, Please enter correct username and password",
                        Source = "0",
                    };
                }

                return result;
            }
            catch (Exception ex)
            {

                ReturnValues objex = new ReturnValues
                {
                    Failure = ex.Message,
                    Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                };
                throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
            }
            finally
            {

            }

        }
        public List<UserDataRegister> GetUser(int uid)
        {
            return GetUserInfo(uid.ToString());
        }
        public List<UserDataRegister> GetUserInfo(string UserID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    GmContext _db = new GmContext();
                    // RepsistoryEF<UserRegister> _o = new global::RepsistoryEF<Model.UserRegister>();
                    int UID = 0;
                    List<UserRegister> lst = new List<UserRegister>();
                    if (UserID.Trim() != "L")
                    {
                        UID = int.Parse(UserID);

                        var lsts = _db.UserRegister.Where(z => z.RegistrationID == UID).Join(
         _db.FileSettings.Where(z => z.FileType == "UserProfile"),
         U => U.RegistrationID,
         F => F.SourceID,
         (U, F) => new { u = U, f = F }
     ).Select(us => new
     {
         Email = us.u.Email,
         FilePathName = us.f.FilePath,
         FirstName = us.u.FirstName,
         LastName = us.u.LastName,
         Mobile = us.u.Mobile,
         RegistrationID = us.u.RegistrationID,
         UserName = us.u.UserName,
         FileId = us.f.Id,
         GCMId = us.u.GCMId
     }).AsQueryable();

                        lst = lsts.ToList().Select(us => new UserRegister
                        {
                            Email = us.Email,
                            FilePathName = us.FilePathName,
                            FirstName = us.FirstName,
                            LastName = us.LastName,
                            Mobile = us.Mobile,
                            RegistrationID = us.RegistrationID,
                            UserName = us.UserName,
                            GCMId = us.GCMId,
                            FileId = us.FileId
                        }).ToList();

                    }
                    else
                    {
                        var lsts = _db.UserRegister.Join(_db.FileSettings.Where(z => z.FileType == "UserProfile"), U => U.RegistrationID, F => F.SourceID, (U, F) => new { u = U, f = F }
   ).Select(us => new UserRegister
   {
       Email = us.u.Email,
       FilePathName = us.f.FilePath,
       FirstName = us.u.FirstName,
       LastName = us.u.LastName,
       Mobile = us.u.Mobile,
       RegistrationID = us.u.RegistrationID,
       UserName = us.u.UserName,
       FileId = us.f.Id,
       GCMId = us.u.GCMId
   }).AsQueryable().ToList();

                        lst = lsts.ToList().Select(us => new UserRegister
                        {
                            Email = us.Email,
                            FilePathName = us.FilePathName,
                            FirstName = us.FirstName,
                            LastName = us.LastName,
                            Mobile = us.Mobile,
                            RegistrationID = us.RegistrationID,
                            UserName = us.UserName,
                            GCMId = us.GCMId,
                            FileId = us.FileId
                        }).ToList();

                    }
                    trans.Complete();
                    List<UserDataRegister> udr = new List<UserDataRegister>();
                    foreach (var i in lst)
                    {
                        udr.Add(new UserDataRegister() { LastName = i.LastName, FirstName = i.FirstName, FilePathName = i.FilePathName, GCMId = i.GCMId, Email = i.Email, FileName = i.FileName, Mobile = i.Mobile, RegistrationID = i.RegistrationID, UserName = i.UserName });
                    }
                    return udr;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    throw ex;
                }
                finally
                {
                    trans.Dispose();
                }
            }

        }
        #endregion

        #region ["Documents"]
        public List<FileSettings> GetfileInfo(string fileID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<FileSettings> _o = new global::RepsistoryEF<Model.FileSettings>();
                    int UID = 0;
                    List<FileSettings> lst = new List<FileSettings>();
                    if (fileID.Trim() != "L")
                    {
                        UID = int.Parse(fileID); lst = _o.GetListBySelector(z => z.Id == UID).ToList();
                    }
                    else
                    {
                        lst = _o.GetList().OrderByDescending(z => z.Id).ToList();
                    }
                    trans.Complete();
                    return lst;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    throw;
                }
                finally
                {
                    trans.Dispose();
                }
            }

        }
        public List<FileSettings> GetDocuments(string SourceID, string Filtype)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<FileSettings> _o = new global::RepsistoryEF<Model.FileSettings>();
                    int sID = 0;
                    sID = int.Parse(SourceID);
                    List<FileSettings> lst = new List<FileSettings>();
                    lst = _o.GetListBySelector(z => z.SourceID == sID && z.FileType == Filtype).ToList();

                    trans.Complete();
                    return lst;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    throw;
                }
                finally
                {
                    trans.Dispose();
                }
            }

        }
        #endregion

        #region ["Add Blogs"]

        public AddBlogData GetBlogListByID(string BlogID)
        {
            GmContext _db = new GmContext();

            int BlogsID = int.Parse(BlogID);
            List<AddBlogData> lst = new List<AddBlogData>();
            lst = _db.AddBlogs.Where(z => z.BlogId == BlogsID).OrderByDescending(z => z.BlogId).Select(a => new AddBlogData
            {
                BlogId = a.BlogId,
                CategoryID = a.CategoryID,
                CreatedDate = a.CreatedDate,
                PrivacyID = a.PrivacyID,
                textContent = a.textContent,
                UpdatedDate = a.UpdatedDate,
                UserID = a.UserID,
                UserLikes = a.UserLikes
            }).ToList();
            // Add User Information in against of Blog
            lst.ForEach(a =>
            {
                var us = GetUser(a.UserID).FirstOrDefault();
                List<UserDataRegister> lstUserinfo = new List<UserDataRegister>();
                lstUserinfo.Add(new UserDataRegister
                {
                    Email = us.Email,
                    FilePathName = us.FilePathName,
                    FirstName = us.FirstName,
                    LastName = us.LastName,
                    Mobile = us.Mobile,
                    RegistrationID = us.RegistrationID,
                    UserName = us.UserName,
                    GCMId = us.GCMId
                });
                a.Userinfo = lstUserinfo;
            });


            return lst.FirstOrDefault();


        }

        public ReturnValues DeleteBlog(BlogData obj)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {

                    GmContext _db = new GmContext();
                    int dleteblog = _db.DeleteBlog(obj.BlogId);
                    ReturnValues result = null;
                    if (dleteblog > 0)
                    {
                        result = new ReturnValues
                        {
                            Success = "Post Successfully Removed ",
                            Status = true,
                            Source = obj.BlogId.ToString()
                        };
                    }
                    trans.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public ReturnValues DeleteBlogImage(BlogData obj)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<FileSettings> _db = new RepsistoryEF<FileSettings>();
                    _db.DeleteByExpression(z => (z.FilePath == obj.ImageName));
                    ReturnValues result = null;

                    result = new ReturnValues
                    {
                        Success = "Image Successfully Removed ",
                        Status = true,
                        Source = obj.BlogId.ToString()
                    };

                    trans.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }



        public ReturnValues AddBlog(AddBlogs obj)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<AddBlogs> _o = new global::RepsistoryEF<AddBlogs>();
                    AddBlogs resultValue = new AddBlogs();
                    if (obj.BlogId > 0)
                    {
                        // Update blogs
                        var getSpecificData = _o.GetListBySelector(z => z.BlogId == obj.BlogId).FirstOrDefault();
                        getSpecificData.UpdatedDate = DateTime.Now;
                        getSpecificData.CategoryID = obj.CategoryID;
                        getSpecificData.PrivacyID = obj.PrivacyID;
                        getSpecificData.textContent = obj.textContent;
                        getSpecificData.UserID = obj.UserID;
                        resultValue = _o.Update(getSpecificData);
                    }
                    else
                    {
                        //Add New Blogs
                        obj.CreatedDate = DateTime.Now;
                        obj.UpdatedDate = DateTime.Now;
                        resultValue = _o.Save(obj);
                        var getBlogData = GetBlogListByID(obj.BlogId.ToString());
                        if (getBlogData != null)
                        {
                            ReposNotification.sendAndroidPush(getBlogData);
                        }
                    }

                    ReturnValues result = null;
                    if (resultValue != null)
                    {
                        result = new ReturnValues
                        {
                            Success = "Blog Successfully Added ",
                            Status = true,
                            Source = resultValue.BlogId.ToString()
                        };
                    }
                    trans.Complete();
                    return result;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        public void AddBlogsdocs(string FileName, int BlogID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<AddBlogs> _o = new global::RepsistoryEF<AddBlogs>();
                    if (FileName != null)
                    {

                        // file Setting
                        RepsistoryEF<FileSettings> _F = new global::RepsistoryEF<FileSettings>();
                        FileSettings objf = new FileSettings { FilePath = FileName, FileType = "BlogImage", SourceID = BlogID };
                        _F.Save(objf);

                        RepsistoryEF<BlogDocuments> _BlogdocF = new global::RepsistoryEF<BlogDocuments>();


                        // Blog Documents
                        BlogDocuments objBlogDocf = new BlogDocuments
                        {
                            BlogId = BlogID,
                            FileID = objf.Id,
                            CreatedDate = DateTime.Now
                        };
                        _BlogdocF.Save(objBlogDocf);
                        trans.Complete();
                    }
                    //  return result;
                }
                catch (Exception ex)
                {
                    trans.Dispose();
                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }


        public List<AddBlogData> GetBlogList(string BlogID, string CategoryID, string pages, string pageSizes)
        {
            int page = int.Parse(pages);

            int pageSize = int.Parse(pageSizes);
            try
            {
                GmContext _db = new GmContext();
                //       int TotalSize = _db.AddBlogs.Count();
                int PageSkip = (page - 1) * pageSize;
                int UID = 0;
                List<AddBlogData> lst = new List<AddBlogData>();

                if (BlogID != "null" && BlogID.Trim() != "L")
                {
                    UID = int.Parse(BlogID);
                    lst = _db.AddBlogs.Where(z => z.BlogId == UID).OrderByDescending(z => z.BlogId).Select(a => new AddBlogData
                    {
                        BlogId = a.BlogId,
                        CategoryID = a.CategoryID,
                        CreatedDate = a.CreatedDate,
                        PrivacyID = a.PrivacyID,
                        textContent = a.textContent,
                        UpdatedDate = a.UpdatedDate,
                        UserID = a.UserID,
                        UserLikes = a.UserLikes
                    }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                }
                else if (CategoryID != "null" && BlogID.Trim() != "L")
                {
                    UID = int.Parse(CategoryID);
                    lst = _db.AddBlogs.Where(z => z.CategoryID == UID).OrderByDescending(z => z.BlogId).Select(a => new AddBlogData
                    {
                        BlogId = a.BlogId,
                        CategoryID = a.CategoryID,
                        CreatedDate = a.CreatedDate,
                        PrivacyID = a.PrivacyID,
                        textContent = a.textContent,
                        UpdatedDate = a.UpdatedDate,
                        UserID = a.UserID,
                        UserLikes = a.UserLikes
                    }).Skip(PageSkip).Take(pageSize).ToList();
                }
                else
                {
                    lst = _db.AddBlogs.OrderByDescending(z => z.BlogId).Select(a => new AddBlogData
                    {
                        BlogId = a.BlogId,
                        CategoryID = a.CategoryID,
                        CreatedDate = a.CreatedDate,
                        PrivacyID = a.PrivacyID,
                        textContent = a.textContent,
                        UpdatedDate = a.UpdatedDate,
                        UserID = a.UserID,
                        UserLikes = a.UserLikes
                    }).Skip(PageSkip).Take(pageSize).ToList();

                }
                lst.ForEach(a =>
                {
                    var us = GetUser(a.UserID).FirstOrDefault();
                    List<UserDataRegister> lstUserinfo = new List<UserDataRegister>();
                    lstUserinfo.Add(new UserDataRegister
                    {
                        Email = us.Email,
                        FilePathName = us.FilePathName,
                        FirstName = us.FirstName,
                        LastName = us.LastName,
                        Mobile = us.Mobile,
                        RegistrationID = us.RegistrationID,
                        UserName = us.UserName,
                        GCMId = us.GCMId
                    });
                    a.Userinfo = lstUserinfo;

                    var imginfo = _db.BlogDocuments.Where(z => z.BlogId == a.BlogId).Join(_db.FileSettings.Where(z => z.FileType == "BlogImage"), U => U.FileID, F => F.Id, (U, F) => new { u = U, f = F }).Select(x => new
                    {
                        FilePathName = x.f.FilePath,
                        BlogID = x.u.BlogId

                    }).AsQueryable().ToList();
                    List<string> lstFilepath = new List<string>();
                    foreach (var f in imginfo)
                    {
                        lstFilepath.Add(f.FilePathName);
                    }
                    a.Fileinfo = lstFilepath;

                });
                return lst;
            }
            catch (Exception ex)
            {
                //  trans.Dispose();
                throw ex;
            }
            finally
            {
                // trans.Dispose();
            }
        }

        #region["ForgetPassword"]
        public ReturnValues ForgetPassword(string emailID)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    RepsistoryEF<UserRegister> _o = new global::RepsistoryEF<UserRegister>();
                    ReturnValues ReturnObj;
                    var RegisteredUser = _o.GetListBySelector(z => z.Email == emailID).FirstOrDefault();

                    if (_o.GetListBySelector(z => z.Email == emailID).Any())
                    {
                        reposSendMail o = new reposSendMail();
                        var dd = o.contentBody(RegisteredUser, "ForgetPassword");
                        ReturnObj = new ReturnValues
                        {
                            Success = "Success",
                        };
                        trans.Complete();

                    }
                    else
                    {
                        ReturnObj = new ReturnValues
                        {
                            Success = "Email address not exists, please try again",
                        };
                    }
                    return ReturnObj;
                }
                catch (Exception ex)
                {
                    trans.Dispose();

                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        #endregion


        #region["Get BlogList by UserID"]
        public List<AddBlogData> GetBlogListbyUserID(string CategoryID, string UserID, string pages, string pageSizes)
        {
            int page = int.Parse(pages);
            int pageSize = int.Parse(pageSizes);
            try
            {
                GmContext _db = new GmContext();
                //  int TotalSize = _db.AddBlogs.Count();
                int PageSkip = (page - 1) * pageSize;
                int UID = 0;
                List<AddBlogData> lst = new List<AddBlogData>();

                if (CategoryID != "null" && UserID != "null")
                {
                    UID = int.Parse(CategoryID);
                    var UsID = int.Parse(UserID);

                    lst = _db.AddBlogs.Where(z => z.CategoryID == UID && z.UserID == UsID).OrderByDescending(z => z.BlogId).Select(a => new AddBlogData
                    {
                        BlogId = a.BlogId,
                        CategoryID = a.CategoryID,
                        CreatedDate = a.CreatedDate,
                        PrivacyID = a.PrivacyID,
                        textContent = a.textContent,
                        UpdatedDate = a.UpdatedDate,
                        UserID = a.UserID,
                        UserLikes = a.UserLikes
                    }).Skip(PageSkip).Take(pageSize).ToList();

                }
                else if (UserID != "null")
                {
                    UID = int.Parse(UserID);
                    lst = _db.AddBlogs.Where(z => z.UserID == UID).OrderByDescending(z => z.BlogId).Select(a => new AddBlogData
                    {
                        BlogId = a.BlogId,
                        CategoryID = a.CategoryID,
                        CreatedDate = a.CreatedDate,
                        PrivacyID = a.PrivacyID,
                        textContent = a.textContent,
                        UpdatedDate = a.UpdatedDate,
                        UserID = a.UserID,
                        UserLikes = a.UserLikes
                    }).Skip((page - 1) * pageSize).Take(pageSize).ToList();

                }
                lst.ForEach(a =>
                           {
                               var us = GetUser(a.UserID).FirstOrDefault();
                               List<UserDataRegister> lstUserinfo = new List<UserDataRegister>();
                               lstUserinfo.Add(new UserDataRegister
                               {
                                   Email = us.Email,
                                   FilePathName = us.FilePathName,
                                   FirstName = us.FirstName,
                                   LastName = us.LastName,
                                   Mobile = us.Mobile,
                                   RegistrationID = us.RegistrationID,
                                   UserName = us.UserName,
                                   GCMId = us.GCMId
                               });
                               a.Userinfo = lstUserinfo;
                               var imginfo = _db.BlogDocuments.Where(z => z.BlogId == a.BlogId).Join(_db.FileSettings.Where(z => z.FileType == "BlogImage"), U => U.FileID, F => F.Id, (U, F) => new { u = U, f = F }).Select(x => new
                               {
                                   FilePathName = x.f.FilePath,
                                   BlogID = x.u.BlogId
                               }).AsQueryable().ToList();
                               List<string> lstFilepath = new List<string>();
                               foreach (var f in imginfo)
                               {
                                   lstFilepath.Add(f.FilePathName);
                               }
                               a.Fileinfo = lstFilepath;
                           });

                return lst;
            }
            catch (Exception ex)
            {
                //  trans.Dispose();
                throw ex;
            }
            finally
            {
                // trans.Dispose();
            }
        }

        #endregion

        #region["Change Password"]
        public ReturnValues ChangePassword(ChangeUserPassword obj)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                try
                {
                    ReturnValues ReturnObj = new ReturnValues();
                    RepsistoryEF<UserRegister> _o = new global::RepsistoryEF<UserRegister>();
                    UserRegister resultValue = new UserRegister();
                    if (obj.RegistrationID > 0)
                    {
                        resultValue = _o.GetListBySelector(z => z.RegistrationID == obj.RegistrationID).FirstOrDefault();
                        if (resultValue.Password.Equals(obj.OldPassword))
                        {
                            if (resultValue != null)
                            {
                                resultValue.Password = obj.NewPassword;
                                var es = _o.Update(resultValue);
                                reposSendMail o = new reposSendMail();
                                var dd = o.contentBody(resultValue, "ChangePassword");
                                ReturnObj = new ReturnValues
                                {
                                    Success = "Password has been changed successfully",
                                    Status = true,
                                };
                            }
                        }
                        else
                        {
                            ReturnObj = new ReturnValues
                            {
                                Status = false,
                                Failure = "Old Password does not match"
                            };
                        }
                    }
                    trans.Complete();
                    return ReturnObj;
                }
                catch (Exception ex)
                {
                    trans.Dispose();

                    ReturnValues objex = new ReturnValues
                    {
                        Failure = ex.Message,
                        Source = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri,
                    };
                    throw new WebFaultException<ReturnValues>(objex, System.Net.HttpStatusCode.InternalServerError);
                }
                finally
                {
                    trans.Dispose();
                }
            }
        }

        #endregion

    }



    #endregion


}

