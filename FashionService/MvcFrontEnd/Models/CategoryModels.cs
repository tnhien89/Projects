using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.Models
{
    public class ServiceInfo
    {
    }

    public class ServiceMenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsRoot { get; set; }
    }

    public class AllCategoriesModel
    {
        public string Title { get; set; }
        public string PageType { get; set; }
        public int CurrentIndex { get; set; }
        public int CheckId { get; set; }
        public List<MenuBOL> Roots { get; set; }
        public List<NewsBOL> News { get; set; }
        public bool HasChildren { get; set; }

        public AllCategoriesModel()
        {
            CurrentIndex = 0;
            Roots = new List<MenuBOL>();
            News = new List<NewsBOL>();
            HasChildren = false;
        }
    }

    public class AdsModel
    {
        public List<OtherBOL> Parents { get; set; }
        public List<OtherBOL> Childrens { get; set; }

        public AdsModel()
        {
            Parents = new List<OtherBOL>();
            Childrens = new List<OtherBOL>();
        }
    }
}