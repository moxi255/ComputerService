using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class WorkHistory
    {

        public long Id { get; set; }
        /// <summary>
        /// 工作类别
        /// </summary>
        [Required(ErrorMessage = "工作类别为空")]
        public Guid? WorkTypeId { get; set; }

        public virtual SysDict WorkType { get; set; }
        /// <summary>
        /// 1 工作 2 会议记录
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "内容不符合大于3个字小于1000字要求")]
        public string Content { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "标题不符合大于3个字小于100字要求")]
        public string Title { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Imgs { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserId { get; set; }


        public virtual UserInfo User { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public long? Point { get; set; }
        /// <summary>
        /// 状态 0 未打分 1打分
        /// </summary>
        public int? Status { get; set; }

        public int StartNum { get; set; }
        /// <summary>
        /// 独一无二ID
        /// </summary>
        public Guid? UnqueId { get; set; }
        /// <summary>
        /// 网格ID
        /// </summary>
        public long GridId { get; set; }


        public Gridding Grid { get; set; }
        /// <summary>
        /// 状态字符串
        /// </summary>
        [NotMapped]
        public string statusString{get;set;}
    }
}
