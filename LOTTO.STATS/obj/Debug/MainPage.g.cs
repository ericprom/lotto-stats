﻿

#pragma checksum "C:\Users\EricProm\Documents\Visual Studio 2013\Projects\LOTTO.STATS\LOTTO.STATS\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E395E1BC0907CAE134377B0DA3BC6ACF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LOTTO.STATS
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 37 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.timeList_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 68 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Hub)(target)).SectionsInViewChanged += this.HubControl_SectionsInViewChanged;
                 #line default
                 #line hidden
                #line 69 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.FrameworkElement)(target)).Loaded += this.HubControl_Loaded;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


