﻿#pragma checksum "..\..\..\..\PasswordReset\ResetPassword.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "A524842BCE333117161306B05C200C950E5517A5"
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
using System.Windows.Forms.Integration;
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
    /// ResetPassword
    /// </summary>
    public partial class ResetPassword : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\..\PasswordReset\ResetPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label PageName;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\PasswordReset\ResetPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelEmail;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\..\PasswordReset\ResetPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextboxEmail;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\PasswordReset\ResetPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonResePassword;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\..\PasswordReset\ResetPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockResetInfo;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\PasswordReset\ResetPassword.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextblockError;
        
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
            System.Uri resourceLocater = new System.Uri("/FitLevel-RPG;V1.0.0.2;component/passwordreset/resetpassword.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\PasswordReset\ResetPassword.xaml"
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
            this.PageName = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.LabelEmail = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.TextboxEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ButtonResePassword = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\PasswordReset\ResetPassword.xaml"
            this.ButtonResePassword.Click += new System.Windows.RoutedEventHandler(this.ResetButtonClick);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TextblockResetInfo = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.TextblockError = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

