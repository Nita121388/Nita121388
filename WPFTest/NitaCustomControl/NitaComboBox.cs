using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFTest.DataBinding;
using WPFTest.Enity;
using WPFTest.ViewModel;

namespace WPFTest.NitaCustomControl
{

    [TemplatePart(Name = "PART_ContentSite", Type = typeof(ContentPresenter))]
    public class NitaComboBox : ComboBox
    {
        static NitaComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaComboBox),
                new FrameworkPropertyMetadata(typeof(NitaComboBox)));
        }
        #region 依赖属性

        #region SizeType
        public SizeType SizeType
        {
            get { return (SizeType)GetValue(SizeTypeProperty); }
            set { SetValue(SizeTypeProperty, value); }
        }

        public static readonly DependencyProperty SizeTypeProperty =
            DependencyProperty.Register("SizeType", typeof(SizeType), typeof(NitaComboBox), new PropertyMetadata(SizeType.Medium));
        #endregion

        #region NitaItemModels

        public NitaItemModels NitaItemModels
        {
            get { return (NitaItemModels)GetValue(NitaItemModelsProperty); }
            set { SetValue(NitaItemModelsProperty, value); }
        }

        public static readonly DependencyProperty NitaItemModelsProperty =
            DependencyProperty.Register(
                "NitaItemModels",
                typeof(NitaItemModels),
                typeof(NitaComboBox),
                new PropertyMetadata());

        #endregion

        #region SelectMode
        public SelectionMode SelectionMode
        {
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            set { SetValue(SelectionModeProperty, value); }
        }

        public static readonly DependencyProperty SelectionModeProperty =
            DependencyProperty.Register("SelectModeProperty",
                typeof(SelectionMode),
                typeof(NitaComboBox),
                new PropertyMetadata(SelectionMode.Single));
        #endregion

        #endregion

        private ContentPresenter _selectedNitaItem;

        #region override
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _selectedNitaItem = GetTemplateChild("PART_ContentSite") as ContentPresenter;
            if (ItemsSource != null)
            {
                this.SelectedValue = ItemsSource.OfType<NitaItemModel>().FirstOrDefault();   
            }
        }
        #endregion
    }
}
