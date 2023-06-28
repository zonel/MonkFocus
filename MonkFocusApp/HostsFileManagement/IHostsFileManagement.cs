using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusApp.HostsFileManagement
{
    public interface IHostsFileManagement
    {
        public bool blockWebsite(string websiteAddress);
        public bool unblockWebsite(string websiteAddress);
    }
}
