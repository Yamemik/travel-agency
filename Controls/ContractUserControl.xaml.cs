using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows;


namespace TravelAgency.Controls
{
    public partial class ContractUserControl : UserControl
    {
        public ContractUserControl()
        {
            InitializeComponent();

            DataContext = this;

            QueryingContracts();
        }

        MainWindow? win;
        public MainWindow mama
        {
            set { win = value; }
            get { return win!; }
        }

        private void QueryingContracts()
        {
            using (TravelDBContext db = new())
            {
                // запрос на получение всех категорий и связанных с ними продуктов
                IQueryable<Contract>? contracts = db.Contracts
                    .Include(c => c.Tour)
                    .Include(c => c.Customers);

                DGridContracts.ItemsSource = contracts.ToList<Contract>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(((Button)sender).DataContext as Hotel);
            addEditPage.Show();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(null);
            addEditPage.Show();;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var hotelsForRemoving = DGridContracts.SelectedItems.Cast<Hotel>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {hotelsForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        // запрос на получение всех категорий и связанных с ними продуктов
                        IQueryable<Hotel>? hotels = db.Hotels
                            .Where(c => c.Id == hotelsForRemoving[0].Id);

                        if (hotels is null)
                        {
                            MessageBox.Show("No products found to delete.");
                            return;
                        }
                        else
                        {
                            db.Hotels.RemoveRange(hotels);
                        }
                        int affected = db.SaveChanges();
                    }
                    MessageBox.Show("Данные удалены");

                    QueryingContracts();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnTours_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateTours()
        {

        }
        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void ComboPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTours();
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
