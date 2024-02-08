using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFTest.Enity;

namespace WPFTest.NitaCustomControl
{
    //PART_Delete
    [TemplatePart(Name = "PART_Delete", Type = typeof(NitaButton))]
    public class NitaTag : ContentControl
    {

        #region Constructors

        static NitaTag()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaTag),
                new FrameworkPropertyMetadata(typeof(NitaTag)));
        }
        #endregion

        #region Fileds
        private NitaButton _deleteButton;
        #endregion

        #region 依赖属性

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaTag), new PropertyMetadata(SizeType.Medium));
        #endregion

        #region Icon
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", 
                typeof(ImageSource), 
                typeof(NitaTag), 
                new PropertyMetadata((ImageSource)Application.Current.Resources["Delete"]));
        #endregion

        #region Permission
        public List<Permission> Permissions
        {
            get { return (List<Permission>)GetValue(PermissionsProperty); }
            set { SetValue(PermissionsProperty, value); }
        }

        public static readonly DependencyProperty PermissionsProperty =
            DependencyProperty.Register("Permissions", typeof(List<Permission>), typeof(NitaTag), new PropertyMetadata());
        #endregion

        #region 

        #endregion

        #endregion

        #region Override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _deleteButton = GetTemplateChild("PART_Delete") as NitaButton;
            if (_deleteButton == null)
            {
                throw new Exception("嘿！ PART_Delete从模板中丢失，或者不是NitaButton。抱歉，但是需要此Grid。");
            }
            _deleteButton.Click += _deleteButton_Click;
        }

        private void _deleteButton_Click(object sender, RoutedEventArgs e)
        {
           this.Visibility = Visibility.Collapsed;
        }
        #endregion

    }
}
