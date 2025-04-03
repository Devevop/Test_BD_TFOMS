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
    /// Логика взаимодействия для EditPeopleTable.xaml
    /// </summary>
    public partial class EditPeopleTable : Page
    {
        public PEOPLE people { get; set; }
        public DateTime dateDr;
        List<T007> t007 = new List<T007>();
        bool selectDate, selectDatex, selectDatet;
        public EditPeopleTable(PEOPLE pE)
        {
            InitializeComponent();
            foreach (var item in DB.context.T007.ToList())
            {
                t007.Add(item);
            }
            people = pE;
            this.DataContext = this;
        }
        private void Enp(object sender, TextCompositionEventArgs e)
        {
            int value;
            if (!Int32.TryParse(e.Text, out value))
            {
                e.Handled = true;
            }
        }
        private void Space(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }

        private void EditPeopleItem(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите отредактировать данную запись?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    HISTLPU hISTLPU = new HISTLPU();
                    people.ot = ot.Text;
                    people.lpu = lpu.Text;
                    if (!selectDatex)
                    {
                        people.lpudx = null;
                    }
                    else people.lpudx = lpudx.SelectedDate.Value;
                    if (!selectDatet)
                    {
                        people.lpudt = null;
                    }
                    else people.lpudt = lpudt.SelectedDate.Value;
                    people.lpuuch = lpuuch.Text;
                    hISTLPU.p_id = people.p_id;
                    hISTLPU.lpu = people.lpu;
                    hISTLPU.lpudt = people.lpudt;
                    hISTLPU.lpudx = people.lpudx;
                    hISTLPU.district = people.lpuuch;
                    var r = from t7 in t007
                            where t7.depth == hISTLPU.district && hISTLPU.lpu == t7.code_mo
                            select new { otvet = t7.nom_podr };
                    string prikol = "";
                    foreach (var item in r.ToList())
                    {
                        hISTLPU.subdir = item.otvet;
                    }
                    DB.context.HISTLPU.Add(hISTLPU);
                    DB.context.SaveChanges();
                    this.NavigationService.Navigate(new Pages.EditPeople());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка. Проверьте все ли поля заполнены и соответствуют типам");
            }
        }

        private void DateDrSelectedDate(object sender, SelectionChangedEventArgs e)
        {
            selectDate = true;
        }

        private void DateLputSelectedDate(object sender, SelectionChangedEventArgs e)
        {
            selectDatet = true;
        }

        private void DateLpuxSelectedDate(object sender, SelectionChangedEventArgs e)
        {
            selectDatex = true;
        }
    }
}
