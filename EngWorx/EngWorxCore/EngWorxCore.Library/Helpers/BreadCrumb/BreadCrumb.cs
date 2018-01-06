using System.Security.Principal;
using System.Web.Security;
using System.Web.Mvc;
using System;
using System.Collections.Generic;

[AttributeUsage(System.AttributeTargets.All)]
public class BreadCrumbAttribute : System.Attribute
{

    protected string _description { get; set; }
    protected bool _isRoot { get; set; }
    public BreadCrumbAttribute(string description)
    {
        this._description= description;
        this._isRoot = false;
    }
    public BreadCrumbAttribute(string description,bool isroot)
    {
        this._description = description;
        this._isRoot = isroot;
    }

    public string description
    {
        get { return _description; }
        set { _description = value; }
    }
    public bool isRoot{
        get { return _isRoot; }
        set { _isRoot=value;}
    }
}

public class BreadCrumb
{
    public string controller { get; set; }
    public string action { get; set; }
    public string description { get; set; }
    public string httpMethod { get; set; }
    public IDictionary<string,object> attributes { get; set; }

}
