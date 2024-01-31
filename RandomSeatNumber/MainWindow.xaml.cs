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
        // ��ǰ����
        //private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        // �������ı�
        public string AppTitleBarText {  get; set; }

        // ��ǰ����
        public static MainWindow current;

        public MainWindow()
        {
            // ��ʼ���ؼ�
            this.InitializeComponent();

            // ��ʼ�� current ��ֹ��ֵΪ��
            current = this;

            // �����Զ��������
            ProcessTheCustomTitleBar();

            // �趨�ǿ�����������
            SystemBackdrop = new DesktopAcrylicBackdrop();

            // Ĭ�ϵ����� Generation ҳ��
            contentFrame.Navigate(typeof(Pages.Generation_Page), null, new EntranceNavigationTransitionInfo());// ����
            ((NavigationViewItem)SideBar.MenuItems[0]).IsSelected = true;// �ڱ���ѡ�е�����

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

        private void ProcessTheCustomTitleBar()
        {
            // ����Ϊ�Զ��������
            ExtendsContentIntoTitleBar = true;

            // ���ñ������ı�
            AppTitleBarText = AppInfo.Current.DisplayInfo.DisplayName;

            // ���ñ�������ť��ɫ
            
        }
    }
}
