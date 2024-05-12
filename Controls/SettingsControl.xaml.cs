using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows;
using TravelAgency.Windows.airlines;
using TravelAgency.Windows.customers;
using TravelAgency.Windows.flights;
using TravelAgency.Windows.users;

namespace TravelAgency.Controls
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }

        MainWindow? win;
        public MainWindow mama
        {
            set { win = value; }
            get { return win!; }
        }

        private void BtnHotels_Click(object sender, RoutedEventArgs e)
        {
            HotelWindow hotelWindow = new HotelWindow(false);
            hotelWindow.Owner = this.mama;
            hotelWindow.ShowDialog();
        }

        private void BtnTours_Click(object sender, RoutedEventArgs e)
        {
            ToursWindow toursWindow = new ToursWindow();
            toursWindow.Owner = this.mama;
            toursWindow.ShowDialog();
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow();
            usersWindow.Owner = this.mama;
            usersWindow.ShowDialog();
        }

        private void BtnCountries_Click(object sender, RoutedEventArgs e)
        {
            CountriesWindow countriesWindow = new CountriesWindow();
            countriesWindow.Owner = this.mama;
            countriesWindow.ShowDialog();
        }

        private void BtnFlights_Click(object sender, RoutedEventArgs e)
        {
            FlightsWindow win = new FlightsWindow();
            win.Owner = this.mama;
            win.ShowDialog();
        }

        private void BtnAirlines_Click(object sender, RoutedEventArgs e)
        {
            AirlinesWindow win = new AirlinesWindow();
            win.Owner = this.mama;
            win.ShowDialog();
        }

        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomersWindow win = new CustomersWindow();
            win.Owner = this.mama;
            win.ShowDialog();
        }
    }
}
