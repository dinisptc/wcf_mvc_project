using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcTIC_IT.Libraries
{
    public interface IPagedList   
    {  
        int CurrentPage { get; }  
        bool HasNextPage { get; }   
        bool HasPreviousPage { get; }  
        int ItemsPerPage { get; }  
        int TotalItems { get; }  
        int TotalPages { get; } 
    }
}
