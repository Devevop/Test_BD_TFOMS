using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Логика взаимодействия для Found.xaml
    /// </summary>
    public partial class Found : Page
    {
        List<PEOPLE> pElem=new List<PEOPLE>();
        List<LPU> lpuElem=new List<LPU>();
        List<T001> t1Elem=new List<T001>();
        List<T007> t7Elem=new List<T007>();
        List<HISTLPU> hElem = new List<HISTLPU>();
        List<dynamic> resLst=new List<dynamic>();
        public Found()
        {
            InitializeComponent();
            LoadElem();
            LoadFromListView();
        }

        //загружаем таблицы в список для создания запросов
        public void LoadElem()
        {
            foreach(var item in DB.context.PEOPLE.ToList())
            {
                pElem.Add(item);
            }
            foreach(var item in DB.context.LPU.ToList())
            {
                lpuElem.Add(item);
            }
            foreach(var item in DB.context.T001.ToList())
            {
                t1Elem.Add(item);
            }
            foreach(var item in DB.context.T007.ToList())
            {
                t7Elem.Add(item);
            }
            foreach(var item in DB.context.HISTLPU.ToList())
            {
                hElem.Add(item);
            }
        }

        //создаем метод загрузки в listView
        //некорректно заполнена таблица HISTLPU. Перепутаны столбцы district и subdiv
        public void LoadFromListView()
        {
            resLst.Clear();
            var query = from pE in pElem
                        join t7E in t7Elem on pE?.lpuuch equals t7E.depth into t7
                        from t7E in t7.DefaultIfEmpty()
                        join t1E in t1Elem on t7E?.nom_podr equals t1E.nom_podr into t1
                        from t1E in t1.DefaultIfEmpty()
                        join hE in hElem on pE.p_id equals hE.p_id into h
                        from hE in h.DefaultIfEmpty()
                        join lE in lpuElem on hE?.lpu equals lE.code into l
                        from lE in l.DefaultIfEmpty()
                        where (pE?.lpu == t7E?.code_mo || pE?.lpu=="" || pE.lpuuch=="" || pE.lpudx!=null) && t1E?.mcod==t7E?.code_mo 
                        select new { 
                            fam = pE.fam, im = pE.im, ot = pE.ot, dr = pE.dr,
                            prk = (pE.lpudx == null && pE.lpu != "") ? ((pE.w == 1) ? "прикреплён" : "прикреплена") : ((pE.w == 1) ? "не прикреплён" : "не прикреплена"), z0= (pE.lpudx == null && pE.lpu != "")? " к ":"",
                            caption = (pE.lpudx == null && pE.lpu != "")? lE?.caption:"", z1= (pE.lpudx == null && pE.lpu != "" && pE.lpuuch!="")? ", ":"",
                            nam_mo = (pE.lpudx == null && pE.lpu != "")? t1E?.nam_me:"", 
                            name_depth = (pE.lpudx == null && pE.lpu != "")? t7E?.name_depth:"", lpu=pE.lpu}; 
            foreach (var item in query.Distinct())
            {
                resLst.Add(item);
            }
            listFound.ItemsSource = resLst.ToList();
        }
        //поиск по дате
        private void DateFilter(object sender, SelectionChangedEventArgs e)
        {
            var query = resLst.Where((q => (q.dr.ToString().Contains(findDr.SelectedDate.Value.ToShortDateString())))).ToList();
            if (query.Any())
            {
                listFound.ItemsSource = query;
                proverka.Text = "";
            }
            else
            {
                proverka.Text = "*Дата рождения указана неверно. Попробуйте ещё";
                LoadFromListView();
            }
        }
        //поиск по ФИО/ПОЛИСУ
        private void Find(object sender, TextChangedEventArgs e)
        {
            if (FindText.Text == "")
            {
                FindVisible.Visibility = Visibility.Visible;
                LoadFromListView();
            }
            else
            {
                FindVisible.Visibility = Visibility.Hidden;
                var query= resLst.Where(q => (q.fam.Trim()+" "+q.im.Trim()+" "+q.ot.Trim()).ToLower().Trim().Contains(FindText.Text.ToLower()) || q.lpu.ToLower().Contains(FindText.Text.ToLower())).ToList();
                if (query.Any())
                {
                    listFound.ItemsSource= query;
                    proverka.Text = "";
                }
                else
                {
                    proverka.Text = "*ФИО/ПОЛИС указаны неверно. Попробуйте ещё раз";
                    LoadFromListView();
                }
            }
        }
    }
}
