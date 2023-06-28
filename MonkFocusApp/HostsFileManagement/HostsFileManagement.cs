using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using MonkFocusApp.DTO;

namespace MonkFocusApp.HostsFileManagement;

public class HostsFileManagement : IHostsFileManagement
{
    #region Ctor

    public HostsFileManagement()
    {
        OS = Environment.OSVersion;
    }

    #endregion

    /// <summary>
    ///     This method blocks a website by adding it to the hosts file.
    /// </summary>
    /// <param name="websiteAddress">string of website to block, for example: site.com</param>
    /// <returns>True if operation was successful.</returns>
    public bool blockWebsite(string websiteAddress)
        {
        if (!IsRunningAsAdmin()) return false;
        if (string.IsNullOrEmpty(websiteAddress)) return false;

        var hostsFile = File.ReadAllLines(FilePath).ToList();

        var containsThatWebsite = hostsFile
            .Any(line => line
                .Contains("127.0.0.1 " + websiteAddress) || line
                .Contains("127.0.0.1 www." + websiteAddress));

        if (containsThatWebsite) return false;

        using var writer = File.AppendText(FilePath);
        writer.WriteLine("127.0.0.1 " + websiteAddress);
        writer.WriteLine("127.0.0.1 www." + websiteAddress);

        return true;
    }


    /// <summary>
    ///     This method unblocks a website by removing it from the hosts file.
    /// </summary>
    /// <param name="websiteAddress">string of website to block, for example: site.com</param>
    /// <returns>True if operation was successful.</returns>
    public bool unblockWebsite(string websiteAddress)
    {
        if (!IsRunningAsAdmin()) return false;
        if (string.IsNullOrEmpty(websiteAddress)) return false;

        var hostsFile = File.ReadAllLines(FilePath).ToList();
        var containsThatWebsite = hostsFile
            .Any(line => line
                .Contains("127.0.0.1 " + websiteAddress) || line
                .Contains("127.0.0.1 www." + websiteAddress));
        if (!containsThatWebsite) return false;


        hostsFile.RemoveAll(line => line.Contains("127.0.0.1 " + websiteAddress));
        hostsFile.RemoveAll(line => line.Contains("127.0.0.1 www." + websiteAddress));

        File.WriteAllLines(FilePath, hostsFile);

        return true;
    }


    /// <summary>
    ///     This method checks if the application is running as administrator.
    /// </summary>
    /// <returns>true if user is running application as administrator.</returns>
    public bool IsRunningAsAdmin()
    {
        using (var identity =
               WindowsIdentity.GetCurrent())
        {
            var principal =
                new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    /// <summary>
    ///     This method returns a list of blocked websites.
    /// </summary>
    /// <returns>a list of blocked websites without leading DNS address in every line.</returns>
    public IEnumerable<HostsFileDTO> getBlockedWebsites()
    {
        var lines = File.ReadAllLines(FilePath);
        var rows = new List<HostsFileDTO>();

        for (var i = 0; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i])) continue;
            if (lines[i].Contains("#")) continue;
            lines[i] = lines[i].Replace("127.0.0.1", "");

            rows.Add(new HostsFileDTO { ID = i + 1, Content = lines[i] });
        }

        return rows;
    }

    #region Properties

    private readonly OperatingSystem OS;

    private Dictionary<PlatformID, string> HostsFilePaths { get; } = new()
    {
        [PlatformID.Win32NT] = "C:\\Windows\\System32\\drivers\\etc\\hosts",
        [PlatformID.Unix] = "/etc/hosts",
        [PlatformID.MacOSX] = "/private/etc/hosts"
    };

    public string FilePath
    {
        get
        {
            if (HostsFilePaths.TryGetValue(OS.Platform, out var path)) return path;

            Console.WriteLine("Unsupported platform.");
            return string.Empty;
        }
    }

    #endregion
}