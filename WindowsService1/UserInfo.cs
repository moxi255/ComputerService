using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WGZL.Models
{
    public partial class UserInfo
    {
        public long Id { get; set; }
       
        public string Code { get; set; }
        public string PassWord { get; set; }
        [Required(ErrorMessage = "姓名名不能为空")]
        public string Name { get; set; }
        public int? Sex { get; set; }
        public string Tel { get; set; }
        public string OpenId { get; set; }
        public string Salt { get; set; }
        /// <summary>
        /// 类型 0 用户 1 管理员  
        /// </summary>
        public int? UserType { get; set; }
        public string UserImg { get; set; }
        public bool? IsDelete { get; set; }
        public string Idcard { get; set; }
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "简介过长")]
        public string Memo { get; set; }
        public DateTime? CreateTime { get; set; }
        
        /// <summary>
        /// 管理网格ID
        /// </summary>
        public long? GridId { get; set; }
        /// <summary>
        /// 管理网格
        /// </summary>
        public Gridding Grid { get; set; }

        /// <summary>
        /// 用户状态 0 禁止 1 启用
        /// </summary>
        public int Status { get; set; }
       


        /// <summary>
        /// 用户角色
        /// </summary>
        public Guid? userRoleId { get; set; }
   

        /// <summary>
        /// 学  历
        /// </summary>
        public Guid? EducationId { get; set; }


        /// <summary>
        ///政治面貌
        /// </summary>
        public Guid? PoliticalId { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// 户主
        /// </summary>
        public bool? Head { get; set; }

        /// <summary>
        /// 村民Id
        /// </summary>
        public long? VillagerId { get; set; }


        public string WXNickName { get; set; }


        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid? AreaId { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [NotMapped]
        public string AreaName { get; set; }


        public long Point { get; set; }
       
        public int Age { get; set; }
        /// <summary>
        /// 判断是否今天打卡标志
        /// </summary>
        [NotMapped]
        public bool Punch { get; set; }
        /// <summary>
        /// 积分榜后台映射标记
        /// </summary>
        [NotMapped]
        public string PointRate { get; set; }

        public string miniOpenId { get; set; }
        [NotMapped]
        public string AgeStr { get; set; }
        [NotMapped]
        public string YearStr { get; set; }
        [NotMapped]
        public string WorkStr { get; set; }
        [NotMapped]
        public string SexStr { get; set; }

        [NotMapped]
        public string UserStr { get; set; }

        [NotMapped]
        public string RoleStr { get; set; }
        [NotMapped]
        public bool Party { get; set; }
    }
}
