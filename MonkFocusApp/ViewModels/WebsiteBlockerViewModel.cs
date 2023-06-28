using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MonkFocusApp.Commands;
using MonkFocusApp.DTO;
using MonkFocusDataAccess;
using MonkFocusRepositories;
using MonkFocusApp.HostsFileManagement;

namespace MonkFocusApp.ViewModels
{
    public class WebsiteBlockerViewModel : BaseViewModel
    {
        private string _addWebsiteName;
        public string AddWebsiteName
        {
            get { return _addWebsiteName; }
            set
            {
                _addWebsiteName = value;
                OnPropertyChanged(nameof(AddWebsiteName));
            }
        }

        private string _removeWebsiteName;
        public string RemoveWebsiteName
        {
            get { return _removeWebsiteName; }
            set
            {
                _removeWebsiteName = value;
                OnPropertyChanged(nameof(RemoveWebsiteName));
            }
        }

        private ObservableCollection<HostsFileDTO> _websitesToBlock;
        private readonly int _userId;
        private readonly HostsFileManagement.HostsFileManagement _HFM;

        public ObservableCollection<HostsFileDTO> WebsitesToBlock
        {
            get { return _websitesToBlock; }
            set
            {
                _websitesToBlock = value;
                OnPropertyChanged(nameof(WebsitesToBlock));
            }
        }

        public ICommand AddWebsiteCommand { get; }
        public ICommand RemoveWebsiteCommand { get; }

        public WebsiteBlockerViewModel(int userId)
        {
            _userId = userId;
            _HFM = new HostsFileManagement.HostsFileManagement();
            AddWebsiteCommand = new RelayCommand(AddWebsite);
            RemoveWebsiteCommand = new RelayCommand(DeleteWebsite);
            PopulateHostsList();
        }

        private void DeleteWebsite()
        {
            if (RemoveWebsiteName is null)
            {
                MessageBox.Show("Your field is empty!");
                return;
            }
            if(!_HFM.unblockWebsite(RemoveWebsiteName.Trim())) MessageBox.Show("Website not removed. Please try again.");
            PopulateHostsList();
            RemoveWebsiteName = "";
        }

        private void AddWebsite()
        {
            if (AddWebsiteName is null)
            {
                MessageBox.Show("Your field is empty!");
                return;
            }
            if(!_HFM.blockWebsite(AddWebsiteName.Trim())) MessageBox.Show("Website you've entered may be already on the list.");
            PopulateHostsList();
            AddWebsiteName = "";
        }

        private void PopulateHostsList()
        {
            if (!_HFM.IsRunningAsAdmin())
            {
                MessageBox.Show("You don't have administrator privileges. This module won't work.");
                return;
            }

            IEnumerable<HostsFileDTO> blockedWebsites = _HFM.getBlockedWebsites();
            List<HostsFileDTO> tempWebsitesToBlock = blockedWebsites.ToList();

            WebsitesToBlock = new ObservableCollection<HostsFileDTO>(tempWebsitesToBlock);
        }
    }
}
