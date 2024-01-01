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
        //当前窗口
        private Microsoft.UI.Windowing.AppWindow CurrentAppWindow;

        public MainWindow()
        {
            //初始化控件
            this.InitializeComponent();
            SystemBackdrop = new DesktopAcrylicBackdrop();

            //设置标题文本
            this.Title = "高一 12 班随机点名程序";

            //设置为自定义标题栏
            CurrentAppWindow = GetAppWindowForCurrentWindow();
            var titleBar = CurrentAppWindow.TitleBar;
            titleBar.ExtendsContentIntoTitleBar = true;

            //默认导航到 Generation 页面
            contentFrame.Navigate(typeof(Pages.Generation_Page));
        }

        //按钮点击事件
        private void generate_Click(object sender, RoutedEventArgs e)
        {
            //生成座位号
            int[] seatNum = generateSeatNum();

            //显示到 seatNumberText
            displayResults(seatNum);
        }

        //模块：以数组生成随机座位号
        private int[] generateSeatNum()
        {
            //座号数组
            int[] seatNum = new int[3];

            //随机数生成对象
            Random random = new Random(Guid.NewGuid().GetHashCode());

            //声明“区”、“行”、“列”数字
            int districtNum = random.Next(1, 3 + 1);//区，范围在 [1, 3]
            int rowNum;
            int columnNum;

            //若在第三区就会有七行，所以单独讨论
            if (districtNum == 3)
            {
                rowNum = random.Next(1, 7 + 1); //行，第三区的行范围在 [1, 7]
                //第三区第七行的行范围在 [2, 3]，单独讨论
                if (rowNum == 7)
                {
                    columnNum = random.Next(2, 3 + 1); //列，第三区第七行的行范围在 [2, 3]
                }
                else
                {
                    //如果不在第三区第七行，则正常生成 columnNum
                    columnNum = random.Next(1, 3 + 1); //列，每行共有三列
                }
            }
            //生成的区值 ≠ 3
            else
            {
                //生成的区值 ≠ 3
                rowNum = random.Next(1, 6 + 1); //行，共有六行
                columnNum = random.Next(2, 3 + 1); //列，每行共有三列
            }

            seatNum[0] = districtNum;
            seatNum[1] = rowNum;
            seatNum[2] = columnNum;

            return seatNum;
        }

        //模块：显示到 seatNumberText
        private void displayResults(int[] seatNumber)
        {
            //seatNumberText.Text =
            //    Convert.ToString(seatNumber[0]) +
            //    ", " +
            //    Convert.ToString(seatNumber[1]) +
            //    ", " +
            //    Convert.ToString(seatNumber[2]);
        }

        //获得当前窗口
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
