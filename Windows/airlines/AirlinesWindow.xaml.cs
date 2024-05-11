using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            AddEditPageHotel addEditPage = new AddEditPageHotel(((Button)sender).DataContext as Hotel);
            addEditPage.Show();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(null!);
            addEditPage.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var rowForRemoving = DGrid.SelectedItems.Cast<Country>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {rowForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<Country>? entities = db.Countries
                            .Where(c => c.Id == rowForRemoving[0].Id);

                        if (entities is null)
                        {
                            MessageBox.Show("No entities found to delete.");
                            return;
                        }
                        else
                        {
                            db.Countries.RemoveRange(entities);
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
