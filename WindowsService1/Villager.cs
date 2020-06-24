using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    /// <summary>
    /// 村民表
    /// </summary>
    public class Villager
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string PassWord { get; set; }
        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }
        public int? Sex { get; set; }
        public string Tel { get; set; }
        public string OpenId { get; set; }
       
        
        public string UserImg { get; set; }
        public bool? IsDelete { get; set; }
        public string Idcard { get; set; }
        [StringLength(1000, MinimumLength = 3, ErrorMessage = "简介过长")]
        public string Memo { get; set; }
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 类型  2 网格员 3. 网格长 4.户主
        /// </summary>

        public virtual SysDict userRole { get; set; }


        /// <summary>
        /// 用户角色
        /// </summary>
        public Guid? userRoleId { get; set; }
        /// <summary>
        /// 管理网格ID
        /// </summary>
        public long? GridId { get; set; }
        /// <summary>
        /// 管理网格
        /// </summary>

        public virtual Gridding Grid { get; set; }

        /// <summary>
        /// 用户状态 0 禁止 1 启用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 区域
        /// </summary>

        public virtual SysDict Area { get; set; }
        /// <summary>
        /// 区域ID
        /// </summary>
        public Guid? AreaId { get; set; }
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
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 户主
        /// </summary>
        public bool? Head { get; set; }

        public string Salt { get; set; }
        [NotMapped]
        public string SecondAreaKey { get; set; }
        [NotMapped]
        public string ThirdAreaKey { get; set; }

        /// <summary>
        /// 标记 是否是第一次进入
        /// </summary>
        public bool Flag { get; set; }

        /// <summary>
        /// 自动进入的
        /// </summary>
        public bool Auto { get; set; }
        public string miniOpenId { get; set; }

        public long Point { get; set; }
        /// <summary>
        /// 积分榜后台映射标记
        /// </summary>
        [NotMapped]
        public string PointRate { get; set; }
        /// <summary>
        /// 网格区域名称
        /// </summary>
        [NotMapped]
        public string AreaName { get; set; }
        /// <summary>
        /// 村名称
        /// </summary>
        [NotMapped]
        public string VillageName { get; set; }
        /// <summary>
        ///城名称
        /// </summary>
        [NotMapped]
        public string CityName { get; set; }


        /// <summary>
        /// 0 非专职 1专职
        /// </summary>
        public int UnPart { get; set; }
    }
}
