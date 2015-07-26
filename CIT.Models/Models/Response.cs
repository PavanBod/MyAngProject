using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CIT.Models
{
    public class Response
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ResponseMessage { get; set; }
        public DateTime RepliedOn { get; set; }

        public virtual UserInfo RepliedBy { get; set; }
        public virtual Post RepliedFor { get; set; }
    }
}