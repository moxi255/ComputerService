using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class Log
    {
        /// <summary>
        /// 
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// openId
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 网格ID
        /// </summary>
        public long? GridId { get; set; }
        /// <summary>
        /// 判断是不是村民
        /// </summary>
        public bool Village { get; set; }


        /// <summary>
        /// 村民对应的ID
        /// </summary>
        public long userInfoId { get; set; }
    }
}
