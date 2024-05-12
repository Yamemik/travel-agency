using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using TravelAgency.Windows.flights;

namespace TravelAgency.Windows.tours
{
    public partial class AddEditToursWindow : Window
    {
        private Tour _currentData = new Tour();
        public AddEditToursWindow(Tour? selectedTour)
        {
            InitializeComponent();

            if (selectedTour != null)
            {
                _currentData = selectedTour;

                dPstartDate.SelectedDate = _currentData.StartDate.ToDateTime(TimeOnly.MinValue);
                dPfinishDate.SelectedDate = _currentData.FinishDate.ToDateTime(TimeOnly.MinValue);

                using (TravelDBContext db = new())
                {
                    IQueryable<Flight>? flights = db.Flights
                        .Where(c => c.Id == _currentData.DepartureFlightId)
                        .Include(c => c.Airline)
                        .Include(c => c.DestinationCountry);

                    var fly = flights.ToList<Flight>().First();
                    txtDepartureFlight.Text = fly.Airline.Name + "-" + fly.DestinationCountry.Name;

                    flights = db.Flights
                        .Where(c => c.Id == _currentData.ArrivalFlightId)
                        .Include(c => c.Airline)
                        .Include(c => c.DestinationCountry);

                    fly = flights.ToList<Flight>().First();
                    txtArrivalFlight.Text = fly.Airline.Name + "-" + fly.DestinationCountry.Name;
                }
            }
            else
            {
                DateTime thisDay = DateTime.Today;

                dPstartDate.SelectedDate = thisDay;
                dPfinishDate.SelectedDate = thisDay;
            }

            DataContext = _currentData;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TravelDBContext db = new())
            {
                try
                {
                    if (dPstartDate.SelectedDate.HasValue && dPfinishDate.SelectedDate.HasValue)
                    {
                        DateTime dateTime = dPstartDate.SelectedDate.Value;
                        _currentData.StartDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                        dateTime = dPfinishDate.SelectedDate.Value;
                        _currentData.FinishDate = new DateOnly(dateTime.Year, dateTime.Month, dateTime.Day);
                    }

                    if (_currentData.Id == 0)
                    {

                        _currentData.NumberOfFreeSeats = 1;                        

                        db.Tours.Add(_currentData);
                    }
                    else
                    {
                        Tour updateTour = db.Tours
                            .First(c => c.Id == _currentData.Id);

                        updateTour.HotelId = _currentData.HotelId;
                        updateTour.CountryId = _currentData.CountryId;
                        updateTour.DepartureFlightId = _currentData.DepartureFlightId;
                        updateTour.ArrivalFlightId = _currentData.ArrivalFlightId;
                        updateTour.StartDate = _currentData.StartDate;
                        updateTour.FinishDate = _currentData.FinishDate;
                        updateTour.CompanyServiceCost = _currentData.CompanyServiceCost;
                    }

                    // сохранение отслеживаемых изменений в базе данных
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена!");

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("Закрыть?", "Закрыть", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (response == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void CMHotels_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HotelWindow win = new HotelWindow(true);
            win.Owner = this;
            win.ShowDialog();

            var select = win.selectedHotel;

            if (select is not null)
            {
                _currentData.Hotel = null!;
                _currentData.HotelId = select.Id;
                txtHotels.Text = select.Name;
            }            
        }

        private void CMCountry_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CountriesWindow win = new CountriesWindow(true);
            win.Owner = this;
            win.ShowDialog();

            var select = win.selectedRow;

            if (select is not null)
            {
                _currentData.Country = null!;
                _currentData.CountryId = select.Id;
                txtCountry.Text = select.Name;
            }

        }

        private void DeparFly_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FlightsWindow win = new FlightsWindow(true);
            win.Owner = this;
            win.ShowDialog();

            var select = win.selectedRow;

            if (select is not null)
            {
                _currentData.DepartureFlightId = select.Id;
                txtDepartureFlight.Text = select.Airline.Name + "-" + select.DestinationCountry.Name;
            }
        }

        private void ArrivalFly_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            FlightsWindow win = new FlightsWindow(true);
            win.Owner = this;
            win.ShowDialog();

            var select = win.selectedRow;

            if (select is not null)
            {
                _currentData.ArrivalFlightId = select.Id;
                txtArrivalFlight.Text = select.Airline.Name + "-" + select.DestinationCountry.Name;
            }
        }
    }
}
