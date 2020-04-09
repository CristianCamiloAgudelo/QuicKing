using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuicKing.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace QuicKing.Prism.ViewModels
{
    public class TaxiMasterDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public TaxiMasterDetailPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            _navigationService = navigationService;
            LoadMenus();
        }

        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        private void LoadMenus()
        {
            List<Menu> menus = new List<Menu>
            {
                new Menu
                {
                    Icon = "ic_airport_shuttle",
                    PageName = "HomePage",
                    Title = "Nuevo Viaje"
                },
                new Menu
                {
                    Icon = "ic_local_taxi",
                    PageName = "TaxiHistoryPage",
                    Title = "Mira el historial del vehiculo"
                },
                new Menu
                {
                    Icon = "ic_nature_people",
                    PageName = "GroupPage",
                    Title = "Administrar mi grupo familiar"
                },
                new Menu
                {
                    Icon = "ic_account_circle",
                    PageName = "ModifyUserPage",
                    Title = "Modificar Usuario"
                },
                new Menu
                {
                    Icon = "ic_report",
                    PageName = "ReportPage",
                    Title = "Reportar un incidente"
                },
                new Menu
                {
                    Icon = "ic_exit_to_app",
                    PageName = "LoginPage",
                    Title = "Log in"
                }
            };

            Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(m => new MenuItemViewModel(_navigationService)
                {
                    Icon = m.Icon,
                    PageName = m.PageName,
                    Title = m.Title
                }).ToList());
        }
    }

}
