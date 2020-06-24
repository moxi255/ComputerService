using System;
using System.Collections.Generic;


namespace WGZL.Models
{
    public partial class Gridding
    {
        public long Id { get; set; }
        /// <summary>
        /// 网格名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 网格区域ID
        /// </summary>
        public Guid AreaId { get; set; }
        /// <summary>
        /// 网格区域名称
        /// </summary>
        public string ParentName { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 封面照片
        /// </summary>
        public string Img { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string Memo { get; set; }
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
        /// 居民人数
        /// </summary>
        public long ResidentsCount { get; set; }

        /// <summary>
        /// 辖区户数
        /// </summary>
        public long AreaNumber { get; set; }

        /// <summary>
        /// 添加用户数
        /// </summary>
        public long AddUser { get; set; }
        /// <summary>
        /// 网格长
        /// </summary>


        /// <summary>
        /// 好评率
        /// </summary>
        public string GoodRating { get; set; }

        /// <summary>
        /// 网格区域ID
        /// </summary>
        public string FirstAreaKey { get; set; }
        /// <summary>
        /// 网格区域ID
        /// </summary>
        public string SecondAreaKey { get; set; }
        /// <summary>
        /// 网格区域ID
        /// </summary>
        public string ThirdAreaKey { get; set; }

        

    }
}
