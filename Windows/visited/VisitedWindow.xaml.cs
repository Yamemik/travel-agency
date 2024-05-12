using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Windows.visited
{
    public partial class VisitedWindow : Window
    {
        public VisitedExcursion? selectedRow { get; set; }

        public VisitedWindow(bool isSelect = false)
        {
            InitializeComponent();

            QueryingEntities();

            if (isSelect)
            {
                BtnSelectColumn.Visibility = Visibility.Visible;
                BtnEditColumn.Visibility = Visibility.Hidden;

                BtnAdd.Visibility = Visibility.Hidden;
                //BtnDelete.Visibility = Visibility.Hidden;
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
                IQueryable<VisitedExcursion>? ent = db.VisitedExcursions?
                    .Include(c => c.Contract)
                    .Include(c => c.Excursion);

                DGrid.ItemsSource = ent?.ToList<VisitedExcursion>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //AddEditToursWindow addEditPage = new AddEditToursWindow(((Button)sender).DataContext as Tour);
            //addEditPage.ShowDialog();
            QueryingEntities();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //AddEditToursWindow addEditPage = new AddEditToursWindow(null!);
            //addEditPage.ShowDialog();
            QueryingEntities();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rowsForRemoving = DGrid.SelectedItems.Cast<VisitedExcursion>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {rowsForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<VisitedExcursion>? ent = db.VisitedExcursions
                            .Where(c => c.ContractId == rowsForRemoving[0].ContractId);

                        if (ent is null)
                        {
                            MessageBox.Show("No tours found to delete.");
                            return;
                        }
                        else
                        {
                            db.VisitedExcursions.RemoveRange(ent);
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
            selectedRow = (VisitedExcursion)((Button)sender).DataContext;

            this.Close();
        }
    }
}
