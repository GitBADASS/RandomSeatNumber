using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using RandomSeatNumber.Generate;
using System;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace RandomSeatNumber.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Generation_Page : Page
    {
        private string generatedNumber;

        public List<int[]> SeatTable; // ��ʱ����

        Random Random = new Random(); // ��ʱ����
        public string GeneratedNumber
        {
            get
            {
                return generatedNumber;
            }

            set
            {
                generatedNumber = value;

                if(NumberTextBlock != null) 
                {
                    NumberTextBlock.Text = generatedNumber;
                }
            }
        }

        public Generation_Page()
        {
            GeneratedNumber = "���������ɡ���ť";
            this.InitializeComponent();

            /*
             * === �·�Ϊ��ʱ���� ===
             * ��ʱ��ȡӲ���룬������Ҫ����
             */

            // ��һ��
            // ɾ�����ſ���һ�ֿ���ֱ��ɾ�����Ż����еķ���
            List<int[]> BlackList_1_2 = new List<int[]>
            {
                new int[] { 7, 1 },
                new int[] { 7, 2 },
                new int[] { 7, 3 },
            };

            List<int[]> BlackList_3 = new List<int[]>
            {
                new int[] { 7, 1 },
                new int[] { 7, 2 }
            };

            InitialDistract distract1 = new InitialDistract(
                1, 7, 3, null, BlackList_1_2);

            // �ڶ���
            InitialDistract distract2 = new InitialDistract(
                2, 6, 3, null, BlackList_1_2);

            // ������
            InitialDistract distract3 = new InitialDistract(
                3, 6, 3, null, BlackList_3);

            // ���ɳ�ʼ���α�
            SeatTable = Generator.CreateSeatTableByDistracts(distract1, distract2, distract3);

        }

        private void Generate_Btn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * === �·�Ϊ��ʱ���� ===
             * ��ʱ��ȡӲ���룬������Ҫ����
             */

            // �����ȡ����
            int[] RandomSeatNumber = GetRandomSeatNumber();

            // תΪ�ַ���
            GeneratedNumber = TransformToString(RandomSeatNumber);
        }

        private void MultiplyGenerate_Click(object sender, RoutedEventArgs e)
        {
            string Result = "";
            MultipleNumber.Text = Result;

            for (int i = 1; i <= 5; i++)
            {
                if(i != 5)
                {
                    Result += TransformToString(GetRandomSeatNumber()) + " �� ";
                }
                else
                {
                    Result += TransformToString(GetRandomSeatNumber());
                }
            }

            MultipleNumber.Text = Result;
        }

        private int[] GetRandomSeatNumber()
        {
            // �����ȡ����
            int RandomOrder = (int)Random.NextInt64(0, SeatTable.Count);
            int[] RandomSeatNumber = SeatTable[RandomOrder];
            return RandomSeatNumber;
        }

        private string TransformToString(int[] RandomSeatNumber)
        {
            // תΪ�ַ���
            string DistrictNumber = RandomSeatNumber[0].ToString();
            string RowNumber = RandomSeatNumber[1].ToString();
            string ColumnNumber = RandomSeatNumber[2].ToString();

            return DistrictNumber + ", " + RowNumber + ", " + ColumnNumber;
        }

        private void MultiplyGenerate_TextChanged(object sender, TextChangedEventArgs e)
        {
            // ������ȳ��� 5���� infobar ������Ӧ��ʾ����������
        }
    }
}
