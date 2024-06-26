﻿using BusinessLayer.Results.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface ILoginService
    {
        IDataResult<Users> CheckLoginCredentials(Users user);
        ClaimsIdentity SetRolesAndAuthenticate(Users user);
    }
}
