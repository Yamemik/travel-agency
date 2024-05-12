using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using TravelAgency.Windows.tours;

namespace TravelAgency.Windows
{
    public partial class ToursWindow : Window
    {
        public Flight? selectedRow { get; set; }

        public ToursWindow(bool isSelect = false)
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
                IQueryable<Tour>? ent = db.Tours?
                    .Include(c => c.Country)
                    .Include(c => c.Hotel);

                DGrid.ItemsSource = ent?.ToList<Tour>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditToursWindow addEditPage = new AddEditToursWindow(((Button)sender).DataContext as Tour);
            addEditPage.ShowDialog();
            QueryingEntities();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditToursWindow addEditPage = new AddEditToursWindow(null!);
            addEditPage.ShowDialog();
            QueryingEntities();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = DGrid.SelectedItems.Cast<Tour>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {rowsForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<Tour>? ent = db.Tours
                            .Where(c => c.Id == rowsForRemoving[0].Id);

                        if (ent is null)
                        {
                            MessageBox.Show("No tours found to delete.");
                            return;
                        }
                        else
                        {
                            db.Tours.RemoveRange(ent);
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
            selectedRow = (Flight)((Button)sender).DataContext;

            this.Close();
        }
    }
}
