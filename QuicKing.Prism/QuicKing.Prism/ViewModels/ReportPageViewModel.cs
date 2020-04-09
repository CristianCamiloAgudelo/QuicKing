using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuicKing.Prism.ViewModels
{
    public class ReportPageViewModel : ViewModelBase
    {
        public ReportPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "Reportar un Incidente";
        }
    }
}
