using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows.contracts;

namespace TravelAgency.Windows.reports
{
    public partial class ReportWindow1 : Window
    {
        public ReportWindow1(int id)
        {
            InitializeComponent();

            QueryingEntities(id);

        }

        private void QueryingEntities(int id)
        {
            using (TravelDBContext db = new())
            {
                var ent = db.Customers.FirstOrDefault(c => id == c.Id);

                IQueryable<Customer>? cus = db.Customers
                    .Include(c => c.Contracts)
                    .Where(c => c.Id == id);

                if (ent is not null)
                {
                    txt_fio.Text = ent.Surname + " " + ent.Name + " " + ent.Patronymic;
                    txt_date_now.Text = DateTime.Now.ToString();

                    DGridContracts.ItemsSource = (cus.ToList<Customer>()[0]).Contracts;
                }
            }             
        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            AboutContratWindow addEditPage = new AboutContratWindow(((Button)sender).DataContext as Contract);
            addEditPage.ShowDialog();
        }
    }
}
