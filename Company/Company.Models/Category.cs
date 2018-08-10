using System;
using Company.Extensions;

namespace Company.Models
{
    public class Category : BaseModel
    {
        private string _name;
        public string Name
        {
            get {
                return _name;
            }

            set {
                _name = value;
                this.ItemKey = value.CreateItemKey();
            }
        }

        public string ItemKey { get; private set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}
