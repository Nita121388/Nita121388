﻿#pragma checksum "..\..\..\..\UI\VideoPlayControl.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3E2AAE332517CE9AE34CA455840AC7146A56E920"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using NitaVision.UI.Source.Convert;
using NitaVision.UI.UI;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Vlc.DotNet.Wpf;


namespace NitaVision.UI.UI {
    
    
    /// <summary>
    /// VideoPlayControl
    /// </summary>
    public partial class VideoPlayControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Vlc.DotNet.Wpf.VlcControl VlcControl;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnFastRewind;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock StartTextBlock;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider VideoSlider;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock EndTextBlock;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnFastForward;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnPer;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PlayGrid;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnPlay;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnPause;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\UI\VideoPlayControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnNext;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NitaVision.UI;component/ui/videoplaycontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UI\VideoPlayControl.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.7.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.VlcControl = ((Vlc.DotNet.Wpf.VlcControl)(target));
            return;
            case 2:
            this.BtnFastRewind = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.BtnFastRewind.Click += new System.Windows.RoutedEventHandler(this.FastRewindClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.StartTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.VideoSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 40 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.VideoSlider.AddHandler(System.Windows.Controls.Primitives.Thumb.DragStartedEvent, new System.Windows.Controls.Primitives.DragStartedEventHandler(this.SliderDragStarted));
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.VideoSlider.AddHandler(System.Windows.Controls.Primitives.Thumb.DragDeltaEvent, new System.Windows.Controls.Primitives.DragDeltaEventHandler(this.SliderDragDelta));
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.VideoSlider.AddHandler(System.Windows.Controls.Primitives.Thumb.DragCompletedEvent, new System.Windows.Controls.Primitives.DragCompletedEventHandler(this.SliderDragCompleted));
            
            #line default
            #line hidden
            
            #line 43 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.VideoSlider.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.VideoSliderMouseDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.EndTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.BtnFastForward = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.BtnFastForward.Click += new System.Windows.RoutedEventHandler(this.FastForwardClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.BtnPer = ((System.Windows.Controls.Button)(target));
            
            #line 59 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.BtnPer.Click += new System.Windows.RoutedEventHandler(this.BtnPer_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PlayGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.BtnPlay = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.BtnPlay.Click += new System.Windows.RoutedEventHandler(this.BtnPlay_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.BtnPause = ((System.Windows.Controls.Button)(target));
            
            #line 67 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.BtnPause.Click += new System.Windows.RoutedEventHandler(this.BtnPause_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.BtnNext = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\..\..\UI\VideoPlayControl.xaml"
            this.BtnNext.Click += new System.Windows.RoutedEventHandler(this.BtnNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
