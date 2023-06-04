namespace MonkFocusLib
{
    public class HostsFileManagement
    {
        private OperatingSystem OS = Environment.OSVersion;

        #region FilePath
        private Dictionary<PlatformID, string> HostsFilePaths { get; } = new Dictionary<PlatformID, string>
        {
            [PlatformID.Win32NT] = "C:\\Windows\\System32\\drivers\\etc\\hosts",
            [PlatformID.Unix] = "/etc/hosts",
            [PlatformID.MacOSX] = "/private/etc/hosts"
        };
        public string FilePath
        {
            get
            {
                if (HostsFilePaths.TryGetValue(OS.Platform, out string path))
                {
                    return path;
                }
            
                Console.WriteLine("Unsupported platform.");
                return string.Empty;
            }
        }
        #endregion

        public HostsFileManagement()
        {
            if (!IsRunningAsAdmin())
            {
                Console.WriteLine("Please run the program as administrator to edit the hosts file.");
                return;
            }
        }

        private static bool IsRunningAsAdmin()
        {
            using (System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
        }

        public void blockWebsite(string websiteAddress)
        {
            if (!IsRunningAsAdmin())
            {
                Console.WriteLine("Please run the program as administrator to edit the hosts file.");
                return;
            }

            using (StreamWriter writer = File.AppendText(FilePath))
            {
                writer.WriteLine("127.0.0.1 " + websiteAddress);
            }
        }

    }
}