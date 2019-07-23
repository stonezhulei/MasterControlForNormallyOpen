using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloseValve
{
    public  class SubmitData
    {
        /// <summary>
        /// 产品条码
        /// </summary>
        public string pID { get; set; }
        /// <summary>
        /// NG类型
        /// </summary>
        public int ErrorCode { get; set; }
        /// <summary>
        /// NG描述
        /// </summary>
        public string ErrorMsg { get; set; }
        /// <summary>
        /// 出站时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 采集值集合
        /// </summary>
        public List<ListData> Data { get; set; }
        /// <summary>
        /// 每个工站生产结果
        /// </summary>
        public List<Results> Result { get; set; }
        public SubmitData()
        {
            this.Data = new List<ListData>();
            this.Result = new List<Results>();
        }
    }
}
