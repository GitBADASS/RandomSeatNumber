using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RandomSeatNumber
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        //��ǰ����
        private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        public MainWindow()
        {
            //��ʼ���ؼ�
            this.InitializeComponent();
            SystemBackdrop = new DesktopAcrylicBackdrop();

            //���ñ����ı�
            this.Title = "��һ 12 �������������";

            //����Ϊ�Զ��������
            CurrentAppWindow = GetAppWindowForCurrentWindow();
            var titleBar = CurrentAppWindow.TitleBar;
            titleBar.ExtendsContentIntoTitleBar = true;

            //Ĭ�ϵ����� Generation ҳ��
            contentFrame.Navigate(typeof(Pages.Generation_Page));
        }

        //��ť����¼�
        private void generate_Click(object sender, RoutedEventArgs e)
        {
            //������λ��
            int[] seatNum = generateSeatNum();

            //��ʾ�� seatNumberText
            displayResults(seatNum);
        }

        //ģ�飺���������������λ��
        private int[] generateSeatNum()
        {
            //��������
            int[] seatNum = new int[3];

            //��������ɶ���
            Random random = new Random(Guid.NewGuid().GetHashCode());

            //���������������С������С�����
            int districtNum = random.Next(1, 3 + 1);//������Χ�� [1, 3]
            int rowNum;
            int columnNum;

            //���ڵ������ͻ������У����Ե�������
            if (districtNum == 3)
            {
                rowNum = random.Next(1, 7 + 1); //�У����������з�Χ�� [1, 7]
                //�����������е��з�Χ�� [2, 3]����������
                if (rowNum == 7)
                {
                    columnNum = random.Next(2, 3 + 1); //�У������������е��з�Χ�� [2, 3]
                }
                else
                {
                    //������ڵ����������У����������� columnNum
                    columnNum = random.Next(1, 3 + 1); //�У�ÿ�й�������
                }
            }
            //���ɵ���ֵ �� 3
            else
            {
                //���ɵ���ֵ �� 3
                rowNum = random.Next(1, 6 + 1); //�У���������
                columnNum = random.Next(2, 3 + 1); //�У�ÿ�й�������
            }

            seatNum[0] = districtNum;
            seatNum[1] = rowNum;
            seatNum[2] = columnNum;

            return seatNum;
        }

        //ģ�飺��ʾ�� seatNumberText
        private void displayResults(int[] seatNumber)
        {
            //seatNumberText.Text =
            //    Convert.ToString(seatNumber[0]) +
            //    ", " +
            //    Convert.ToString(seatNumber[1]) +
            //    ", " +
            //    Convert.ToString(seatNumber[2]);
        }

        //��õ�ǰ����
        private Microsoft.UI.Windowing.AppWindow GetAppWindowForCurrentWindow()
        {
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return Microsoft.UI.Windowing.AppWindow.GetFromWindowId(wndId);
        }

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
