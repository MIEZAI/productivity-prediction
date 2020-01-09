using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PressureYield
    {
        #region 压力产量
        /// <summary>
        /// 生产时间
        /// </summary>
        public double hours { get; set; }
        /// <summary>
        /// 油压
        /// </summary>
        public double yield { get; set; }
        /// <summary>
        /// 日产气量
        /// </summary>
        public double gas { get; set; }
        /// <summary>
        /// 日产水量
        /// </summary>
        public double water { get; set; }
        
        #endregion
    }
}
