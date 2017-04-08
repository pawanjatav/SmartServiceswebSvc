using SmartServiceweb.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SmartServiceweb
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService" in both code and config file together.
    [ServiceContract]
    public interface IService
    {
        // TODO: Add your service operations here [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetCategoryList/{CategoryID}")]
        List<Category> GetCategoryList(string CategoryID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetPrivacyTypeList/{PrivacyTypeID}")]
        List<PrivacyType> GetPrivacyTypeList(string PrivacyTypeID);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RegisterUser")]
        void RegisterUser(UserRegister obj);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "LoginUser")]
        ReturnValues LoginUser(Login obj);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetUserInfo/{UserID}")]
        List<UserDataRegister> GetUserInfo(string UserID);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddBlog")]
        ReturnValues AddBlog(AddBlogs obj);

        [OperationContract]
        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "AddUpdateBlogComment")]
        ReturnValues AddUpdateBlogComment(BlogComments obj);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetfileInfo/{fileID}")]
        List<FileSettings> GetfileInfo(string fileID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetDocuments/{SourceID}/{Filtype}")]
        List<FileSettings> GetDocuments(string SourceID, string Filtype);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetBlogList/{BlogID}/{CategoryID}/{page}/{pageSize}")]
        List<AddBlogData> GetBlogList(string BlogID, string CategoryID,string page, string pageSize);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "UserComment/{BlogID}")]
        List<BlogComments> UserComment(string BlogID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "DeleteBlogComment/{CommentId}")]
        ReturnValues DeleteBlogComment(string CommentId);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "UserLikes/{BlogID}/{UserID}")]
        ReturnValues UserLikes(string BlogID, string UserID);

        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "ForgetPassword/{emailID}")]
        ReturnValues ForgetPassword(string emailID);

    }
}
