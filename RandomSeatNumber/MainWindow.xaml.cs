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
        // ��ǰ����
        //private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        // �������ı�
        public string AppTitleBarText {  get; set; }

        public MainWindow()
        {
            // ��ʼ���ؼ�
            this.InitializeComponent();

            // �����Զ��������
            ProcessTheCustomTitleBar();

            // �趨�ǿ�����������
            SystemBackdrop = new DesktopAcrylicBackdrop();

            // Ĭ�ϵ����� Generation ҳ��
            contentFrame.Navigate(typeof(Pages.Generation_Page), null, new EntranceNavigationTransitionInfo());// ����ҳ��
            ((NavigationViewItem)SideBar.MenuItems[0]).IsSelected = true;// �ڱ���ѡ�е�����

            // ��ʼ�� current ��ֹ��ֵΪ��
            MainWindowHelpers.current = this;
        }

        // ����������¼�������ҳ����
        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var SelectedItem = (NavigationViewItem)args.SelectedItem;
            switch ((String)SelectedItem.Tag)
            {
                // ����ҳ
                case "Generation_Page":
                    contentFrame.Navigate(typeof(Pages.Generation_Page));
                    break;
                // ��Ϣҳ
                case "Information_Page":
                    contentFrame.Navigate(typeof(Pages.Information_Page));
                    break;
            }
        }

        private void ProcessTheCustomTitleBar()
        {
            // ����Ϊ�Զ��������
            ExtendsContentIntoTitleBar = true;

            // ���ñ������ı�
            AppTitleBarText = AppInfo.Current.DisplayInfo.DisplayName;

            // ���ñ�������ť��ɫ
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
