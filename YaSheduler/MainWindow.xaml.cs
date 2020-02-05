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

namespace YaSheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Binding binding = new Binding();
            //binding.ElementName = "txtBoxDescription";
            //binding.Path = new PropertyPath("Text");
            //txtBoxDescription.SetBinding(some1.CurrentItem, binding);
            //some1.SetBinding(txtBoxDescription.Text, binding);
            
                
            List<Goal> goals = new List<Goal>()
            {
                new Goal{ GoalID = 1, Description = "Some description1", GoalType = GoalTypes.NotImportantNotUrgent},
                new Goal{ GoalID = 2, Description = "Some description2", GoalType = GoalTypes.NotImportantUrgent},
                new Goal{ GoalID = 3, Description = "Some description3", GoalType = GoalTypes.NotImportantNotUrgent},
                new Goal{ GoalID = 4, Description = "Some description4", GoalType = GoalTypes.ImportantNotUrgent},
                new Goal{ GoalID = 5, Description = "Some description5", GoalType = GoalTypes.ImportantNotUrgent},
                new Goal{ GoalID = 6, Description = "Some description6", GoalType = GoalTypes.NotImportantUrgent},
                new Goal{ GoalID = 7, Description = "Some description7", GoalType = GoalTypes.ImportantUrgent},
                new Goal{ GoalID = 8, Description = "Some description8", GoalType = GoalTypes.ImportantUrgent},
                new Goal{ GoalID = 9, Description = "Some description9", GoalType = GoalTypes.NotImportantNotUrgent},
                new Goal{ GoalID = 10, Description = "Some description10", GoalType = GoalTypes.NotImportantUrgent},
                new Goal{ GoalID = 11, Description = "Some description11", GoalType = GoalTypes.NotImportantNotUrgent},
                new Goal{ GoalID = 12, Description = "Some description12", GoalType = GoalTypes.ImportantNotUrgent},
                new Goal{ GoalID = 13, Description = "Some description13", GoalType = GoalTypes.ImportantUrgent},
                new Goal{ GoalID = 14, Description = "Some description14", GoalType = GoalTypes.ImportantUrgent}                
            };

            try
            {
                foreach (var item in goals)
                {
                    switch (item.GoalType)
                    {
                        case GoalTypes.ImportantUrgent:
                            some1.Tasks.Add(item.Description);
                            some1.GoalType = GoalTypes.ImportantUrgent;
                            break;
                        case GoalTypes.ImportantNotUrgent:
                            some2.Tasks.Add(item.Description);
                            some2.GoalType = GoalTypes.ImportantNotUrgent;
                            break;
                        case GoalTypes.NotImportantUrgent:
                            some3.Tasks.Add(item.Description);
                            some1.GoalType = GoalTypes.NotImportantUrgent;
                            break;
                        case GoalTypes.NotImportantNotUrgent:
                            some4.Tasks.Add(item.Description);
                            some1.GoalType = GoalTypes.NotImportantNotUrgent;
                            break;
                        default:
                            break;
                    }
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
