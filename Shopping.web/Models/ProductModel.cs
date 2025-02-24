﻿namespace Shopping.web.Models;

public class ProductModel
{
    public long Id { get; set; }
    public String Name { get; set; }
    public Decimal Price { get; set; }
    public String Description { get; set; }
    public String CategoryName { get; set; }
    public String ImageUrl { get; set; }

    public string SubstringName()
    {
        if(Name.Length <= 25)
            return Name;
        
        return $"{Name.Substring(0, 22)}...";
    }
    public string SubstringDescription()
    {
        if(Description.Length <= 355)
            return Description;
        
        return $"{Description.Substring(0, 352)}...";
    }
}