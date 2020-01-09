/*
 * 作者：姚强
 * 功能描述：地质评价数据的实体
 * 日期：2020-1-3
 * 修改人：
 * 修改时间： 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class GeologicalEvaluation
    {
        #region 地质评价
        
        /// <summary>
        /// 深度
        /// </summary>
        public double depth { get; set; }
        /// <summary>
        /// AC
        /// </summary>
        public double AC { get; set; }
        /// <summary>
        /// GR
        /// </summary>
        public double GR { get; set; }
        /// <summary>
        /// SP
        /// </summary>
        public double SP { get; set; }
        /// <summary>
        /// DEN
        /// </summary>
        public double DEN { get; set; }
        /// <summary>
        /// POR
        /// </summary>
        public double POR { get; set; }
        /// <summary>
        /// PER
        /// </summary>
        public double PER { get; set; }
        /// <summary>
        /// SW
        /// </summary>
        public double SW { get; set; }
        #endregion
    }
}
