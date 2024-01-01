using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Services.Store;

namespace RandomSeatNumber.Generate
{
    /// <summary>
    /// 策略类
    /// 策略类是一个封装类，记录用户设置的座位分布状况，Generator 会根据它生成配置信息并保存为文件。
    /// </summary>
    class Strategy
    {
        public Strategy(string DistractLimition, string RowLimition, string ColumnLimition)
        {
            this.RowLimition = RowLimition;
            this.ColumnLimition = ColumnLimition;
            this.DistractLimition = DistractLimition;
        }

        /// <summary>
        /// 区限制，浅规定配置信息中的区数
        /// </summary>
        private string distractlimition;
        public string DistractLimition { get => distractlimition; set => distractlimition = value; }

        /// <summary>
        /// 行限制，浅规定每区行数
        /// </summary>
        private string rowLimition;
        public string RowLimition { get => rowLimition; set => rowLimition = value; }

        /// <summary>
        /// 列限制，浅规定每区每行多少列
        /// </summary>
        private string collimition;
        public string ColumnLimition { get => collimition; set => collimition = value; }
        
        /// <summary>
        /// 附加的座位，用于应对特殊情况
        /// </summary>
        private string whiteList;
        public string Whitelist { get => whiteList; set => whiteList = value; }

        /// <summary>
        /// 移除的座位，用于特殊情况与消除某座位的权重
        /// </summary>
        private string blackList;
        public string BlackList { get => blackList; set => blackList = value; }
    }
}
