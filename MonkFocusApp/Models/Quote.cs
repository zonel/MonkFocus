using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusModels
{
    public class Quote
    {
        [Key]
        public int QuoteId { get; set; }
        public string FullQuote { get; set; }
        public string Author { get; set; }
    }
}
