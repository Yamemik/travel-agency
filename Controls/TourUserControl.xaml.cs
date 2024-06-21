using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Xps;


namespace TravelAgency.Controls
{
    public partial class TourUserControl : UserControl
    {
        public string? Title { get; set; }

        public TourUserControl()
        {
            InitializeComponent();

            DataContext = this;

            QueryingEntity();
        }

        MainWindow? win;
        public MainWindow mama
        {
            set { win = value; }
            get { return win!; }
        }

        private void QueryingEntity()
        {
            using (TravelDBContext db = new())
            {
                // запрос на получение всех категорий и связанных с ними продуктов
                IQueryable<Excursion>? ent = db.Excursions
                    .Include(c => c.Country);

                DataGridEx.ItemsSource = ent.ToList<Excursion>();
            }
        }


        private void TboxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name = county_name.Text;
            UpdateTours(name);
        }

        private void UpdateTours(string name)
        {
            using (TravelDBContext db = new())
            {
                // запрос на получение всех категорий и связанных с ними продуктов
                IQueryable<Excursion>? ent = db.Excursions
                    .Include(c => c.Country)
                    .Where(c => c.Country.Name.StartsWith(name));

                DataGridEx.ItemsSource = ent.ToList<Excursion>();
            }
        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateTours();
        }

        private void ComboPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateTours();
        }

        private void btnAbout_click(object sender, RoutedEventArgs e)
        {

        }
    }
}