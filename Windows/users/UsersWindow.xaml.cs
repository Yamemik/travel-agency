using System.Windows;
using System.Windows.Controls;

namespace TravelAgency.Windows.users
{
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();

            QueryingUsers();

        }

        private void QueryingUsers()
        {
            using (TravelDBContext db = new())
            {
                IQueryable<User>? users = db.Users;

                DGridUsers.ItemsSource = users?.ToList<User>();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            AddEditUsersWindow addEditPage = new AddEditUsersWindow(((Button)sender).DataContext as User);
            addEditPage.Show();
            this.Close();
        }
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEditUsersWindow addEditPage = new AddEditUsersWindow(null!);
            addEditPage.Show();
            this.Close();
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

                    QueryingUsers();
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
