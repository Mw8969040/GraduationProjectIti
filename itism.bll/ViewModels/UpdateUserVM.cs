using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.bll.ViewModels
{
    public class UpdateUserVM : BaseUserVM
    {
        public int Id { get; set; }
        [Remote(action: "CheckIfEmailUnique", controller: "User", AdditionalFields = nameof(Id))]
        public override string? Email { get; set; }
    }

}
