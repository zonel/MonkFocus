using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MonkFocusApp.DTO;
using MonkFocusApp.HostsFileManagement;

namespace MonkFocusApp.HostsFileManagement
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
        }

        #endregion

        public bool IsRunningAsAdmin()
        {
            using (System.Security.Principal.WindowsIdentity identity =
                   System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                System.Security.Principal.WindowsPrincipal principal =
                    new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
        }

        public bool blockWebsite(string websiteAddress)
        {
            List<string> hostsFile = File.ReadAllLines(FilePath).ToList();

            bool containsThatWebsite = hostsFile
                .Any(line => line
                    .Contains("127.0.0.1 " + websiteAddress) || line
                    .Contains("127.0.0.1 www." + websiteAddress));

            if (containsThatWebsite) return false;

            using (StreamWriter writer = File.AppendText(FilePath))
            {
                writer.WriteLine("127.0.0.1 " + websiteAddress);
                writer.WriteLine("127.0.0.1 www." + websiteAddress);
            }

            return true;
        }

        public bool unblockWebsite(string websiteAddress)
        {
            List<string> hostsFile = File.ReadAllLines(FilePath).ToList();
            bool containsThatWebsite = hostsFile
                .Any(line => line
                    .Contains("127.0.0.1 " + websiteAddress) || line
                    .Contains("127.0.0.1 www." + websiteAddress));
            if (string.IsNullOrEmpty(websiteAddress)) return false;
            if (!containsThatWebsite) return false;


            hostsFile.RemoveAll(line => line.Contains("127.0.0.1 " + websiteAddress));
            hostsFile.RemoveAll(line => line.Contains("127.0.0.1 www." + websiteAddress));

            File.WriteAllLines(FilePath, hostsFile);

            return true;
        }

        public IEnumerable<HostsFileDTO> getBlockedWebsites()
        {
            var lines = File.ReadAllLines(FilePath);
            var rows = new List<HostsFileDTO>();

            for (int i = 0; i < lines.Length; i++)
            {
                if (string.IsNullOrEmpty(lines[i])) continue;
                if (lines[i].Contains("#")) continue;
                lines[i] = lines[i].Replace("127.0.0.1", "");

                rows.Add(new HostsFileDTO { ID = i + 1, Content = lines[i] });
            }

            return rows;
        }
    }
}