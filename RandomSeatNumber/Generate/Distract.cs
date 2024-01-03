using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomSeatNumber.Generate
{
    /// <summary>
    /// 组（区）类
    /// 针对组规定每组分布情况
    /// </summary>
    class Distract
    {
        /// <summary>
        /// 行
        /// 限制，浅规定每区行数
        /// </summary>
        private int rowLimition;
        public int RowLimition { get => rowLimition; set => rowLimition = value; }

        /// <summary>
        /// 列
        /// 限制，浅规定每区每行多少列
        /// </summary>
        private int collimition;
        public int ColumnLimition { get => collimition; set => collimition = value; }

        /// <summary>
        /// 附加的座位，用于应对特殊情况
        /// </summary>
        private int whiteList;
        public int Whitelist { get => whiteList; set => whiteList = value; }

        /// <summary>
        /// 移除的座位，用于特殊情况与消除某座位的权重
        /// </summary>
        private int blackList;
        public int BlackList { get => blackList; set => blackList = value; }

        public Distract(int rowLimition, int columnLimition, int whitelist, int blackList)
        {
            RowLimition = rowLimition;
            ColumnLimition = columnLimition;
            Whitelist = whitelist;
            BlackList = blackList;
        }
    }
}
