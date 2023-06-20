using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusModels
{
    public class WebsitesToBlock
    {
        [Key]
        public int WebsitesToBlockId { get; set; }
        public string Domain { get; set; }
    }
}
