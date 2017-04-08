
namespace SmartServiceweb.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Web;


  #region ["Return Values"]
        [DataContract]
        public class ReturnValues
        {
            [DataMember]
            public string Success { get; set; }
            [DataMember]
            public string Failure { get; set; }

            [DataMember]
            public string Source { get; set; }
            [DataMember]
            public bool Status { get; set; }
            [DataMember]
            public string UserLikes { get; set; }
            


        }
        #endregion

        #region Login
        [DataContract]
        public class Login
        {
            [DataMember]
            public string UserName { get; set; }
            [DataMember]
            public string Password { get; set; }
        }
        #endregion

        [DataContract]
        public partial class AddBlogData
        {
            [DataMember]
            public int BlogId { get; set; }
            [DataMember]
            public int UserID { get; set; }
            [DataMember]
            public int CategoryID { get; set; }
            [DataMember]
            public string textContent { get; set; }
            [DataMember]
            public System.DateTime CreatedDate { get; set; }
            [DataMember]
            public System.DateTime UpdatedDate { get; set; }
            [DataMember]
            public int PrivacyID { get; set; }
            [DataMember]
            public string UserLikes { get; set; }
            [DataMember]
            public List<string> Fileinfo { get; set; }
            [DataMember]
            public List<UserRegister> Userinfo { get; set; }
        }
   [DataContract]
        public partial class UserRegister
        {
            [DataMember]           
            public byte[] FileName { get; set; }
            [DataMember]           
            public string FilePathName { get; set; }
        
        
        }

    [DataContract]
    public class UserDataRegister
    {
        [DataMember]
        public string FilePathName{ get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Mobile { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public int RegistrationID { get; set; }

        [DataMember]
        public string GCMId { get; set; }



    }
}