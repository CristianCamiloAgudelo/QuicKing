using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using QuicKing.Common.Models;
using QuicKing.Common.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuicKing.Prism.ViewModels
{
    public class TaxiHistoryPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IApiService _apiService;
        private TaxiResponse _taxi;
        private DelegateCommand _checkPlaqueCommand;
        private bool _isRunning;
        private List<TripItemViewModel> _details;

        public TaxiHistoryPageViewModel(
            INavigationService navigationService,
            IApiService apiService) : base(navigationService)
        {
            _navigationService = navigationService;
            _apiService = apiService;
            Title = "Historial del Vehiculo";
        }

        public List<TripItemViewModel> Details
        {
            get => _details;
            set => SetProperty(ref _details, value);
        }


        public bool IsRunning
        {
            get => _isRunning;
            set => SetProperty(ref _isRunning, value);
        }


        public TaxiResponse Taxi
        {
            get => _taxi;
            set => SetProperty(ref _taxi, value);
        }

        public string Plaque { get; set; }

        public DelegateCommand CheckPlaqueCommand => _checkPlaqueCommand ?? (_checkPlaqueCommand = new DelegateCommand(CheckPlaqueAsync));

        private async void CheckPlaqueAsync()
        {
            if (string.IsNullOrEmpty(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "Tu debes ingresar un placa.",
                    "Aceptar");
                return;
            }

            Regex regex = new Regex(@"^([A-Za-z]{3}\d{3})$");
            if (!regex.IsMatch(Plaque))
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    "La placa debe empezar por 3 letras y terminar en 3 numeros",
                    "Aceptar");
                return;
            }

            IsRunning = true;
            var url = App.Current.Resources["UrlAPI"].ToString();
            var connection = await _apiService.CheckConnectionAsync(url);
            if (!connection)
            {
                IsRunning = false;
                await App.Current.MainPage.DisplayAlert("Error", "Verifica tu conexión a internet", "Aceptar");
                return;
            }

            Response response = await _apiService.GetTaxiAsync(Plaque, url, "api", "/Taxis");
            IsRunning = false;


            if (!response.IsSuccess)
            {
                await App.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

            Taxi = (TaxiResponse)response.Result;
            Details = Taxi.Trips.Where(t => t.Qualification != 0).Select(t => new TripItemViewModel(_navigationService)
            {
                EndDate = t.EndDate,
                Id = t.Id,
                Qualification = t.Qualification,
                Remarks = t.Remarks,
                Source = t.Source,
                SourceLatitude = t.SourceLatitude,
                SourceLongitude = t.SourceLongitude,
                StartDate = t.StartDate,
                Target = t.Target,
                TargetLatitude = t.TargetLatitude,
                TargetLongitude = t.TargetLongitude,
                TripDetails = t.TripDetails,
                User = t.User
            }).OrderByDescending(t => t.StartDate).ToList();

        }
    }

}
