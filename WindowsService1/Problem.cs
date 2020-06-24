using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class Problem
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public Guid? TypeId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Required(ErrorMessage = "内容不能为空")]
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "内容不符合大于3个字小于1000字要求")]
        public string Content { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Imgs { get; set; }


        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 缩放比列
        /// </summary>
        public string Zoom { get; set; }
        /// <summary>
        /// 发生地点
        /// </summary>
        public string HappenPosition { get; set; }

        /// <summary>
        /// 0待受理 1 以下发 2处理中 3 已完成  4 已评价
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 村民ID
        /// </summary>
        public long VillagerId { get; set; }
        /// <summary>
        /// 传达给层级
        /// </summary>
        public long? CurrentUserId { get; set; }


        /// <summary>
        /// 当前用户
        /// </summary>
        public UserInfo CurrentUser { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 网格ID
        /// </summary>
        public long? GridId { get; set; }

        public Gridding Grid { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 是否转发 0  不转发 1 转发 2 直接处理
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// 当前网格ID
        /// </summary>
        public long? CurrentGridId { get; set; }
        /// <summary>
        /// 当前区域ID
        /// </summary>
        public Guid? CurrentAreaId { get; set; }
        /// <summary>
        /// 当前区域
        /// </summary>
        public SysDict CurrentArea { get; set; }
        /// <summary>
        /// 反馈内容
        /// </summary>
        public string ReplyContent { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OperaterTime { get; set; }
        public bool HaveEvaluate { get; set; }

        public ConsultEvaluate consultEvaluate { get; set; }

        /// <summary>
        /// 录音文件
        /// </summary>
        public string VioceFile { get; set; }

        /// <summary>
        /// 判断是否匿名 true 匿名 false 不是匿名
        /// </summary>
        public bool Anonymous { get; set; }
        /// <summary>
        /// 村民姓名
        /// </summary>
        public string VillagerName { get; set; }

        /// <summary>
        /// 网格长
        /// </summary>
        public string SubmitUser { get; set; }
        /// <summary>
        /// 当前处理人
        /// </summary>
        public string OperateUser { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 回复图片
        /// </summary>
        public String ReplyImg { get; set; }

        /// <summary>
        /// 当前报送位置或网格
        /// </summary>
        [NotMapped]
        public string CurrentAreaName { get; set; }
        /// <summary>
        /// 红颜色
        /// </summary>
        [NotMapped]
        public bool RedColor { get; set; }

        [NotMapped]
        public string Number { get; set; }

        public bool Delete { get; set; }
        /// <summary>
        /// 评价字符串
        /// </summary>
        [NotMapped]
        public string ConsultEvaluateStr { get; set; }


        [NotMapped]
        public string CurentStr { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [NotMapped]
        public string Memo { get; set; }
    }
}
