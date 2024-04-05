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

        public List<int[]> DistinctSeatTable; // ��ʱ����

        Random Random = new Random(); // ��ʱ����

        private int currentGenerateTime = 1;

        private int newGenerateTime;

        private string intitialHistory;

        private string[] historyInArrays;

        private int generatedTimes;

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
            DistinctSeatTable = SeatTable;

            intitialHistory = HistoryOfGenerated.Text;
        }

        private void Generate_Btn_Click(object sender, RoutedEventArgs e)
        {
            /*
             * === �·�Ϊ��ʱ���� ===
             * ��ʱ��ȡӲ���룬������Ҫ����
             */

            // �������ַ���
            string Result = "";
            string ResultOfHistory = "";

            if(HistoryOfGenerated.Text == intitialHistory)
            {
                HistoryOfGenerated.Text = "";
            }

            if(generatedTimes > 100)
            {
                generatedTimes = 0;
                // TODO: ������ʷ��ֱ����գ����ǰ����ȵļ�������ȥ��������գ����ܵ�ʵ�ַ�������ȥͷ currentGenerateTime �����ݡ�
                // ���߻��� ListView ��ʾ������ TextBlock��
                historyInArrays = HistoryOfGenerated.Text.Split('\n');
                for (global::System.Int32 i = currentGenerateTime - 1; i < historyInArrays.Length; i++)
                {
                    ResultOfHistory += historyInArrays[i] + "\n";
                }
                HistoryOfGenerated.Text = ResultOfHistory;
            }

            // �Խ���ַ���������ز���
            // ������ɴ��������� 1��ѭ���������ɽ��
            if ((bool)!Distinct.IsChecked)
            {
                if (currentGenerateTime != 1)
                {
                    // �������ɽ���ַ���
                    for (int i = 1; i <= currentGenerateTime; i++)
                    {
                        if (i != currentGenerateTime)
                        {
                            Result += TransformToString(GetRandomSeatNumber()) + "\n";
                        }
                        else
                        {
                            Result += TransformToString(GetRandomSeatNumber());
                        }
                    }
                }
                else
                {
                    // ������ɴ���Ϊ 1��ֻ������һ��
                    Result = TransformToString(GetRandomSeatNumber());
                } 
            }
            else
            {
                if (currentGenerateTime != 1)
                {
                    // �������ɽ���ַ���
                    for (int i = 1; i <= currentGenerateTime; i++)
                    {
                        if (i != currentGenerateTime)
                        {
                            Result += TransformToString(GetDistinctRandomSeatNumber()) + "\n";
                        }
                        else
                        {
                            Result += TransformToString(GetDistinctRandomSeatNumber());
                        }
                    }
                }
                else
                {
                    // ������ɴ���Ϊ 1��ֻ������һ��
                    Result = TransformToString(GetDistinctRandomSeatNumber());
                }
            }

            HistoryOfGenerated.Text += Result + "\n";
            generatedTimes += currentGenerateTime;
            // ���ı����ǵ���ʾ����Ŀؼ�
            NumberTextBlock.Text = Result;
        }

        private int[] GetRandomSeatNumber()
        {
            // �����ȡ����
            int RandomOrder = (int)Random.NextInt64(0, SeatTable.Count);
            int[] RandomSeatNumber = SeatTable[RandomOrder];
            return RandomSeatNumber;
        }
        
        private int[] GetDistinctRandomSeatNumber()
        {
            if(DistinctSeatTable.Count <= 0)
            {
                DistinctSeatTable.AddRange(SeatTable);
            }
            // �����ȡ����
            int RandomOrder = (int)Random.NextInt64(0, DistinctSeatTable.Count);
            int[] RandomSeatNumber = DistinctSeatTable[RandomOrder];
            DistinctSeatTable.RemoveAll(e => e == RandomSeatNumber);
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

        // Ӧ���û���������ɴ���

        private void NumberOfGenerateTime_Input_LostFocus(object sender, RoutedEventArgs e)
        {
            // �����ɽ��תΪ int������ֵ�� newGenerateTime
            bool result = int.TryParse(NumberOfGenerateTime_Input.Text, out newGenerateTime);

            // ������ϡ�ȫ��Ϊ�����ַ��Ҹ����ֵ�ֵ������ 100��������
            if (result && newGenerateTime > 0 && newGenerateTime <= 100)
            {
                // Ӧ����ֵ
                currentGenerateTime = newGenerateTime;

                // ֪ͨ�û��޸ĳɹ�
                ShowNotification("�ɹ��޸�", 
                                 "���Ѿ��ɹ��޸����ɴ���Ϊ " + currentGenerateTime + " ��", 
                                 InfoBarSeverity.Success);
            }
            // �������������
            else
            {
                // newGenerateTime ����ֵΪ 0��currentGenerateTime ����Ӱ��

                // ��������Ϊ�û������˷������ַ�
                if (!result)
                {
                    ShowNotification("��Ч����", "����������ݿ��ܰ����ǰ����������ַ�", InfoBarSeverity.Warning);
                }
                // ���ͨ����������жϣ�����ж�������ֵ������Ų�
                // ��������ֵ����
                else if (newGenerateTime > 100 || newGenerateTime < 0)
                {
                    ShowNotification("������ֵ", "���������ֵ���ܲ���������ķ�Χ�ڣ������� 1~100 �ڵ�����", InfoBarSeverity.Warning);
                }
            }
        }

        private void ShowNotification(string title, string message, InfoBarSeverity severity)
        {
            if(InforBar.IsOpen)
            {
                InforBar.IsOpen = false;
            }

            InforBar.Title = title;
            InforBar.Message = message;
            InforBar.Severity = severity;

            InforBar.IsOpen = true;
        }
    }
}
