using System.Security.Principal;
using System.Web.Security;
using System.Web.Mvc;
using System;

[AttributeUsage(System.AttributeTargets.All)]
public class AuthorizePermissionBypass : System.Attribute
{
    //private bool _bypass = false;
    //public AuthorizePermissionBypass(Boolean ByPassAuth)
    //{
    //    this._bypass = ByPassAuth;
    //}
}
