using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBViewer
{
    internal class PageItem
    {
        /// <summary>
        /// 每页记录数
        /// </summary>
        internal int PageSize { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        internal int RecordCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        internal int PageCount
        {
            get {
                if (PageSize == 0)
                    return this.PageSize;
                return (RecordCount % PageSize != 0) ? RecordCount / PageSize + 1 : RecordCount / PageSize; 
            }
        }

        /// <summary>
        /// 当前页
        /// </summary>
        internal int CurrentPage { get; set; }

        /// <summary>
        /// 当前选中行
        /// </summary>
        internal int CurrentSelectRowIndex { get; set; }

        /// <summary>
        /// 当前页显示的第一行
        /// </summary>
        internal int FirstDisplayedScrollingRowIndex { get; set; }

        /// <summary>
        /// 当前记录数
        /// </summary>
        internal int CurrentShowCount
        {
            get { return this.CurrentPage * this.PageSize + this.CurrentSelectRowIndex; }
        }
    }
}
