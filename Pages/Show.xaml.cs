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
    /// Логика взаимодействия для Show.xaml
    /// </summary>
    public partial class Show : Page
    {
        //подгружаем таблицы в виде списка для DataGrid
        public Show()
        {
            InitializeComponent();
            cmbType.SelectedIndex = 0;
            dbPeople.ItemsSource = DB.context.PEOPLE.ToList();
            dbHISTLPU.ItemsSource=DB.context.HISTLPU.ToList();
            dbLPU.ItemsSource=DB.context.LPU.ToList();
            dbt1.ItemsSource=DB.context.T001.ToList();
            dbt7.ItemsSource = DB.context.T007.ToList();
        }

        private void Type(object sender, SelectionChangedEventArgs e)
        {
            switch (cmbType.SelectedIndex)
            {
                case 0:
                    pShow.Opacity = 1;
                    hShow.Opacity = 0;
                    lShow.Opacity = 0;
                    t1Show.Opacity = 0;
                    t7Show.Opacity = 0;
                    break;
                case 1:
                    hShow.Opacity = 1;
                    pShow.Opacity = 0;
                    lShow.Opacity = 0;
                    t1Show.Opacity = 0;
                    t7Show.Opacity = 0;
                    break;
                case 2:
                    lShow.Opacity = 1;
                    pShow.Opacity = 0;
                    hShow.Opacity = 0;
                    t1Show.Opacity = 0;
                    t7Show.Opacity = 0;
                    break;
                case 3:
                    t1Show.Opacity = 1;
                    lShow.Opacity = 0;
                    pShow.Opacity = 0;
                    hShow.Opacity = 0;
                    t7Show.Opacity = 0;
                    break;
                case 4:
                    t7Show.Opacity = 1;
                    t1Show.Opacity = 0;
                    lShow.Opacity = 0;
                    pShow.Opacity = 0;
                    hShow.Opacity = 0;
                    break;
                default:
                    break;
            }
        }

        private void DeletePrk(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите удалить запись?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var select = (HISTLPU)dbHISTLPU.SelectedItem;
                    DB.context.HISTLPU.Remove(select);
                    DB.context.SaveChanges();
                    this.NavigationService.Navigate(new Pages.Show());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Попробуйте ещё");
            }
        }
    }
}
