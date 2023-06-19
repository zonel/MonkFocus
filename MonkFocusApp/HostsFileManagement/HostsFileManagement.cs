using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MonkFocusApp.HostsFileManagement;

namespace MonkFocusLib
{
    public class HostsFileManagement : IHostsFileManagement
    {

        #region Properties
        private readonly OperatingSystem OS;
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

        #region Ctor

        public HostsFileManagement()
        {
            OS = Environment.OSVersion;
            if (!IsRunningAsAdmin())
            {
                Console.WriteLine("Please run the program as administrator to edit the hosts file.");
                return;
            }
        }

        #endregion

        private bool IsRunningAsAdmin()
        {
            using (System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
        }

        public void blockWebsite(string websiteAddress)
        {
            List<string> hostsFile = File.ReadAllLines(FilePath).ToList();
            bool containsThatWebsite = hostsFile
                .Any(line => line
                    .Contains("127.0.0.1 " + websiteAddress) || line
                    .Contains("127.0.0.1 www." + websiteAddress));

            if (containsThatWebsite)
            {
                Console.WriteLine("Website already blocked.");
                return;
            }
            else
            {
                using (StreamWriter writer = File.AppendText(FilePath))
                {
                    writer.WriteLine("127.0.0.1 " + websiteAddress);
                    writer.WriteLine("127.0.0.1 www." + websiteAddress);
                }
                Console.WriteLine("Blocked website: "+websiteAddress);

            }

            foreach (string host in hostsFile)
            {
                Console.WriteLine(host);
            }
        }

        public void unblockWebsite(string websiteAddress)
        {
            List<string> hostsFile = File.ReadAllLines(FilePath).ToList();
            bool containsThatWebsite = hostsFile
                .Any(line => line
                    .Contains("127.0.0.1 " + websiteAddress) || line
                    .Contains("127.0.0.1 www." + websiteAddress));

            if (!containsThatWebsite)
            {
                Console.WriteLine("Website was not blocked.");
                return;
            }

            hostsFile.RemoveAll(line => line.Contains("127.0.0.1 " + websiteAddress));
            hostsFile.RemoveAll(line => line.Contains("127.0.0.1 www." + websiteAddress));

            File.WriteAllLines(FilePath, hostsFile);

            Console.WriteLine("Unblocked website: " + websiteAddress);

            foreach (string host in hostsFile)
            {
                Console.WriteLine(host);
            }
        }

    }
}