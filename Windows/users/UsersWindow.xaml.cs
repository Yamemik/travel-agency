using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Windows.users
{
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();

            QueryingTours();

        }

        private void QueryingTours()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<User>? users = db.Users;

                DGridUsers.ItemsSource = users?.ToList<User>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel((sender as Button).DataContext as Hotel);
            addEditPage.Show();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditPageHotel addEditPage = new AddEditPageHotel(null!);
            addEditPage.Show();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var usersForRemoving = DGridUsers.SelectedItems.Cast<User>().ToList();
            if (MessageBox.Show($"Вы точно хотите удалить следущие {usersForRemoving.Count()} элемент?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    using (TravelDBContext db = new())
                    {
                        IQueryable<User>? users = db.Users
                            .Where(c => c.Id == usersForRemoving[0].Id);

                        if (users is null)
                        {
                            MessageBox.Show("No products found to delete.");
                            return;
                        }
                        else
                        {
                            db.Users.RemoveRange(users);
                        }
                        int affected = db.SaveChanges();
                    }
                    MessageBox.Show("Данные удалены");

                    QueryingTours();
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
    }
}
