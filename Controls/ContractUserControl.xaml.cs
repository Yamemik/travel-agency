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

        private void QueryingContracts(string sort = " ")
        {
            using (TravelDBContext db = new())
            {
                // запрос на получение всех категорий и связанных с ними продуктов
                IQueryable<Contract>? contracts = contracts = db.Contracts
                        .Include(c => c.Tour)
                        .Include(c => c.Customers);

                if (sort == "abc")
                {
                    contracts = db.Contracts
                        .Include(c => c.Tour)
                        .Include(c => c.Customers)
                        .OrderBy(c => c.Tour.CompanyServiceCost);
                }
                if (sort == "cba")
                {
                    contracts = db.Contracts
                        .Include(c => c.Tour)
                        .Include(c => c.Customers)
                        .OrderByDescending(c => c.Tour.CompanyServiceCost);
                }

                //DGridContracts.ItemsSource.D;
                DGridContracts.ItemsSource = contracts.ToList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //AddEditPageHotel addEdit = new AddEditPageHotel(((Button)sender).DataContext as Hotel);
            //addEdit.ShowDialog();

            QueryingContracts();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //AddEditPageHotel addEdit = new AddEditPageHotel(null);
            //addEdit.ShowDialog();

            QueryingContracts();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = DGridContracts.SelectedItems.Cast<Contract>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {rowsForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        // запрос на получение всех категорий и связанных с ними продуктов
                        IQueryable<Contract>? ent = db.Contracts
                            .Where(c => c.Id == rowsForRemoving[0].Id);

                        if (ent is null)
                        {
                            MessageBox.Show("No products found to delete.");
                            return;
                        }
                        else
                        {
                            db.Contracts.RemoveRange(ent);
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
            if (ComboPrice.SelectedIndex == 1)
            {
                QueryingContracts("abc");
            }
            if (ComboPrice.SelectedIndex == 2)
            {
                QueryingContracts("cba");
            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
