using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    /// <summary>
    /// 网格指数数据表
    /// </summary>
    public partial class GridValue
    {
        public long Id { get; set; }
        /// <summary>
        /// 主键
        /// </summary>
        public string HealthKey { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public string HealthValue { get; set; }
        /// <summary>
        /// 村民数量
        /// </summary>
       public int AllNumber { get; set; }
        /// <summary>
        /// 打卡率
        /// </summary>
        public decimal PunchRate { get; set; }
        /// <summary>
        /// 村民问题上报率
        /// </summary>
        public decimal problemRate { get; set; }
        /// <summary>
        /// 问题率
        /// </summary>
        public decimal OkRate { get; set; }
        /// <summary>
        /// 网格数
        /// </summary>
        public int GridNumber { get; set; }
        /// <summary>
        /// 完成数量
        /// </summary>
        public int FinishProblem { get; set; }
        /// <summary>
        /// 所有数量
        /// </summary>
        public int AllProblem { get; set; }
        /// <summary>
        /// 问题处理率
        /// </summary>
        public decimal problemDealRate { get; set; }
        /// <summary>
        /// 任务完成数
        /// </summary>
        public int finishTaskCount { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
