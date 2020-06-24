using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WGZL.Models
{
    public class SysDict
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 字典值
        /// </summary>
        public string DictKey { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Summary { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 钥匙
        /// </summary>
        public string Key { get; set; }

        public List<Gridding> griddings { get; set; } 

    }
}
