using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService1
{
    public class UserData
    {
        public DateTime dateTime { get; set; }
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
    }
}
