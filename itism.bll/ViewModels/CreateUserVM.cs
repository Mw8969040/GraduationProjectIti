using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace itism.bll.ViewModels
{
    public class CreateUserVM : BaseUserVM
    {
        [Remote(action: "CheckIfEmailUnique", controller: "User")]
        public override string? Email { get; set; }
    }
}
