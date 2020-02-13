using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using YandexDisk.Client;
using YandexDisk.Client.Clients;
using YandexDisk.Client.Http;
using YandexDisk.Client.Protocol;
using YaSheduler.UserControls;

namespace YaSheduler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string oauthToken = "";
        TokenWindow tokenWindow;
        private readonly string jsonPath;
        private readonly string jsonFullName;
        private const string secret = "c02efddce5b1465b9a1d0ed0f2790d83";
        List<Goal> goals;

        public MainWindow()
        {
            jsonPath = Directory.GetCurrentDirectory() + "/JsonData";
            jsonFullName = jsonPath + "/" + "Data.json";
            InitializeComponent();



            //Binding binding = new Binding();
            //binding.ElementName = "txtBoxDescription";
            //binding.Path = new PropertyPath("Text");
            //txtBoxDescription.SetBinding(some1.CurrentItem, binding);
            //some1.SetBinding(txtBoxDescription.Text, binding);


            goals = new List<Goal>()
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


        private JsonGoalList jsonCurrent;


        private JsonGoalList GetJson(string fullName)
        {
            if (File.Exists(fullName))
            {
                using (StreamReader reader = new StreamReader(fullName))
                {
                    string json = reader.ReadToEnd();
                    JsonSerializerSettings serSettings = new JsonSerializerSettings();
                    serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    JsonGoalList tmpGoalList = JsonConvert.DeserializeObject<JsonGoalList>(json, serSettings);
                    return tmpGoalList;
                }
                //byte[] bytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(array));
                //await File.WriteAllBytesAsync(fullName, bytes);    
            }
            return null;
        }


        private async Task UpdateJson(JsonGoalList goalList, bool islocal, bool isWeb)
        {
            goalList.UpdateDate = DateTime.Now;
            if (islocal)
            { 
                byte[] bytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(goalList));
                await File.WriteAllBytesAsync(jsonFullName, bytes);            
            }

            if (isWeb)
            {
                IDiskApi diskApi = new DiskHttpApi(oauthToken);

                //try
                //{
                    await diskApi.Files.UploadFileAsync(path: "/foo/Data.json",
                                                    overwrite: false,
                                                    localFile: jsonFullName,
                                                    cancellationToken: CancellationToken.None);
                //}
                //catch (Exception ex)
                //{
                //    //MessageBox.Show(ex.InnerException.Message);
                    
                //}
                 
            }
        }

        private JsonGoalList GetLocal()
        {
            string fullName = jsonPath + "/" + "Data.json";
            //если конфиг существует, то сравнить дату изменения с серверной версией
            if (File.Exists(fullName))
            {
                return GetJson(fullName);
                //byte[] bytes = Encoding.Default.GetBytes(JsonConvert.SerializeObject(array));
                //await File.WriteAllBytesAsync(fullName, bytes);    
            }
            return null;
        }
        private JsonGoalList GetFromYa(string token = "")
        {            
            string fullName = jsonPath + "/" + "Data2.json";
            if (String.IsNullOrEmpty(token))
            {
                string url = "https://oauth.yandex.ru/authorize?response_type=token&client_id=" + secret;
                try
                {
                    Process.Start(url);
                }
                catch (Win32Exception)
                {
                    var startInfo = new ProcessStartInfo(@"C:\Program Files\Internet Explorer\iexplore.exe", url);
                    Process.Start(startInfo);
                }
                finally
                {

                }
                tokenWindow = new TokenWindow();
                //tokenWindow.Owner = (Application.Current as App).MainWindow;
                tokenWindow.Closing += GetToken;
                tokenWindow.Show();
            }
            else
            {
                oauthToken = token;
                Task t = Task.Run(() => DownloadJson());
                //t.Wait();
                if (t.IsCompletedSuccessfully)
                {
                    MessageBox.Show("Succes");
                    return GetJson(fullName);
                }
                else
                {
                    MessageBox.Show("NotExist");
                    
                }
            }
            return null;

        }
        private void InitializeJsomFile()
        {
            if (!Directory.Exists(jsonPath))
            {                
                Directory.CreateDirectory(jsonPath);               
            }
            

            //4 ситуации
            //1. Нет данных о data.json нигде     
            //2. Данные есть только на сервере
            //3. Данные есть только в локальном каталоге (что врядли, либо если нет соединения)
            //4. Данные и там, и там (надо сравнивать) 

            JsonGoalList jsonLocal = GetLocal();
            string token = "";
            if (jsonLocal != null)
            {
                token = jsonLocal.Token;
            }
            JsonGoalList jsonYa = GetFromYa(token);

            if (jsonLocal == null && jsonYa == null)
            {
                MessageBox.Show("No data");
                jsonCurrent = new JsonGoalList() { Key = secret, UpdateDate = DateTime.Now };
                Task t = Task.Run(() => UpdateJson(jsonCurrent, true, true));
                // t.Wait();
                //t.GetAwaiter();
                if (!t.IsCompletedSuccessfully)
                {
                    MessageBox.Show("Error: " + t.Status.ToString());
                }
            }
            else if (jsonLocal != null && jsonYa == null)
            {
                MessageBox.Show("Only local");
                jsonCurrent = jsonLocal;
                Task t = Task.Run(() => UpdateJson(jsonLocal, false, true));

                //  t.Wait(10000);
                //t.ContinueWith((o) => { MessageBox.Show(o.Exception.ToString()); });
                //t.GetAwaiter().GetResult();
                if (!t.IsCompletedSuccessfully)
                {
                    MessageBox.Show("Error: " + t.Status.ToString());
                    MessageBox.Show("Error: " + t.Exception.InnerException.ToString());
                }
            }
            else if (jsonLocal == null && jsonYa != null)
            {
                jsonCurrent = jsonYa;
                MessageBox.Show("Only Ya");
                Task t = Task.Run(() => UpdateJson(jsonYa, true, false));
                if (!t.IsCompletedSuccessfully)
                {
                    MessageBox.Show("Error: " + t.Status.ToString());
                }
            }
            else 
            {
                MessageBox.Show("Both availble");
                if (jsonYa.UpdateDate < jsonLocal.UpdateDate)
                {
                    Task t = Task.Run(() => UpdateJson(jsonLocal, false, true));
                    jsonCurrent = jsonLocal;
                }
                else
                {
                    Task t = Task.Run(() => UpdateJson(jsonYa, true, false));
                    jsonCurrent = jsonYa;
                }
            }
        }

        private void mainWindow_Initialized(object sender, EventArgs e)
        {
            InitializeJsomFile();
        }

        private void GetToken(object sender, CancelEventArgs e)
        {
            oauthToken = tokenWindow.Token;
            jsonCurrent.Token = oauthToken;
           // Task.Run(() => UploadSample());
            //Task.Run(() => UpdateJson(jsonCurrent, true, true));
        }

        //async Task UploadSample()
        //{
        //    //You should have oauth token from Yandex Passport.
        //    //See https://tech.yandex.ru/oauth/            

        //    // Create a client instance
        //    IDiskApi diskApi = new DiskHttpApi(oauthToken);

        //    //Upload file from local
        //    await diskApi.Files.UploadFileAsync(path: "/foo/myfile.txt",
        //                                        overwrite: false,
        //                                        localFile: @"C:\[WORK]\myfile.txt",
        //                                        cancellationToken: CancellationToken.None);
        //}

        async Task DownloadJson()
        {
            //if (!String.IsNullOrEmpty(oauthToken))
            //{ 
                IDiskApi diskApi = new DiskHttpApi(oauthToken);
                //Upload file from local
                await diskApi.Files.DownloadFileAsync(path: "foo/data.json", localFile: jsonPath + "/" + "Data.json", cancellationToken: CancellationToken.None);           
            //}
            //else
            //{
            //}
        }

        private void mainWindow_Closing(object sender, CancelEventArgs e)
        {
            Task task = Task.Run(() => UpdateJson(jsonCurrent, true, true));
            //Task task = Task.Run(() => UpdateJson(new JsonGoalList
            //{
            //    Key = jsonCurrent.Key,
            //    Token = oauthToken,
            //    UpdateDate = DateTime.Now,
            //    Goals = goals
            //}, true, true)); ;

        }
    }
}
