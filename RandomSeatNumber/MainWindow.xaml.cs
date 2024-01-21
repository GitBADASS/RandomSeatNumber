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
        //��ǰ����
        private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        //�������ı�
        public string AppTitleBarText {  get; set; }

        public MainWindow()
        {
            //��ʼ���ؼ�
            this.InitializeComponent();

            //����Ϊ�Զ��������
            CurrentAppWindow = GetAppWindowForCurrentWindow();
            var titleBar = CurrentAppWindow.TitleBar;
            titleBar.ExtendsContentIntoTitleBar = true;
            //this.SetTitleBar(AppTitleBar);

            //�趨�ǿ�����������
            SystemBackdrop = new DesktopAcrylicBackdrop();

            //���ñ����ı�
            this.Title = "��һ 12 �������������";

            //Ĭ�ϵ����� Generation ҳ��
            contentFrame.Navigate(typeof(Pages.Generation_Page), null, new EntranceNavigationTransitionInfo());// ����
            ((Microsoft.UI.Xaml.Controls.NavigationViewItem)SideBar.MenuItems[0]).IsSelected = true;// �ڱ���ѡ��

        }

        //��õ�ǰ����
        private Microsoft.UI.Windowing.AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(wndId);
        }

        // ����������¼�������ҳ����
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
