using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi.Models
{
    public class VobModel
    {
        public Guid Id { get; set; }
        public string Word { get; set; }
        public int WordType { get; set; }
        public string Spelling { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
        public DateTime? CreatedDate { get; set; }

        public string WordTypeName { get; set; }
    }
}
