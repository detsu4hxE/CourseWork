﻿#pragma checksum "..\..\..\Windows\AdminWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FDD6F0C4FDB46A3F9546F26EEBDB3A7BF71A0372A62026463D8F85F7F9578D06"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWork.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace CourseWork.Windows {
    
    
    /// <summary>
    /// AdminWindow
    /// </summary>
    public partial class AdminWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Windows\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse profilePicture;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Windows\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse iconPicture;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\Windows\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TBlockUsername;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\Windows\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame FrameMain;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Windows\AdminWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnBack;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CourseWork;component/windows/adminwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Windows\AdminWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.profilePicture = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 22 "..\..\..\Windows\AdminWindow.xaml"
            this.profilePicture.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Ellipse_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 25 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.profileBtn_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 26 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.historyBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 27 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.exitBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.iconPicture = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 34 "..\..\..\Windows\AdminWindow.xaml"
            this.iconPicture.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Icon_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TBlockUsername = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.FrameMain = ((System.Windows.Controls.Frame)(target));
            
            #line 41 "..\..\..\Windows\AdminWindow.xaml"
            this.FrameMain.ContentRendered += new System.EventHandler(this.FrameMain_ContentRendered);
            
            #line default
            #line hidden
            return;
            case 8:
            this.BtnBack = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Windows\AdminWindow.xaml"
            this.BtnBack.Click += new System.Windows.RoutedEventHandler(this.BtnBack_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 44 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnRoles_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 45 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnUsers_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 46 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnAnswers_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 47 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnSubjects_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 48 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnTasks_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 49 "..\..\..\Windows\AdminWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnTests_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

