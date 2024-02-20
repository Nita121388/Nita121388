using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFTest.Enity;
using WPFTest.ViewModel;

namespace WPFTest.NitaCustomControl
{
    public class NitaDataGrid : DataGrid
    {

        #region Constructors

        static NitaDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaDataGrid), new FrameworkPropertyMetadata(typeof(NitaDataGrid)));
        }
        #endregion

        #region DependencyProperty

        #region ShowRowNumber
        public static readonly DependencyProperty ShowRowNumberProperty = DependencyProperty.Register("ShowRowNumber", typeof(bool), typeof(NitaDataGrid), new PropertyMetadata(true));

        public bool ShowRowNumber
        {
            get { return (bool)GetValue(ShowRowNumberProperty); }
            set { SetValue(ShowRowNumberProperty, value); }
        }
        #endregion




        #endregion

        #region override

        #region OnLoadingRow
        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            base.OnLoadingRow(e);
            if (ShowRowNumber)
            {
                e.Row.Header = e.Row.GetIndex() + 1;
            }
        }
        #endregion

        #region OnAutoGeneratingColumn
        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            base.OnAutoGeneratingColumn(e);

            // 获取当前列对应的数据模型属性
            var propertyDescriptor = e.PropertyDescriptor as PropertyDescriptor;
            if (propertyDescriptor == null) return;

            // 获取当前列对应的数据模型类型
            var propertyType = propertyDescriptor.PropertyType;
            if (!typeof(NitaDataMode).IsAssignableFrom(propertyType)) return;

            string propertyName = propertyDescriptor.Name;
            NitaDataMode nitaDataMode = propertyDescriptor.GetValue(e.) as NitaDataMode;
            switch (propertyName)
            {
               
            }




            // 获取当前列对应的数据模型实例
            var dataMode = (NitaDataMode)e.Row.Item;
            if (dataMode == null) return;

            // 根据NitaDataType类型为表格中添加不同的列
            switch (dataMode.NitaDataType)
            {
                case NitaDataType.Num:
                    // 添加Num类型的列
                    break;
                case NitaDataType.Text:
                    // 添加Text类型的列
                    break;
                case NitaDataType.ComboBox:
                    // 添加ComboBox类型的列
                    break;
                case NitaDataType.Action:
                    // 添加Action类型的列
                    break;
                case NitaDataType.Control:
                    // 添加Control类型的列
                    break;
                default:
                    break;
            }
        }
        #endregion






        #endregion


    }
}
