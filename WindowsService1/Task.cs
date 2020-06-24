using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class Task
    {
        public long Id { get; set; }
        /// <summary>
        /// 任务类别
        /// </summary>
        [Required(ErrorMessage = "任务类别为空")]
        public Guid? WorkTypeId { get; set; }

        public SysDict WorkType { get; set; }
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
        public long CreateUserId { get; set; }


        public virtual UserInfo CreateUser { get; set; }
        /// <summary>
        /// 网格ID
        /// </summary>
        public long GridID { get; set; }
        public virtual Gridding Grid { get; set; }
        /// <summary>
        /// 接受用户ID
        /// </summary>
        public long ReciveUserID { get; set; }

        /// <summary>
        /// 接受用户
        /// </summary>
        public virtual UserInfo ReciveUser { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Point { get; set; }

        /// <summary>
        /// 状态 0 未完成 1 完成 2 已评分
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 最后分值
        /// </summary>
        public int? EndPoint { get; set; }
        /// <summary>
        /// 反馈分值
        /// </summary>
        public DateTime? ReplyTime { get; set; }

        public string ReplyContent { get; set; }
        public string ReplyImgs { get; set; }

        public int StartNum { get; set; }


        [NotMapped]
        public string userIds { get; set; }


        /// <summary>
        /// 独一无二ID
        /// </summary>
        public Guid? UnqueId { get; set; }
    }
}
