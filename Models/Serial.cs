using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace core_web_api1.Models
{
    public class Serial
    {
        [Key]
        public int SerialId { get; set; }
        public int CityId { get; set; }
    }
}
