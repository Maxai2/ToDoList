using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Xml;
using System.Xml.Linq;
//------------------------------------------------
namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Task> Tasks { get; set; }

        SolidColorBrush alertCol;
        SolidColorBrush standCol;

        SaveFileDialog saveFileDialog;
        OpenFileDialog openFileDialog;


        //------------------------------------------------    
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            Tasks = new ObservableCollection<Task>();

            //   dpDeadline.DisplayDateStart = DateTime.Now.Date;
            dpDeadline.SelectedDate = DateTime.Now.Date.AddDays(10);

            alertCol = new SolidColorBrush(Colors.Red);
            standCol = new SolidColorBrush(Colors.Black);

            saveFileDialog = new SaveFileDialog();
            openFileDialog = new OpenFileDialog();

            saveFileDialog.Filter = "Xml files (.xml)|*.xml";
            openFileDialog.Filter = "Xml files (.xml)|*.xml";


        }
        //------------------------------------------------
        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            tbName.BorderBrush = standCol;
        }
        //------------------------------------------------
        void SaveToXml()
        {
            if (saveFileDialog.ShowDialog() == true)
            {
                XDocument xdocWrite = new XDocument(new XElement("Tasks")); //new XDeclaration("1.0", "utf-8", "yes")

                foreach (var item in Tasks)
                {
                    xdocWrite.Root.Add(new XElement("task", new XElement("name", item.Name),     // new XAttribute("Id", item.Id)
                                                        new XElement("priority", item.Priority),
                                                        new XElement("deadline", item.Deadline),
                                                        new XElement("comment", item.Comment)));
                }

                xdocWrite.Save(saveFileDialog.FileName);
            }
        }
        //------------------------------------------------
        private void MenuNew_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.Count != 0)
            {
                SaveToXml();
                Tasks.Clear();

                lbLastTaskName.Content = "";
                lbTaskCount.Content = "";
            }
        }
        //------------------------------------------------
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.Count != 0)
                SaveToXml();

            //    XmlTextWriter writer = new XmlTextWriter(saveFileDialog.FileName, Encoding.UTF8);

            //    writer.WriteStartElement("Tasks");

            //    foreach (var item in Tasks)
            //    {
            //        writer.WriteStartElement("task");

            //        writer.WriteAttributeString("name", item.Name);
            //        writer.WriteAttributeString("priority", item.Priority);
            //        writer.WriteAttributeString("deadline", item.Deadline);
            //        writer.WriteAttributeString("comment", item.Comment);

            //        writer.WriteEndElement();
            //    }

            //    writer.WriteEndElement();
            //    writer.Close();
        }
        //------------------------------------------------
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            if (Tasks.Count != 0)
            {
                SaveToXml(); 
                Tasks.Clear();

                lbLastTaskName.Content = "";
                lbTaskCount.Content = "";
            }

            if (openFileDialog.ShowDialog() == true)
            {
                XDocument xdocRead = XDocument.Load(openFileDialog.FileName);

                foreach (XElement item in xdocRead.Root.Elements())
                {
                    Tasks.Add(new Task
                    {
                        Name = item.Element("name").Value,
                        Priority = item.Element("priority").Value,
                        Deadline = item.Element("deadline").Value,
                        Comment = item.Element("comment").Value
                    });
                }

                lbTaskCount.Content = Tasks.Count;
            }
        }
        //------------------------------------------------
        private void lbTasks_KeyDown(object sender, KeyEventArgs e)
        {
            int num = lbTasks.SelectedIndex;

            if (e.Key == Key.Enter)
            {
                string info;

                info = "Name:\t\t" + Tasks[num].Name + '\n';
                info += "Priority:\t\t" + Tasks[num].Priority + '\n';
                info += "Deadline:\t" + Tasks[num].Deadline + '\n';
                info += "Comment:\t" + Tasks[num].Comment + '\n';

                MessageBox.Show(info);
            }
        }
        //------------------------------------------------
        private void bAdd_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "")
            {
                tbName.BorderBrush = alertCol;
                MessageBox.Show("Fill Name", "Warning!", MessageBoxButton.OK);
                return;
            }

            Tasks.Add(new Task
            {
                Name = tbName.Text,
                Priority = cbPriority.Text,
                Deadline = dpDeadline.Text,
                Comment = tbComment.Text
            });

            lbLastTaskName.Content = tbName.Text;
            lbTaskCount.Content = Tasks.Count;

            tbName.Text = "";
            tbComment.Text = "";

            tbName.Focus();
        }
        //------------------------------------------------
        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var ans = MessageBox.Show("Are you sure you want to delete?", "", MessageBoxButton.YesNo);

            switch (ans)
            {
                case MessageBoxResult.None:
                case MessageBoxResult.No:
                    break;
                case MessageBoxResult.Yes:
                    Tasks.RemoveAt(lbTasks.SelectedIndex);
                    break;
            }
        }
        //------------------------------------------------
        private void tbEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lbTasks.SelectedItem != null)
            {
                int num = lbTasks.SelectedIndex;

                if (tbEdit.IsChecked == true)
                {
                    tbName.Text = Tasks[num].Name;
                    cbPriority.Text = Tasks[num].Priority;
                    dpDeadline.Text = Tasks[num].Deadline;
                    tbComment.Text = Tasks[num].Comment;
                }
                else
                {
                    Tasks[num].Name = tbName.Text;
                    Tasks[num].Priority = cbPriority.Text;
                    Tasks[num].Deadline = dpDeadline.Text;
                    Tasks[num].Comment = tbComment.Text;

                    lbTasks.Items.Refresh();

                    tbName.Text = "";
                    tbComment.Text = "";
                }

                tbName.Focus();
            }
            else
                tbEdit.IsChecked = false;
        }
    }
}
//------------------------------------------------