//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartServiceweb.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRegister
    {
        public int RegistrationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int FileId { get; set; }
        public string GCMId { get; set; }
        public Nullable<bool> IsNotification { get; set; }
    }
}
