using System;
using System.Collections.Generic;

#nullable disable

namespace EKidApi.EF
{
    public partial class Vob
    {
        public Guid Id { get; set; }
        public string Word { get; set; }
        public int WordType { get; set; }
        public string Spelling { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
