using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Windows.airlines
{
    public partial class AirlinesWindow : Window
    {
        public Airline? selectedRow { get; set; }
        public AirlinesWindow(bool isSelect = false)
        {
            InitializeComponent();

            QueryingEntities();

            if (isSelect)
            {
                BtnSelectColumn.Visibility = Visibility.Visible;
                BtnEditColumn.Visibility = Visibility.Hidden;

                BtnAdd.Visibility = Visibility.Hidden;
                BtnDelete.Visibility = Visibility.Hidden;

            }
            else
            {
                BtnSelectColumn.Visibility = Visibility.Hidden;
            }
        }

        private void QueryingEntities()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<Airline>? ent = db.Airlines;

                DGrid.ItemsSource = ent.ToList<Airline>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditAirlinesWindow addEdit = new AddEditAirlinesWindow(((Button)sender).DataContext as Airline);
            addEdit.Show();
            this.Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditAirlinesWindow addEdit = new AddEditAirlinesWindow(null!);
            addEdit.Show();
            this.Close();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rowForRemoving = DGrid.SelectedItems.Cast<Airline>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {rowForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<Airline>? entities = db.Airlines
                            .Where(c => c.Id == rowForRemoving[0].Id);

                        if (entities is null)
                        {
                            MessageBox.Show("No entities found to delete.");
                            return;
                        }
                        else
                        {
                            db.Airlines.RemoveRange(entities);
                        }
                        int affected = db.SaveChanges();
                    }
                    MessageBox.Show("Данные удалены");

                    QueryingEntities();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnTours_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            selectedRow = (Airline)((Button)sender).DataContext;

            this.Close();
        }
    }
}
