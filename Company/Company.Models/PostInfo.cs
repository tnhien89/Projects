using System;
using Company.Extensions;
using Company.Extensions.Attributes;

namespace Company.Models
{
    public class PostInfo
    {
        public long Id { get; set; }
        private string _title;
        public string Title {
            get {
                return _title;
            }

            set {
                _title = value;

                if (!string.IsNullOrEmpty(value))
                {
                    ItemKey = value.CreateItemKey();
                }
            }
        }
        public string ItemKey { get; private set; }

        public string Type { get; set; }
        public string Images { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int WageMin { get; set; }
        public int WageMax { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Status { get; set; }
        public string PostLevel { get; set; }
        public string CommentStatus { get; set; }
        [IgnoreParam]
        public int CommentCount { get; set; }
        public string Password { get; set; }
        public long ParentId { get; set; }
        public int MenuId { get; set; }
        [IgnoreParam]
        public string MenuName { get; set; }

        [IgnoreParam]
        public DateTimeOffset? CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        [IgnoreParam]
        public DateTimeOffset? UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        [IgnoreParam]
        public string UpdatedByUsername { get; set; }

        public string GetWageString()
        {
            if (WageMin > 0 && WageMax > 0)
            {
                return WageMin.ToString() + " - " + WageMax.ToString();
            }

            if (WageMin > 0)
            {
                return WageMin.ToString();
            }

            return " (Thỏa Thuận)";
        }

        public string GetCompanyAddress()
        {
            string rs = this.Address;
            if (!string.IsNullOrEmpty(this.Town))
            {
                if (!string.IsNullOrEmpty(rs))
                {
                    rs += ", ";
                }

                rs += this.Town;
            }

            if (!string.IsNullOrEmpty(this.City))
            {
                if (!string.IsNullOrEmpty(rs))
                {
                    rs += ", ";
                }

                rs += this.City;
            }

            return rs;
        }
    }
}
