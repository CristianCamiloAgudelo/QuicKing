using QuicKing.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuicKing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;

        public SeedDb(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckTaxisAsync();
        }

        private async Task CheckTaxisAsync()
        {
            //true si al menos al un registro
            if (!_dataContext.Taxis.Any())
            {
                _dataContext.Taxis.Add(new TaxiEntity
                {
                    Plaque = "AAA123",
                    Trips = new List<TripEntity>
                    {
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.5f,
                            Source = "UNIVERSIDAD DE CALDAS - CEDE CENTARL",
                            Target = "UNIVERSIDAD DE CALDAS - CEDE BELLAS ARTES",
                            Remarks = "Muy buen servicio"
                        },
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.8f,
                             Source = "UNIVERSIDAD DE CALDAS - CEDE BELLAS ARTES",
                            Target = "UNIVERSIDAD DE CALDAS - CEDE CENTRAL",
                            Remarks = "Muy buen servicio"
                        }
                    }
                });

                _dataContext.Taxis.Add(new TaxiEntity
                {
                    Plaque = "THW321",
                    Trips = new List<TripEntity>
                    {
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.5f,
                            Source = "Universidad de Caldas - Hospital",
                            Target = "Universidad Nacional - La nubia",
                            Remarks = "Conductor muy amable"
                        },
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.8f,
                             Source = "Universidad de Caldas - La nubia",
                            Target = "Universidad Nacional - La Hospital",
                            Remarks = "Conductor muy amable"
                        }
                    }
                });

                await _dataContext.SaveChangesAsync();
            }

        }
    }
}


