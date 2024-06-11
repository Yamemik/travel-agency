using System.Windows;

namespace TravelAgency.Windows.contracts
{
    public partial class AboutContratWindow : Window
    {
        public AboutContratWindow(Contract? contract)
        {
            InitializeComponent();

            if(contract is null)
            {
                
            }
            else
            {
                txt_title.Text = "Договор № " + contract.Id.ToString();

                // table
                DGridCustomer.ItemsSource = contract.Customers.ToList();

            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
