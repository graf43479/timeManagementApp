using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
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
    public partial class TaskList : INotifyPropertyChanged
    {
        public static readonly DependencyProperty CurrentItemProperty;

        public CrUWindow editWindow;
        static ListBox dragSource = null;

        static TaskList()
        {
            CurrentItemProperty = DependencyProperty.Register("CurrentItem", typeof(string), typeof(TaskList));            
        }
        public string CurrentItem
        {
            get { return (string)GetValue(CurrentItemProperty); }
            set { SetValue(CurrentItemProperty, value); }
        }

        ObservableCollection<string> tasks;        
      
        private GoalTypes goalType;

        public ObservableCollection<string> Tasks
        {
            get { return this.tasks; }
            set {
                this.tasks = value;
                OnPropertyChanged("Tasks");
                }
        }       

        public GoalTypes GoalType
        {
            get { return goalType; }
            set
            {
                goalType = value;               
                OnPropertyChanged("GoalType");
            }
        }      

        internal void OnPropertyChanged(string prop="")
        {
            if (PropertyChanged != null) 
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop)); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
              
        
        public TaskList()
        {
            InitializeComponent();
            tasks = new ObservableCollection<string>();
            lstBox.ItemsSource = Tasks; 
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

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lstBox.SelectedIndex != -1)
            {
                var item = lstBox.Items[lstBox.SelectedIndex].ToString();
                int selected = lstBox.SelectedIndex;
                lstBox.SelectedIndex = selected==0 ? 1 : selected-1;
                tasks.Remove(tasks.First(x => x == item));               
            }
        }

        private void lstBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {  
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (editWindow != null)
            {
                editWindow.Close();
            }

            if (lstBox.SelectedIndex != -1)
            {
                editWindow = new CrUWindow("Обновить", lstBox.Items[lstBox.SelectedIndex].ToString());                
            }
            else
            {
                editWindow = new CrUWindow("Обновить");
            }
            
            editWindow.Closing += UpdateList;
            editWindow.Owner = (Application.Current as App).MainWindow;
            editWindow.Show();
        }

        private void lstBox_Drop(object sender, DragEventArgs e)
        {
            DragDropData data = e.Data.GetData(typeof(DragDropData)) as DragDropData;
            if (!tasks.Any(x => x == data.ActualData.ToString()))
            { 
                Tasks.Add(data.ToString());
            }
            var a =  data.DragStartSource;
            if (a != tasks)
            { 
                a.Remove(data.ToString());
            }            
        }

        private void lstBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListBox parent = (ListBox)sender;
                dragSource = parent;
                object data = GetDataFromListBox(dragSource, e.GetPosition(parent));            
                if (data != null)
                {
                    var dragDropData = new DragDropData
                    {
                        ActualData = data,
                        DragStartSource = tasks
                    };

                    DragDrop.DoDragDrop(parent, dragDropData, DragDropEffects.Move);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static object GetDataFromListBox(ListBox source, Point point)
        {
            try
            {
                UIElement element = source.InputHitTest(point) as UIElement;
                if (element != null)
                {
                    object data = DependencyProperty.UnsetValue;
                    while (data == DependencyProperty.UnsetValue)
                    {
                        data = source.ItemContainerGenerator.ItemFromContainer(element);

                        if (data == DependencyProperty.UnsetValue)
                        {
                            element = VisualTreeHelper.GetParent(element) as UIElement;
                        }

                        if (element == source)
                        {
                            return null;
                        }
                    }

                    if (data != DependencyProperty.UnsetValue)
                    {
                        return data;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public void UpdateList(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string newTxt = editWindow.txtBoxDescription.Text;
            if (!String.IsNullOrEmpty(newTxt))
            {
                if (!tasks.Any(x => x == newTxt))
                {
                    if (!String.IsNullOrEmpty(editWindow.BaseDescription))
                    {
                        Tasks.Remove(editWindow.BaseDescription);
                        Tasks.Add(newTxt);
                    }
                    else
                    {
                        Tasks.Add(newTxt);
                    }
                }
                else
                {
                    if(newTxt!=editWindow.BaseDescription)
                    MessageBox.Show("Запись уже добавлена!");
                }
            }
        }
    }

    public class DragDropData
    {
        public object ActualData { get; set; }
        public ObservableCollection<string> DragStartSource { get; set; }

        public override string ToString()
        {
            return ActualData.ToString();
        }
    }
}


