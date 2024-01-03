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
        /// <summary>
        /// 内容属性 包含策略中的所有组
        /// </summary>
        internal List<Distract> Content { get; set; }

        /// <summary>
        /// 接受一个 `Distract` 集合
        /// </summary>
        /// <param name="content">传入一个集合，内容为所有组</param>
        public Strategy(List<Distract> content)
        {
            Content = content;
        }
    }
}
