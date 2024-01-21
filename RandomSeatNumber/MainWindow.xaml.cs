using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using System;
using System.Linq;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RandomSeatNumber
{
    public sealed partial class MainWindow : Window
    {
        //当前窗口
        private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        //标题栏文本
        public string AppTitleBarText {  get; set; }

        public MainWindow()
        {
            //初始化控件
            this.InitializeComponent();

            //设置为自定义标题栏
            CurrentAppWindow = GetAppWindowForCurrentWindow();
            var titleBar = CurrentAppWindow.TitleBar;
            titleBar.ExtendsContentIntoTitleBar = true;
            //this.SetTitleBar(AppTitleBar);

            //设定亚克力背景材质
            SystemBackdrop = new DesktopAcrylicBackdrop();

            //设置标题文本
            this.Title = "高一 12 班随机点名程序";

            //默认导航到 Generation 页面
            contentFrame.Navigate(typeof(Pages.Generation_Page), null, new EntranceNavigationTransitionInfo());// 导航
            ((Microsoft.UI.Xaml.Controls.NavigationViewItem)SideBar.MenuItems[0]).IsSelected = true;// 在边栏选中

        }

        //获得当前窗口
        private Microsoft.UI.Windowing.AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(wndId);
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
    }
}
