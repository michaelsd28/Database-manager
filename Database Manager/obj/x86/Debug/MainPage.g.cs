﻿#pragma checksum "C:\Users\rd28\Desktop\saveit\Database Manager\Database Manager\MainPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E0C9107FA61C5942BADDE5C5179A2504850F897C227FAC72A7AAB0DA43788A1F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database_Manager
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 75
                {
                    this.Popup_Create = (global::Windows.UI.Xaml.Controls.Primitives.Popup)(target);
                }
                break;
            case 3: // MainPage.xaml line 90
                {
                    this.Popup_ConnectURI = (global::Windows.UI.Xaml.Controls.Primitives.Popup)(target);
                }
                break;
            case 4: // MainPage.xaml line 55
                {
                    this.BCreateDB = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BCreateDB).Click += this.BCreateDB_Click;
                }
                break;
            case 5: // MainPage.xaml line 59
                {
                    this.BConnectDB = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BConnectDB).Click += this.BConnectDB_Click;
                }
                break;
            case 6: // MainPage.xaml line 63
                {
                    this.TSearchDB = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7: // MainPage.xaml line 65
                {
                    this.BRefreshDB = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.BRefreshDB).Click += this.BRefreshDB_Click;
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.19041.685")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

