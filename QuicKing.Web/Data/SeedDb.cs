using QuicKing.Common.Enums;
using QuicKing.Web.Data.Entities;
using QuicKing.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuicKing.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext dataContext,
                      IUserHelper userHelper)
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _dataContext.Database.EnsureCreatedAsync();
            await CheckRolesAsync();
            var admin = await CheckUserAsync("1010", "Cristian", "Agudelo", "camiloagdu@gmail.com", "314 860 6045", "Universidad de Caldas", UserType.Admin);
            var driver = await CheckUserAsync("2020", "Cristian", "Agudelo", "camiloagdu@hotmail.com", "314 860 6045", "Universidad de Caldas", UserType.Driver);
            var user1 = await CheckUserAsync("3030", "Cristian", "Agudelo", "cristian.1701322386@ucaldas.edu.co", "314 860 6045", "Universidad de Caldas", UserType.User);
            var user2 = await CheckUserAsync("4040", "Cristian", "Agudelo", "camiloagdu1@gmail.com", "314 860 6045", "Universidad de Caldas", UserType.User);
            await CheckTaxisAsync(driver, user1, user2);
        }
        private async Task<UserEntity> CheckUserAsync(
           string document,
           string firstName,
           string lastName,
           string email,
           string phone,
           string address,
           UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new UserEntity
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    UserType = userType
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);

            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Driver.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }

        private async Task CheckTaxisAsync(
            UserEntity driver,
            UserEntity user1,
            UserEntity user2)
        {
            if (!_dataContext.Taxis.Any())
            {
                _dataContext.Taxis.Add(new TaxiEntity
                {
                    User = driver,
                    Plaque = "AAA123",
                    Trips = new List<TripEntity>
                    {
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.5f,
                            Source = "Universidad de Caldas - Sede Central",
                            Target = "Universidad de Caldas - Sede Palogrande",
                            Remarks = "Proin elementum viverra eros, sed varius orci euismod vitae. Phasellus nec bibendum erat. In suscipit dignissim ullamcorper. Nam sem odio, tempus at ante non, scelerisque maximus lectus. Aenean ipsum sapien, maximus elementum elit sit amet, feugiat consequat ipsum. Pellentesque a enim vitae purus luctus sodales. Praesent semper ipsum vitae ipsum porta, nec porta nunc efficitur. Nulla vulputate massa et nunc efficitur ornare. Cras pellentesque elit eu congue viverra. Vivamus porta metus a molestie facilisis. Nam consequat et magna nec eleifend. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Sed arcu magna, scelerisque non lacinia quis, dictum ac libero. Ut iaculis ligula dui, eget porttitor nisi tincidunt ac.",
                            User = user1
                        },
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.8f,
                            Source = "Universidad de Caldas - Sede Palogrande",
                            Target = "Universidad de Caldas - Sede Central",
                            Remarks = "Pellentesque facilisis odio ut consequat dignissim. Nulla commodo ligula sit amet faucibus maximus. Fusce posuere ornare massa, a volutpat ex egestas ac. Pellentesque quis ornare ipsum. Mauris in mauris feugiat tortor semper volutpat in nec nibh. Nullam convallis pretium feugiat. Cras in interdum mauris. Maecenas magna mauris, dapibus eu posuere a, venenatis eget felis. Quisque consequat bibendum commodo. Nunc lobortis, velit dapibus laoreet ornare, turpis leo vestibulum urna, at porttitor ligula felis ac purus.",
                            User = user1
                        }
                    }
                });

                _dataContext.Taxis.Add(new TaxiEntity
                {
                    Plaque = "ONH599",
                    User = driver,
                    Trips = new List<TripEntity>
                    {
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.5f,
                            Source = "Universidad de Caldas - Sede Bellas Artes",
                            Target = "Universidad Nacional - La Nubia",
                            Remarks = "Muy buen servicio",
                            User = user2
                        },
                        new TripEntity
                        {
                            StartDate = DateTime.UtcNow,
                            EndDate = DateTime.UtcNow.AddMinutes(30),
                            Qualification = 4.8f,
                            Source = "Universidad Nacional - La Nubia",
                            Target = "Universidad de Caldas - Sede Bellas Artes",
                            Remarks = "Conductor muy amable",
                            User = user2
                        }
                    }
                });

                await _dataContext.SaveChangesAsync();
            }
        }
    }
}

