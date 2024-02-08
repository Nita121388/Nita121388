using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WPFTest.NitaCustomControl
{
    [TemplatePart(Name = "PART_Grid", Type = typeof(Grid))]
    [TemplatePart(Name = "PART_DownEllipse", Type = typeof(EllipseGeometry))]
    [TemplatePart(Name = "PART_DownPath", Type = typeof(Path))]
    public class NitaClickCanvas : ContentControl
    {
        #region 字段
        private Grid _Grid;
        private EllipseGeometry _EllipseGeometry;
        private Path _DownPath;
        #endregion
        static NitaClickCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NitaClickCanvas), new FrameworkPropertyMetadata(typeof(NitaClickCanvas)));
        }
        #region 依赖属性

        #region ClickColor
        public Brush ClickColor
        {
            get { return (Brush)GetValue(ClickColorProperty); }
            set { SetValue(ClickColorProperty, value); }
        }

        public static readonly DependencyProperty ClickColorProperty =
            DependencyProperty.Register("ClickColor", typeof(Brush), typeof(NitaClickCanvas), new PropertyMetadata((Brush)Application.Current.Resources["DefaultClickColor"]));
        #endregion

        #region ActiveClickStyleCommand
        public RelayCommand ActiveClickStyleCommand
        {
            get { return (RelayCommand)GetValue(ActiveClickStyleCommandProperty); }
            set { SetValue(ActiveClickStyleCommandProperty, value); }
        }

        public static readonly DependencyProperty ActiveClickStyleCommandProperty =
            DependencyProperty.Register("ActiveClickStyleCommand", typeof(RelayCommand), typeof(NitaClickCanvas), new PropertyMetadata());
        #endregion

        #region animationDuration
        public double AnimationDuration
        {
            get { return (double)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(double), typeof(NitaClickCanvas), new PropertyMetadata(1.3));
        #endregion

        #endregion


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _Grid = GetTemplateChild("PART_Grid") as Grid;
            if (_Grid == null)
            {
                throw new Exception("嘿！ PART_Grid从模板中丢失，或者不是Grid。抱歉，但是需要此Grid。");
            }
            _EllipseGeometry = GetTemplateChild("PART_DownEllipse") as EllipseGeometry;
            if (_EllipseGeometry == null)
            {
                throw new Exception("嘿！ PART_DownEllipse 从模板中丢失，或者不是EllipseGeometry。抱歉，但是需要此EllipseGeometry。");
            }
            _DownPath = GetTemplateChild("PART_DownPath") as Path;
            if (_DownPath == null)
            {
                throw new Exception("嘿！ PART__DownPath 从模板中丢失，或者不是Path。抱歉，但是需要此Path。");
            }
            this.ActiveClickStyleCommand = new RelayCommand(ActiveClickStyle);
            //_Grid.MouseLeftButtonDown += ActiveClickStyle;
        }



        public void ActiveClickStyle()
        {
            var center = Mouse.GetPosition(this);;
            var target = this._EllipseGeometry;
            if (target == null) return;
            target.Center = center;
            var animation = new DoubleAnimation()
            {
                From = 0,
                To = 150,
                Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
            };
            target.BeginAnimation(EllipseGeometry.RadiusXProperty, animation);
            var animation2 = new DoubleAnimation()
            {
                From = 0.3,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
            };
            var target2 = this._DownPath;
            if (target2 == null) return;

            target2.BeginAnimation(Path.OpacityProperty, animation2);
        }

        public void EnableActiveStyle()
        {
            var target = this._EllipseGeometry;
            if (target == null) return;
            target.Center = new Point(_Grid.ActualWidth / 2, _Grid.ActualHeight / 2); ;
            var animation = new DoubleAnimation()
            {
                From = 0,
                To = 150,
                Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
            };
            target.BeginAnimation(EllipseGeometry.RadiusXProperty, animation);
            var animation2 = new DoubleAnimation()
            {
                From = 0.3,
                To = 0,
                Duration = new Duration(TimeSpan.FromSeconds(AnimationDuration))
            };
            var target2 = this._DownPath;
            if (target2 == null) return;

            target2.BeginAnimation(Path.OpacityProperty, animation2);
        }
    }
}
