using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;


namespace TravelAgency.Controls
{
    public partial class TourUserControl : UserControl
    {
        public string? Title { get; set; }

        public TourUserControl()
        {
            InitializeComponent();

            DataContext = this;

            QueryingTours();
        }

        MainWindow? win;
        public MainWindow mama
        {
            set { win = value; }
            get { return win!; }
        }

        private void QueryingTours()
        {
            using (TravelDBContext db = new())
            {
                // запрос на получение всех категорий и связанных с ними продуктов
                IQueryable<Tour>? tours = db.Tours
                    .Include(c => c.Country)
                    .Include(c => c.Hotel)
                    .Include(c => c.ArrivalFlight)
                    .Include(c => c.DepartureFlight);

                dg_tours.ItemsSource = tours.ToList<Tour>();
            }
        }


        private void TboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTours();
        }

        private void UpdateTours()
        {

        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ChecActual_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void btnAbout_click(object sender, RoutedEventArgs e)
        {

        }
    }
}