﻿#pragma checksum "..\..\..\TrackWorkout.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "FF3F1C0798FF79511BC9699AD133898A7EBFE85A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using FitLevel_RPG;
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


namespace FitLevel_RPG {
    
    
    /// <summary>
    /// TrackWorkout
    /// </summary>
    public partial class TrackWorkout : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\TrackWorkout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock timerTextBlock;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\TrackWorkout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock textBlock;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\TrackWorkout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dataGrid;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\TrackWorkout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button StartTimerButton;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\TrackWorkout.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EndTimerButton;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/FitLevel-RPG;V1.0.0.2;component/trackworkout.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\TrackWorkout.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.4.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.timerTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.textBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.dataGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.StartTimerButton = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\TrackWorkout.xaml"
            this.StartTimerButton.Click += new System.Windows.RoutedEventHandler(this.BeginButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.EndTimerButton = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\TrackWorkout.xaml"
            this.EndTimerButton.Click += new System.Windows.RoutedEventHandler(this.StopButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

