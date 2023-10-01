using NitaVision.UI.Source.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NitaVision.UI.UI.StudyList
{
    /// <summary>
    /// Folder.xaml 的交互逻辑
    /// </summary>
    public partial class FolderItem : UserControl
    {
        public FolderItem()
        {
            InitializeComponent();
        }
        public static  DependencyProperty FolderNameProperty =
          DependencyProperty.Register("FolderName", typeof(string), typeof(FolderItem), new PropertyMetadata(""));

        public static  DependencyProperty FolderIsReadOnlyProperty =
          DependencyProperty.Register("FolderIsReadOnly", typeof(bool), typeof(FolderItem), new PropertyMetadata(true)); 
        
        public static DependencyProperty FolderIsSelectedProperty =
          DependencyProperty.Register("FolderIsSelected", typeof(bool), typeof(FolderItem), new PropertyMetadata(false));
        public string FolderName
        {
            get { return (string)GetValue(FolderNameProperty); }
            set { 
                SetValue(FolderNameProperty, value);
            }
        }
        public bool FolderIsReadOnly
        {
            get { return (bool)GetValue(FolderIsReadOnlyProperty); }
            set { 
                SetValue(FolderIsReadOnlyProperty, value);
            }
        }
        public bool FolderIsSelected
        {
            get { return (bool)GetValue(FolderIsSelectedProperty); }
            set
            {
                SetValue(FolderIsSelectedProperty, value);
            }
        }
        /*MouseLeftButtonDown="ItemsControl_MouseLeftButtonDown"
                MouseLeftButtonUp="ItemsControl_MouseLeftButtonUp"
                MouseMove="ItemsControl_MouseMove"*/
        public event MouseButtonEventHandler DragMouseLeftButtonDown
        {
            add { DragIcon.MouseLeftButtonDown += value; }
            remove { DragIcon.MouseLeftButtonDown -= value; }
        }
        public event MouseButtonEventHandler DragMouseLeftButtonUp
        {
            add { DragIcon.MouseLeftButtonUp += value; }
            remove { DragIcon.MouseLeftButtonUp -= value; }
        }
        public event MouseEventHandler DragMouseMove
        {
            add { DragIcon.MouseMove += value; }
            remove { DragIcon.MouseMove -= value; }
        }
        public event MouseButtonEventHandler DragMouseButtonEventHandler
        {
            add { DragIcon.MouseLeftButtonDown += value; }
            remove { DragIcon.MouseLeftButtonDown -= value; }
        }
        public event MouseButtonEventHandler TextBoxMouseDoubleClick
        {
            add { folderTextBox.MouseDoubleClick += value; }
            remove { folderTextBox.MouseDoubleClick -= value; }
        }
        public event KeyEventHandler TextBoxKeyDown
        {
            add { folderTextBox.KeyDown += value; }
            remove { folderTextBox.KeyDown -= value; }
        }
        private void Grid_LostFocus(object sender, RoutedEventArgs e)
        {
            // Your code here
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                if (e.Key == Key.Enter)
                {
                    ThicknessAnimation ta = new ThicknessAnimation();
                    ta.From = new Thickness(5, 5, 5, 5);
                    ta.To = new Thickness(0, 0, 0, 0);
                    ta.Duration = TimeSpan.FromSeconds(0.2);
                    textBox.BeginAnimation(TextBox.BorderThicknessProperty, ta);
                }
            }
        }
    }
}
