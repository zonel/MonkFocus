using MonkFocusApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkFocusApp.HostsFileManagement
{
    /// <summary>
    /// This interface is used to manage the hosts file.
    /// </summary>
    public interface IHostsFileManagement
    {
        public bool blockWebsite(string websiteAddress);
        public bool unblockWebsite(string websiteAddress);
        public bool IsRunningAsAdmin();
        public IEnumerable<HostsFileDTO> getBlockedWebsites();
    }
}
