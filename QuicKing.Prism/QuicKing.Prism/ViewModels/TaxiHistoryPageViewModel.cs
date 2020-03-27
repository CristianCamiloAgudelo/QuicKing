using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuicKing.Prism.ViewModels
{
    public class TaxiHistoryPageViewModel : ViewModelBase
    {
        public TaxiHistoryPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            Title = "See Taxi History";
        }
    }
}
