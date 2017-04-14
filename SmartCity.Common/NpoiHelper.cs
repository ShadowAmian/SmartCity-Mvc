using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartCity.Common
{
    /// <summary>
    /// 日志导出Helper
    /// </summary>
    public class NpoiHelper
    {
        public readonly HSSFWorkbook _workbook;
        public readonly HSSFSheet _sheet1;
        public readonly string[] _params;

        public HSSFWorkbook Workbook
        {
            get { return _workbook; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="title">合同和sheet的标题</param>
        /// <param name="colInfos">[注意，序号会自动生成]params列名参数，数目即列数</param>
        public NpoiHelper(string title, params string[] colInfos)
        {
            this._workbook = new HSSFWorkbook();
            this._sheet1 = (HSSFSheet)Workbook.CreateSheet(title);
            this._params = colInfos;
            this.SetFileInfo();
            this.SetTitleRow(title, colInfos.Length);
            this.SetSecondRow(_params);
        }
        /// <summary>
        /// 设置Workbook的2个属性信息
        /// </summary>
        private void SetFileInfo()
        {
            //设置Workbook的DocumentSummaryInformation信息
            DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
            dsi.Company = "CHMTECH";
            this.Workbook.DocumentSummaryInformation = dsi;
            //设置Workbook的SummaryInformation信息
            SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
            si.Subject = "CHMTECH EXCEL-EXPORT";
            this.Workbook.SummaryInformation = si;
        }
        /// <summary>
        /// 设置第一行为title行 里面的一些样式直接写死了
        /// </summary>
        /// <param name="titleStr">第一行title的context</param>
        /// <param name="mergedCount">合并的列数，一般为列头信息个数</param>
        /// <returns></returns>
        private void SetTitleRow(string titleStr, int mergedCount)
        {
            IRow titleRow = this._sheet1.CreateRow(0);
            titleRow.Height = 30 * 20;
            ICellStyle titleStyle = this.Workbook.CreateCellStyle();
            titleStyle.Alignment = HorizontalAlignment.Center; //字体排列
            //合并titleRow的格子 因为多了个序号，所以合并的时候也需要多合并一格
            this._sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, mergedCount - 1));
            IFont font = this.Workbook.CreateFont(); //set font style
            font.FontHeight = 40 * 20;
            font.Boldweight = (short)FontBoldWeight.Bold; //bold
            font.FontHeightInPoints = 16; //字体大小
            titleStyle.SetFont(font);
            ICell titleCell = titleRow.CreateCell(0);
            titleCell.SetCellValue(new HSSFRichTextString(titleStr)); //title context
            titleCell.CellStyle = titleStyle; //bind style
        }
        //设置第二行的列头信息
        private void SetSecondRow(string[] headArr)
        {
            ICellStyle style = this.Workbook.CreateCellStyle();
            style.VerticalAlignment = VerticalAlignment.Justify;//垂直对齐(默认应该为center，如果center无效则用justify)
            style.Alignment = HorizontalAlignment.Center;
            style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index;
            style.FillPattern = FillPattern.SolidForeground;
            IRow irow = this._sheet1.CreateRow(1);
            //序号格子
            this._sheet1.SetColumnWidth(0, 25 * 256);
            irow.Height = 20 * 20;
            for (int i = 0; i < headArr.Length; i++)
            {
                string cellValue = headArr[i];
                HSSFCell curCell = (HSSFCell)irow.CreateCell(i);
                curCell.SetCellValue(cellValue);
                this._sheet1.SetColumnWidth(i + 1, 25 * 256);
                curCell.CellStyle = style;
            }
        }
        /// <summary>
        /// 提供下载，通过response输出2进制信息。
        /// </summary>
        /// <param name="Response">当前页面的response</param>
        /// <param name="fileName">文件名，无须输入后缀名</param>
        public void OutPutDownload(HttpResponseBase Response, string fileName)
        {
            //导出，让用户下载
            if (!fileName.EndsWith(@".xls"))
            {
                fileName += @".xls";
            }
            Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlPathEncode(fileName));
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/vnd.ms-excel";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            Response.Clear();
            MemoryStream file = new MemoryStream();
            this.Workbook.Write(file);
            Response.BinaryWrite(file.GetBuffer());
        }
    }
}
