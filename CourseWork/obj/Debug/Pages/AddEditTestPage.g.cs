﻿#pragma checksum "..\..\..\Pages\AddEditTestPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "72E1E9C63D541C588A3763489D127FFB0049B1FCB5A0A6F79C6B5A0F5AD8A679"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using CourseWork.Pages;
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


namespace CourseWork.Pages {
    
    
    /// <summary>
    /// AddEditTestPage
    /// </summary>
    public partial class AddEditTestPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\Pages\AddEditTestPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox taskBox;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Pages\AddEditTestPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBoxInput;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Pages\AddEditTestPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBoxOutput;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\AddEditTestPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSave;
        
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
            System.Uri resourceLocater = new System.Uri("/CourseWork;component/pages/addedittestpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\AddEditTestPage.xaml"
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
            this.taskBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 2:
            this.TBoxInput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TBoxOutput = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.BtnSave = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Pages\AddEditTestPage.xaml"
            this.BtnSave.Click += new System.Windows.RoutedEventHandler(this.BtnSave_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

