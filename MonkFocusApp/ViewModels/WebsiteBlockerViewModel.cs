using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusApp.DTO;

namespace MonkFocusApp.ViewModels;

public class WebsiteBlockerViewModel : BaseViewModel
{
    private readonly HostsFileManagement.HostsFileManagement _HFM;
    private readonly int _userId;
    private string _addWebsiteName;

    private string _removeWebsiteName;

    private ObservableCollection<HostsFileDTO> _websitesToBlock;

    public WebsiteBlockerViewModel(int userId)
    {
        _userId = userId;
        _HFM = new HostsFileManagement.HostsFileManagement();
        AddWebsiteCommand = new RelayCommand(AddWebsite);
        RemoveWebsiteCommand = new RelayCommand(DeleteWebsite);
        PopulateHostsList();
    }

    public string AddWebsiteName
    {
        get => _addWebsiteName;
        set
        {
            _addWebsiteName = value;
            OnPropertyChanged();
        }
    }

    public string RemoveWebsiteName
    {
        get => _removeWebsiteName;
        set
        {
            _removeWebsiteName = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<HostsFileDTO> WebsitesToBlock
    {
        get => _websitesToBlock;
        set
        {
            _websitesToBlock = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddWebsiteCommand { get; }
    public ICommand RemoveWebsiteCommand { get; }

    /// <summary>
    ///     This method deletes website from the list of blocked websites.
    /// </summary>
    private void DeleteWebsite()
    {
        if (RemoveWebsiteName is null)
        {
            MessageBox.Show("Your field is empty!");
            return;
        }

        if (!_HFM.unblockWebsite(RemoveWebsiteName.Trim())) MessageBox.Show("Website not removed. Please try again.");
        PopulateHostsList();
        RemoveWebsiteName = "";
    }

    /// <summary>
    ///     This method adds website to the list of blocked websites.
    /// </summary>
    private void AddWebsite()
    {
        if (AddWebsiteName is null)
        {
            MessageBox.Show("Your field is empty!");
            return;
        }

        if (!_HFM.blockWebsite(AddWebsiteName.Trim()))
            MessageBox.Show("Website you've entered may be already on the list.");
        PopulateHostsList();
        AddWebsiteName = "";
    }

    /// <summary>
    ///     This method populates the list of blocked websites.
    /// </summary>
    private void PopulateHostsList()
    {
        if (!_HFM.IsRunningAsAdmin())
        {
            MessageBox.Show("You don't have administrator privileges. This module won't work.");
            return;
        }

        var blockedWebsites = _HFM.getBlockedWebsites();
        var tempWebsitesToBlock = blockedWebsites.ToList();

        WebsitesToBlock = new ObservableCollection<HostsFileDTO>(tempWebsitesToBlock);
    }
}