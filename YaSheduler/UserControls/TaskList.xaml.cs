using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YaSheduler.UserControls
{
    /// <summary>
    /// Логика взаимодействия для TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl //, INotifyPropertyChanged
    {
        //public static DependencyObject ItemsSourceProperty;
        ObservableCollection<string> tasks;



        public ObservableCollection<string> Tasks
        {
            get { return this.tasks; }
            set {
                this.tasks = value;
                //RaisePropertyChanged("Tasks");
                }
        }

        // Using a DependencyProperty as the backing store for Tasks.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TasksProperty =
        //    DependencyProperty.Register("Tasks", typeof(ObservableCollection<string>), typeof(TaskList), new PropertyMetadata(
        //        new ObservableCollection<string>()));

        //internal void RaisePropertyChanged(string prop)
        //{
        //    if (PropertyChanged != null) { PropertyChanged(this, new PropertyChangedEventArgs(prop)); }
        //}


        //public event PropertyChangedEventHandler PropertyChanged;

        static TaskList()
        {
            
        }
        //public IEnumerable ItemsSource { get; set; }
        public TaskList()
        {
            InitializeComponent();
            tasks = new ObservableCollection<string>() {
                "1",
                "2",
                "3"
            };
            lstBox.ItemsSource = tasks;           
            
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selected = lstBox.SelectedIndex;
                if (selected >= 0)
                {
                    var itemToMoveUp = tasks[selected];
                    tasks.RemoveAt(selected);
                    tasks.Insert(selected - 1, itemToMoveUp);
                    lstBox.SelectedIndex = selected - 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            

        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int selected = lstBox.SelectedIndex;
                if (selected >= 0 & selected < lstBox.Items.Count - 1)
                {
                    var itemToMoveDoewn = tasks[selected];
                    tasks.RemoveAt(selected);
                    tasks.Insert(selected + 1, itemToMoveDoewn);
                    lstBox.SelectedIndex = selected + 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            tasks.Add("4");
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var item = lstBox.Items[lstBox.SelectedIndex].ToString();
            tasks.Remove(item);
        }

        public string GetItem()
        {
            int selected = lstBox.SelectedIndex;
            var itemToMoveDoewn = lstBox.Items[selected];

            return tasks[selected]; // lstBox.Items[lstBox.SelectedIndex].ToString() ;
        }

        private void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        //public List<string> GetTasks()
        //{
        //    return lstBox.Items as List<string>;
        //}
    }
}


