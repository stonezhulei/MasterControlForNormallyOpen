using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloseValve
{
    public   class Results
    {
        /// <summary>
        /// 工站名称
        /// </summary>
        public string Station { get; set; }
        /// <summary>
        /// 结果  True=OK
        /// </summary>
        public  bool Result { get; set; }
    }
}
