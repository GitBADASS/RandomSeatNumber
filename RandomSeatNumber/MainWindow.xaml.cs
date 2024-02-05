using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using Windows.ApplicationModel;

namespace RandomSeatNumber
{
    public sealed partial class MainWindow : Window
    {
        // 当前窗口
        //private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        // 标题栏文本
        public string AppTitleBarText {  get; set; }

        public MainWindow()
        {
            // 初始化控件
            this.InitializeComponent();

            // 处理自定义标题栏
            ProcessTheCustomTitleBar();

            // 设定亚克力背景材质
            SystemBackdrop = new DesktopAcrylicBackdrop();

            // 默认导航到 Generation 页面
            contentFrame.Navigate(typeof(Pages.Generation_Page), null, new EntranceNavigationTransitionInfo());// 导航页面
            ((NavigationViewItem)SideBar.MenuItems[0]).IsSelected = true;// 在边栏选中导航项

            // 初始化 current 防止其值为空
            MainWindowHelpers.current = this;
        }

        // 导航项更改事件，导航页面用
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = (NavigationViewItem)args.SelectedItem;
            switch ((String)SelectedItem.Tag)
            {
                // 生成页
                case "Generation_Page":
                    contentFrame.Navigate(typeof(Pages.Generation_Page));
                    break;
                // 信息页
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
            switch (Application.Current.RequestedTheme)
            {
                case ApplicationTheme.Light:
                    this.AppWindow.TitleBar.ButtonForegroundColor = Colors.Black;
                    break;
                case ApplicationTheme.Dark:
                    this.AppWindow.TitleBar.ButtonForegroundColor = Colors.White;
                    break;
                default:
                    break;
            }
        }
    }
}
