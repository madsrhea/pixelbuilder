﻿#pragma checksum "..\..\ArtViewWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "481BC8D3DA6589DB207613D3F807FEE365678DE75E2FCA0F3FF7100508246432"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PresentationLayer;
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


namespace PresentationLayer {
    
    
    /// <summary>
    /// ArtViewWindow
    /// </summary>
    public partial class ArtViewWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image imgArt;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblArt;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBookmark;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFollow;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDelete;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lstboxColors;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock txtboxDescription;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNext;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrev;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ArtViewWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditDesc;
        
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
            System.Uri resourceLocater = new System.Uri("/PresentationLayer;component/artviewwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ArtViewWindow.xaml"
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
            this.imgArt = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.lblArt = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.btnBookmark = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.btnFollow = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\ArtViewWindow.xaml"
            this.btnFollow.MouseEnter += new System.Windows.Input.MouseEventHandler(this.btnFollow_MouseEnter);
            
            #line default
            #line hidden
            
            #line 21 "..\..\ArtViewWindow.xaml"
            this.btnFollow.MouseLeave += new System.Windows.Input.MouseEventHandler(this.btnFollow_MouseLeave);
            
            #line default
            #line hidden
            
            #line 21 "..\..\ArtViewWindow.xaml"
            this.btnFollow.Click += new System.Windows.RoutedEventHandler(this.btnFollow_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnDelete = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\ArtViewWindow.xaml"
            this.btnDelete.Click += new System.Windows.RoutedEventHandler(this.btnDelete_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.lstboxColors = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.txtboxDescription = ((System.Windows.Controls.TextBlock)(target));
            
            #line 28 "..\..\ArtViewWindow.xaml"
            this.txtboxDescription.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtboxDescription_KeyDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnNext = ((System.Windows.Controls.Button)(target));
            return;
            case 9:
            this.btnPrev = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            this.btnEditDesc = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\ArtViewWindow.xaml"
            this.btnEditDesc.Click += new System.Windows.RoutedEventHandler(this.btnEditDesc_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
