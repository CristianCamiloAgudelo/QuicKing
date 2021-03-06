﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuicKing.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuicKing.Prism.ViewModels
{
    public class TripDetailPageViewModel : ViewModelBase
    {
        private TripResponse _trip;

        public TripDetailPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            Title = "Detalles del Viaje";
        }

        public TripResponse Trip
        {
            get => _trip;
            set => SetProperty(ref _trip, value);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            if (parameters.ContainsKey("trip"))
            {
                Trip = parameters.GetValue<TripResponse>("trip");
            }
        }
    }

}
