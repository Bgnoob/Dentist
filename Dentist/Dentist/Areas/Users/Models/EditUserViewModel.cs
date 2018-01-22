using Dentist.Areas.Admin.Models;
using Dentist.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Dentist.Areas.Users.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int Years { get; set; }

        //public static Expression<Func<ApplicationUser, EditUserViewModel>> Create
        //{
        //    get
        //    {
        //        return u => new EditUserViewModel()
        //        {

        //            Id = u.Id,
        //            Username = u.UserName,
        //            Firstname=u.FirstName,
        //            Lastname = u.LastName,
        //            Years=u.Years
        //        };
        //    }

        //}
    }
}