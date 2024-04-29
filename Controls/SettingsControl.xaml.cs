using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows;

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
            HotelWindow hotelWindow = new HotelWindow();
            hotelWindow.Owner = this.mama;
            hotelWindow.Show();
        }
    }
}
