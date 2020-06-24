using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class TodaySatisfaction
    {
        /// <summary>
        /// id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        public decimal Rate { get; set; }


        public long GridId { get; set; }
    }
}
