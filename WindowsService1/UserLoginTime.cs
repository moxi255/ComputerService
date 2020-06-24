using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class UserLoginTime
    {
        public long ID { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 次数
        /// </summary>
        public int Time { get; set; }
    }
}
