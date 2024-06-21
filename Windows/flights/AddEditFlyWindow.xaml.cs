using Microsoft.EntityFrameworkCore;
using System.Windows;
using TravelAgency.Windows.airlines;

namespace TravelAgency.Windows.flights
{
    public partial class AddEditFlyWindow : Window
    {
        private Flight _currentData = new Flight();

        public AddEditFlyWindow(Flight? selected)
        {
            InitializeComponent();

            if (selected != null)
            {
                _currentData = selected;

                using (TravelDBContext db = new())
                {
                    IQueryable<Flight>? flights = db.Flights
                        .Include(c => c.Airline)
                        .Include(c => c.DepartureCountry)
                        .Include(c => c.DestinationCountry);

                    //var fly = flights.ToList<Flight>().First();
                    //txtDepartureCountry.Text = fly.DestinationCountry.Name;
                }
            }

            DataContext = _currentData;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using (TravelDBContext db = new())
            {
                try
                {
                    if (_currentData.Id == 0)
                    {

                        //_currentData.NumberOfFreeSeats = 1;

                        db.Flights.Add(_currentData);
                    }
                    else
                    {
                        Flight updateTour = db.Flights
                            .First(c => c.Id == _currentData.Id);

                        updateTour.AirlineId = _currentData.AirlineId;
                        updateTour.CompanyPrice = _currentData.CustomerPrice;
                        updateTour.CustomerPrice = _currentData.CustomerPrice;
                        updateTour.DepartureCountryId = _currentData.DepartureCountryId;
                        updateTour.DestinationCountryId = _currentData.DestinationCountryId;
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
            AirlinesWindow win = new AirlinesWindow(true);
            win.Owner = this;
            win.ShowDialog();

            var select = win.selectedRow;

            if (select is not null)
            {
                _currentData.Airline = null!;
                _currentData.AirlineId = select.Id;
                txtAir.Text = select.Name;
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
                _currentData.DepartureCountry = null!;
                _currentData.DepartureCountryId = select.Id;
                txtDepartureCountry.Text = select.Name;
            }
        }

        private void DeparFly_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CountriesWindow win = new CountriesWindow(true);
            win.Owner = this;
            win.ShowDialog();

            var select = win.selectedRow;

            if (select is not null)
            {
                _currentData.DestinationCountry = null!;
                _currentData.DestinationCountryId = select.Id;
                txtDestinationCountry.Text = select.Name;
            }
        }
    }
}
