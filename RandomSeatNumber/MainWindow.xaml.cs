using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using Windows.ApplicationModel;
using WinRT.Interop;

namespace RandomSeatNumber
{
    public sealed partial class MainWindow : Window
    {
        // 当前窗口
        //private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        // 标题栏文本
        public string AppTitleBarText {  get; set; }

        // 当前对象
        public static MainWindow current;

        public MainWindow()
        {
            // 初始化控件
            this.InitializeComponent();

            // 初始化 current 防止其值为空
            current = this;

            // 处理自定义标题栏
            ProcessTheCustomTitleBar();

            // 设定亚克力背景材质
            SystemBackdrop = new DesktopAcrylicBackdrop();

            // 默认导航到 Generation 页面
            contentFrame.Navigate(typeof(Pages.Generation_Page), null, new EntranceNavigationTransitionInfo());// 导航
            ((NavigationViewItem)SideBar.MenuItems[0]).IsSelected = true;// 在边栏选中导航项

        }

        // 导航项更改事件，导航页面用
        private void NavigationView_SelectionChanged(Microsoft.UI.Xaml.Controls.NavigationView sender, Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = (NavigationViewItem)args.SelectedItem;
            switch ((String)SelectedItem.Tag)
            {
                case "Generation_Page":
                    contentFrame.Navigate(typeof(Pages.Generation_Page));
                    break;
                case "Information_Page":
                    contentFrame.Navigate(typeof(Pages.Information_Page));
                    break;
            }
        }

        private void ProcessTheCustomTitleBar()
        {
            // 设置为自定义标题栏
            ExtendsContentIntoTitleBar = true;

            // 设置标题栏文本
            AppTitleBarText = AppInfo.Current.DisplayInfo.DisplayName;

            // 设置标题栏按钮颜色
            
        }
    }
}
