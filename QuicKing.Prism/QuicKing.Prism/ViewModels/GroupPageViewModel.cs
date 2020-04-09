using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuicKing.Prism.ViewModels
{
    public class GroupPageViewModel : ViewModelBase
    {
        public GroupPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Administrar Mi Grupo Familiar";
        }
    }
}
