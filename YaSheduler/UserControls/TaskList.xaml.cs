using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
    public partial class TaskList : UserControl, INotifyPropertyChanged
    {
        //public static DependencyObject ItemsSourceProperty;
        ObservableCollection<string> tasks;        

        private string currentItem;

        private string newItem;

        public ObservableCollection<string> Tasks
        {
            get { return this.tasks; }
            set {
                this.tasks = value;
                OnPropertyChanged("Tasks");
                }
        }

        public string CurrentItem
        {
            get { return currentItem; }
            set
            {
                this.currentItem = value;
                OnPropertyChanged("CurrentItem");
            }
        }

        public string NewItem
        {
            get { return newItem; }
            set
            {
                this.newItem = value;
                OnPropertyChanged("NewItem");
            }
        }

        // Using a DependencyProperty as the backing store for Tasks.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TasksProperty =
        //    DependencyProperty.Register("Tasks", typeof(ObservableCollection<string>), typeof(TaskList), new PropertyMetadata(
        //        new ObservableCollection<string>()));

        //public static readonly DependencyProperty CurrentItemProperty =
        //    DependencyProperty.Register("CurrentItem", typeof(string), typeof(string), new PropertyMetadata(
        //        string.Empty
        //        ));

        //public static readonly DependencyProperty NewItemProperty =
        //    DependencyProperty.Register("NewItem", typeof(string), typeof(string), new PropertyMetadata(
        //        string.Empty));

        internal void OnPropertyChanged(string prop="")
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop)); 
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        static TaskList()
        {            
        }
        
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
            if (!tasks.Any(x => x == currentItem))
            {
                tasks.Add(currentItem);
            }
            else
            {
                MessageBox.Show("Запись уже добавлена!");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstBox.SelectedIndex != -1)
            {
                var item = lstBox.Items[lstBox.SelectedIndex].ToString();
                int selected = lstBox.SelectedIndex;
                lstBox.SelectedIndex = selected==0 ? 1 : selected-1;
                tasks.Remove(item);
            }
        }

        public string GetItem()
        {
            int selected = lstBox.SelectedIndex;
            var itemToMoveDoewn = lstBox.Items[selected];
            return tasks[selected]; 
        }

        private void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}


