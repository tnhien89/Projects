using Company.Extensions;
using System;
using Company.Extensions.Attributes;

namespace Company.Models
{
    public class Menu : BaseModel
    {
        private string _name;
        public string Name {
            get {
                return _name;
            }

            set {
                _name = value;

                if (!string.IsNullOrEmpty(value))
                {
                    this.ItemKey = value.CreateItemKey();
                }
            }
        }
        public string ItemKey { get; private set; }
        public string MenuType { get; set; }

        public int ParentId { get; set; }
        [IgnoreParam]
        public int RootId { get; set; }
        [IgnoreParam]
        public int MenuLevel { get; set; }
        public string Description { get; set; }
        [IgnoreParam]
        public long PostId { get; set; }
        public string PostContent { get; set; }
        public int Priority { get; set; }

        public Menu()
        { }
    }
}
