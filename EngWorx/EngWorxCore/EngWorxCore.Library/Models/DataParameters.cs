using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

public class DataParameter
{
    private string _sort = "";
    private string _sortDir = "";
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private int _totalRows;

    public int pageNumber
    {
        get
        {
            return _pageNumber;
        }
        set
        {
            _pageNumber = value;
        }
    }
    public int pageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = value;
        }
    }
    public int totalRows
    {
        get
        {
            return _totalRows;
        }
        set
        {
            _totalRows = value;
        }
    }
    public string sort
    {
        get
        {
            return _sort;
        }
        set
        {
            _sort = value;
        }
    }
    public string sortDir
    {
        get
        {
            return _sortDir;
        }
        set
        {
            _sortDir = value;
        }
    }

    public DataParameter()
    {
    }
}
