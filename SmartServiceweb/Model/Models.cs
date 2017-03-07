
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
        public partial class AddBlogs
        {
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

 
}