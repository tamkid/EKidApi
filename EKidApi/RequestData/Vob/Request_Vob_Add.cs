using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EKidApi.RequestData.Vob
{
    public class Request_Vob_Add
    {
        [Required]
        public string Word { get; set; }
        public int WordType { get; set; }
        public string Spelling { get; set; }
        [Required]
        public string Meaning { get; set; }
    }
}
