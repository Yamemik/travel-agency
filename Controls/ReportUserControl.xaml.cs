using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows.reports;

namespace TravelAgency.Controls
{
    public partial class ReportUserControl : UserControl
    {
        public ReportUserControl()
        {
            InitializeComponent();

            QueryingEntities();
        }

        private void BtnCustomer_Click(object sender, RoutedEventArgs e)
        {
            var customer = cm_customer.SelectedItem; 

            ReportWindow1 report = new ReportWindow1(((TravelAgency.Customer)customer).Id);
            report.ShowDialog();
        }

        private void QueryingEntities()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Customer>? ent = db.Customers;

                cm_customer.ItemsSource = ent.ToList<Customer>();
            }
        }

        private void BtnHotels_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
