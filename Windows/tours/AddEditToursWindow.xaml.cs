using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;

namespace TravelAgency.Windows.tours
{
    public partial class AddEditToursWindow : Window
    {
        private Tour _currentTour = new Tour();
        public Hotel selectedHotel { get; set; }

        public AddEditToursWindow(Tour? selectedTour)
        {
            InitializeComponent();

            if (selectedTour != null)
            {
                _currentTour = selectedTour;

                //current data

                dPstartDate.SelectedDate = _currentTour.StartDate.ToDateTime(TimeOnly.MinValue);
                dPfinishDate.SelectedDate = _currentTour.FinishDate.ToDateTime(TimeOnly.MinValue);

                using (TravelDBContext db = new())
                {
                    IQueryable<Flight>? flights = db.Flights
                        .Where(c => c.Id == _currentTour.DepartureFlightId)
                        .Include(c => c.Airline)
                        .Include(c => c.DestinationCountry);

                    var fly = flights.ToList<Flight>().First();
                    departureFlight.Text = fly.Airline.Name + "-" + fly.DestinationCountry.Name;

                    flights = db.Flights
                        .Where(c => c.Id == _currentTour.ArrivalFlightId)
                        .Include(c => c.Airline)
                        .Include(c => c.DestinationCountry);

                    fly = flights.ToList<Flight>().First();
                    arrivalFlight.Text = fly.Airline.Name + "-" + fly.DestinationCountry.Name;
                }
            }
            else
            {
                DateTime thisDay = DateTime.Today;

                dPstartDate.SelectedDate = thisDay;
                dPfinishDate.SelectedDate = thisDay;
            }

            DataContext = _currentTour;
        }

        private void QueryingHotels()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Hotel>? hotels = db.Hotels;                
            }
        }

        private void QueryingCountries()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Country>? countries = db.Countries;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TravelDBContext db = new())
            {
                try
                {
                    if (_currentTour.Id == 0)
                    {
                        db.Tours.Add(_currentTour);
                    }
                    else
                    {
                        Tour updateTour = db.Tours
                            .First(c => c.Id == _currentTour.Id);

                        updateTour.Hotel = _currentTour.Hotel;
                        updateTour.Country = _currentTour.Country;
                        updateTour.DepartureFlightId = _currentTour.DepartureFlightId;
                        updateTour.ArrivalFlightId = _currentTour.ArrivalFlightId;
                        updateTour.StartDate = _currentTour.StartDate;
                        updateTour.FinishDate = _currentTour.FinishDate;
                        updateTour.CompanyServiceCost = _currentTour.CompanyServiceCost;
                    }

                    // сохранение отслеживаемых изменений в базе данных
                    db.SaveChanges();
                    MessageBox.Show("Информация сохранена!");

                    ToursWindow tourWindow = new ToursWindow();
                    tourWindow.Show();
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
                ToursWindow tourWindow = new ToursWindow();
                tourWindow.Show();
                this.Close();
            }
        }

        private void CMHotels_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HotelWindow hotelWindow = new HotelWindow(true);
            hotelWindow.Owner = this;
            hotelWindow.ShowDialog();            

            _currentTour.Hotel = hotelWindow.selectedHotel;
            _currentTour.HotelId = hotelWindow.selectedHotel.Id;

            txtHotels.Text = hotelWindow.selectedHotel.Name;
        }

        private void CMCountry_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //CountryWindow hotelWindow = new CountryWindow();
            //hotelWindow.Owner = this;
            //hotelWindow.Show();
        }
    }
}
