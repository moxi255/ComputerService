using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class ConsultEvaluate
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 村民ID
        /// </summary>
        public long VillagerId { get; set; }


        /// <summary>
        /// 评价
        /// </summary>
        public string Memo { get; set; }
        /// <summary>
        /// 满意度 0 满意 1基本满意 2不满意
        /// </summary>
        public int SatisfactionDegree { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 问题ID
        /// </summary>
        public long? ProblemId { get; set; }


        /// <summary>
        /// 地区ID
        /// </summary>
        public long GridId { get; set; }
        /// <summary>
        /// 独一无二ID
        /// </summary>
        public Guid? UnqueId { get; set; }

        public string Imgs { get; set; }
    }
    enum Point
    {
        VeryGood = 10,
        Good = 5,
        NO = 0

    }
    

}
