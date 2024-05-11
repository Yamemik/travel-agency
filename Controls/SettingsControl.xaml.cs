using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows;
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
            hotelWindow.Show();
        }

        private void BtnTours_Click(object sender, RoutedEventArgs e)
        {
            ToursWindow toursWindow = new ToursWindow();
            toursWindow.Owner = this.mama;
            toursWindow.Show();
        }

        private void BtnUsers_Click(object sender, RoutedEventArgs e)
        {
            UsersWindow usersWindow = new UsersWindow();
            usersWindow.Owner = this.mama;
            usersWindow.Show();
        }
    }
}
