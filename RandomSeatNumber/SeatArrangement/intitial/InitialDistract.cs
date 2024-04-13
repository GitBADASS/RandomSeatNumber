
using System.Collections.Generic;

namespace RandomSeatNumber.Generate
{
    /// <summary>
    /// 组（区）类
    /// 针对组规定每组分布情况
    /// </summary>
    class InitialDistract
    {
        /// <summary>
        /// 组序
        /// 规定该组的序号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 行
        /// 限制，浅规定每区行数
        /// </summary>
        public int RowLimition { get; set; }

        /// <summary>
        /// 列
        /// 限制，浅规定每区每行多少列
        /// </summary>
        public int ColumnLimition { get; set; }

        /// <summary>
        /// 附加的座位，用于应对特殊情况
        /// </summary>
        public List<int[]> WhiteList { get; set; }

        /// <summary>
        /// 移除的座位，用于特殊情况与消除某座位的权重
        /// </summary+
        public List<int[]> BlackList { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rowLimition">行数限制</param>
        /// <param name="columnLimition">列数限制</param>
        /// <param name="whitelist">白名单</param>
        /// <param name="blackList">黑名单</param>
        public InitialDistract(int ID, int rowLimition, int columnLimition, List<int[]> whitelist, List<int[]> blackList)
        {
            #region Assignment
            RowLimition = rowLimition;
            ColumnLimition = columnLimition;     
            WhiteList = whitelist;
            BlackList = blackList; 
            this.ID = ID; 
            #endregion
        }
    }
}
