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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TFOMS_testv1.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditPeople.xaml
    /// </summary>
    public partial class EditPeople : Page
    {
        public EditPeople()
        {
            InitializeComponent();
            dbPeople.ItemsSource = DB.context.PEOPLE.ToList();
        }


        private void AddNavigate(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Pages.AddPeople());
        }

        private void DeletePeople(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var select=(PEOPLE)dbPeople.SelectedItem;
                    DB.context.PEOPLE.Remove(select);
                    DB.context.SaveChanges();
                    this.NavigationService.Navigate(new Pages.EditPeople());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Попробуйте удалить запись в просмотре таблиц в HISTLPU");
            }
        }

        private void EditItem(object sender, RoutedEventArgs e)
        {
            var select = (PEOPLE)dbPeople.SelectedItem;
            this.NavigationService.Navigate(new Pages.EditPeopleTable(select));
        }
    }
}
