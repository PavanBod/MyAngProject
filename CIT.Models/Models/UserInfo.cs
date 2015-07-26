using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Web;

namespace CIT.Models
{
    public class UserInfo
    {
        [Key]
        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Int64 Telephone { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}