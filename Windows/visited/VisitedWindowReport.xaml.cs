using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace TravelAgency.Windows.visited
{
    public partial class VisitedWindowReport : Window
    {
        public VisitedWindowReport()
        {
            InitializeComponent();
            QueryingEntities();
        }

        private void QueryingEntities()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<VisitedExcursion>? ent = db.VisitedExcursions?
                    .Include(c => c.Contract)
                    .Include(c => c.Excursion);

                DGrid.ItemsSource = ent?.ToList<VisitedExcursion>();
            }
        }

        private void BtnTours_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
