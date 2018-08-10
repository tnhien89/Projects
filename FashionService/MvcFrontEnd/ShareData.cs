using MvcFrontEnd.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFrontEnd
{
    public class ShareData
    {
        public List<MenuBOL> Roots;
        public List<NewsBOL> News;

        public static ShareData Instance { get; private set; }

        static ShareData()
        {
            Instance = new ShareData();
        }

        public ShareData()
        {
            Roots = new List<MenuBOL>();
            News = new List<NewsBOL>();
        }
    }
}