using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBViewer
{
    public class Fields
    {
        /// <summary>
        /// 1.时间
        /// </summary>
        public string Time
        {
            get;
            set;
        }

        /// <summary>
        /// 2.产品类型
        /// </summary>
        public string ProductType { get; set; }

        /// <summary>
        /// 3.工单条码
        /// </summary>
        public string WorksheetBarcode { get; set; }

        /// <summary>
        /// 4.器具条码
        /// </summary>
        public string QJBarcode { get; set; }

        /// <summary>
        /// 5.条码
        /// </summary>
        public string Barcode { get; set; }

        /// <summary>
        /// 6.RFID序列号 @280980980
        /// </summary>
        public string RfidBarcode { get; set; }

        /// <summary>
        /// 7.OP10结果 0109  -> 01-OP10 09-OK 01-NG
        /// </summary>
        public string OP10Result { get; set; }

        /// <summary>
        /// 8.OP10过渡套视觉检测 A0731 150958
        /// </summary>
        public string OP10VisionResult { get; set; }

        /// <summary>
        /// 9.OP10程序号
        /// </summary>
        public string OP10ProID { get; set; }

        /// <summary>
        /// 10.OP20结果
        /// </summary>
        public string OP20Result { get; set; }

        /// <summary>
        /// 11.OP20测虑网视觉检测
        /// </summary>
        public string OP20VisionResult { get; set; }

        /// <summary>
        /// 12.OP30常开阀结果
        /// </summary>
        public string OP30Result { get; set; }

        /// <summary>
        /// 13.OP30GT测试高度 21579 -> 21.579
        /// </summary>
        public string OP30Height { get; set; }

        /// <summary>
        /// 14.OP30程序号 CK045 -> CK0.45  TC100 -> TC1.0
        /// </summary>
        public string OP30ProID { get; set; }

        /// <summary>
        /// 15.OP40结果
        /// </summary>
        public string OP40Result { get; set; }

        /// <summary>
        /// 16.OP40压力值 3061  -> 3.061
        /// </summary>
        public string OP40Press { get; set; }

        /// <summary>
        /// 17.OP40位移值 49555  -> 49.555
        /// </summary>
        public string OP40Distance { get; set; }

        /// <summary>
        /// 18.OP40程序号 0001 -> A-Pr01 0002 -> B-Pr01 ?? 【9种】
        /// </summary>
        public string OP40ProID { get; set; }

        /// <summary>
        /// 19.OP40文件存储
        /// </summary>
        public string OP40FileName { get; set; }

        /// <summary>
        /// 20.OP50结果
        /// </summary>
        public string OP50Result { get; set; }
        
        /// <summary>
        /// 21.OP50GT测试深度 12023 -> 12.023  12030 -> 12.030
        /// </summary>
        public string OP50SD { get; set; }

        /// <summary>
        /// 22.OP50程序号 CK045 -> CK0.45  TC100 -> TC1.0
        /// </summary>
        public string OP50ProID { get; set; }

        /// <summary>
        /// 23.OP60结果
        /// </summary>
        public string OP60Result { get; set; }

        /// <summary>
        /// 24.OP60弹簧型号  A -> A振盘
        /// </summary>
        public string OP60Type { get; set; }

        /// <summary>
        /// 25.OP70结果
        /// </summary>
        public string OP70Result { get; set; }

        /// <summary>
        /// 26.OP70阀杆理论值 12213 -> 12.213
        /// </summary>
        public string OP70Value1 { get; set; }

        /// <summary>
        /// 27.OP70阀杆实际值 12213 -> 12.213
        /// </summary>
        public string OP70Value2 { get; set; }

        /// <summary>
        /// 28.OP70气隙值 186 -> 0.186
        /// </summary>
        public string OP70Value3 { get; set; }

        /// <summary>
        /// 29.OP80结果
        /// </summary>
        public string OP80Result { get; set; }

        /// <summary>
        /// 30.OP90结果
        /// </summary>
        public string OP90Result { get; set; }

        /// <summary>
        /// 31.OP90初始行程值 755 -> 0.755
        /// </summary>
        public string OP90Value1 { get; set; }

        /// <summary>
        /// 32.OP90最终行程值 755 -> 0.755
        /// </summary>
        public string OP90Value2 { get; set; }

        /// <summary>
        /// 33.OP90压力值 35 -> 0.35
        /// </summary>
        public string OP90Press { get; set; }

        /// <summary>
        /// 34.OP90位移值 70021 -> 70.021
        /// </summary>
        public string OP90Distance { get; set; }

        /// <summary>
        /// 35.OP90程序号 0001 - 0009 -> A-CK0.45 A-CK0.65 【9种】
        /// </summary>
        public string OP90ProID { get; set; }

        /// <summary>
        /// 36.OP90文件存储
        /// </summary>
        public string OP90FileName { get; set; }

        /// <summary>
        /// 37.OP100结果
        /// </summary>
        public string OP100Result { get; set; }
        
        /// <summary>
        /// 38.OP100焊接功率
        /// </summary>
        public string OP100PowValue { get; set; }

        /// <summary>
        /// 39.OP100焊接时间 22 -> 2.2
        /// </summary>
        public string OP100Time { get; set; }

        /// <summary>
        /// 40.OP100焊接转速 6 -> 0.6
        /// </summary>
        public string OP100CircleTime { get; set; }

        /// <summary>
        /// 41.OP110结果
        /// </summary>
        public string OP110Result { get; set; }

        /// <summary>
        /// 42.OP110二维码
        /// </summary>
        public string OP110Code { get; set; }

        /// <summary>
        /// 43.OP120结果
        /// </summary>
        public string OP120Result { get; set; }

        /// <summary>
        /// 44.OP120测试行程值 241 -> 0.241
        /// </summary>
        public string OP120Value { get; set; }

        /// <summary>
        /// 45.OP120程序号 A -> A-CK0.45  B -> CK0.65 【9种】
        /// </summary>
        public string OP120ProID { get; set; }

        /// <summary>
        /// 46.OP130结果
        /// </summary>
        public string OP130Result { get; set; }

        /// <summary>
        ///  47.OP130单项密封圈视觉检测 A0731 140958
        /// </summary>
        public string OP130VisionResult { get; set; }

        /// <summary>
        /// 48.OP140结果
        /// </summary>
        public string OP140Result { get; set; }

        /// <summary>
        /// 49.OP140压力值  65 -> 0.65
        /// </summary>
        public string OP140Press { get; set; }

        /// <summary>
        /// 50.OP140位移值  70021 -> 70.021
        /// </summary>
        public string OP140Distance { get; set; }

        /// <summary>
        /// 51.OP140程序号 0001 -> Pr01 0002 -> Pr02？？？？
        /// </summary>
        public string OP140ProID { get; set; }

        /// <summary>
        /// 52.OP140文件存储
        /// </summary>
        public string OP140FileName { get; set; }

        /// <summary>
        /// 53.OP140滤网铆点视觉检测 A0731 14112
        /// </summary>
        public string OP140VisionResult { get; set; }

        /// <summary>
        /// 54.OP150结果
        /// </summary>
        public string OP150Result { get; set; }

        /// <summary>
        /// 55.OP150测试气压 1300 -> 130.0
        /// </summary>
        public string OP150Press { get; set; }

        /// <summary>
        /// 56.OP150泄露量 100 -> 1
        /// </summary>
        public string OP150Leak { get; set; }

        /// <summary>
        /// 57.OP150充气气压 1321 -> 132.1
        /// </summary>
        public string OP150Press2 { get; set; }

        /// <summary>
        /// 58.OP150程序号 A -> A-Pr01 B -> B-Pr01
        /// </summary>
        public string OP150ProID { get; set; }

        /// <summary>
        /// 59.OP150文件存储
        /// </summary>
        public string OP150FileName { get; set; }

        /// <summary>
        /// 60.OP160结果
        /// </summary>
        public string OP160Result { get; set; }

        /// <summary>
        /// 61.OP160测试气压 99 -> 9.9
        /// </summary>
        public string OP160Press { get; set; }

        /// <summary>
        /// 62.OP160泄漏量
        /// </summary>
        public string OP160Leak { get; set; }
        
        /// <summary>
        /// 63.OP160 P1值 99 -> 9.9
        /// </summary>
        public string OP160P1Value { get; set; }

        /// <summary>
        /// 64.OP160 P2值 421 -> 4.21
        /// </summary>
        public string OP160P3Value { get; set; }
        
        /// <summary> 
        /// 65.OP160 P2-P1值 999 -> 9.99
        /// </summary>
        public string OP160P1DiffP2 { get; set; }

        /// <summary>
        /// 66.OP160程序号 A -> A-CK0.45  B -> CK0.65 【9种】
        /// </summary>
        public string OP160ProID { get; set; }

        /// <summary>
        /// 67.OP160文件存储
        /// </summary>
        public string OP160FileName { get; set; }

        /// <summary>
        /// 68.OP170结果
        /// </summary>
        public string OP170Result { get; set; }
    }
}
