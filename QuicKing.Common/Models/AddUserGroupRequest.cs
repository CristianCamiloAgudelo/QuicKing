using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace QuicKing.Common.Models
{
    public class AddUserGroupRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

    }

}
