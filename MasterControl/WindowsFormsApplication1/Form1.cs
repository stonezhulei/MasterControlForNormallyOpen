using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication4;
using CloseValve;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        sent_tcp tcp;
        sent_tcp tcp1;
        sent_tcp tcp2;
        sent_tcp tcp3;
        bool[] okngdata = new bool[30];
        string[] shujudata = new string[30];

        ListData ld1 = new ListData();
        ListData ld2 = new ListData();
        ListData ld3 = new ListData();
        ListData ld4 = new ListData();
        ListData ld5 = new ListData();
        ListData ld6 = new ListData();
        ListData ld7 = new ListData();
        ListData ld8 = new ListData();
        ListData ld9 = new ListData();
        ListData ld10 = new ListData();
        ListData ld11 = new ListData();
        ListData ld12 = new ListData();
        ListData ld13 = new ListData();
        ListData ld14 = new ListData();
        ListData ld15 = new ListData();
        ListData ld16 = new ListData();
        ListData ld17 = new ListData();

        Results re1 = new Results();
        Results re2 = new Results();
        Results re3 = new Results();
        Results re4 = new Results();
        Results re5 = new Results();
        Results re6 = new Results();
        Results re7 = new Results();
        Results re8 = new Results();
        Results re9 = new Results();
        Results re10 = new Results();
        Results re11 = new Results();
        Results re12 = new Results();
        Results re13 = new Results();
        Results re14 = new Results();
        Results re15 = new Results();
        Results re16 = new Results();
        Results re17 = new Results();
        Results re18 = new Results();
        Results re19 = new Results();
        Results re20 = new Results();
        Results re21 = new Results();
        Results re22 = new Results();      
        public Form1()
        {
            InitializeComponent();
            dataGridView1.Rows.Add();
         //   add_data();
        }
        public void add_data()
        {
            SubmitData sd = new SubmitData();
            sd.pID = "123";
            sd.EndTime = DateTime.Now;
            sd.ErrorCode = 1;
            sd.ErrorMsg = "错";
            
            ld1.Name = "OP30压机位移数据"; ld1.Values = shujudata[0];
            ld2.Name = "OP30压机压力数据"; ld2.Values = shujudata[1];
            ld3.Name = "测滤网图片编码"; ld3.Values = shujudata[2];
            ld4.Name = "粗弹簧图片编码"; ld4.Values = shujudata[3];
            ld5.Name = "OP90压机位移数据"; ld5.Values = shujudata[4];
            ld6.Name = "OP90压机压力数据"; ld6.Values = shujudata[5];
            ld7.Name = "细弹簧图片"; ld7.Values = shujudata[6];
            ld8.Name = "OP110压机位移数据"; ld8.Values = shujudata[7];
            ld9.Name = "OP110压机压力数据"; ld9.Values = shujudata[8];
            ld10.Name = "OP120压机行程值"; ld10.Values = shujudata[9];
            ld11.Name = "OP120压机压力值"; ld11.Values = shujudata[10];
            ld12.Name = "二维码数据"; ld12.Values = shujudata[11];
            ld13.Name = "行程复测"; ld13.Values = shujudata[12];
            ld14.Name = "高压测试"; ld14.Values = shujudata[13];
            ld15.Name = "O型圈图片"; ld15.Values = shujudata[14];
            ld16.Name = "低压测试"; ld16.Values = shujudata[15];
            ld17.Name = "性能测试"; ld17.Values = shujudata[16];
                  
            re1.Station = "OP010过度套放置OK/NG"; re1.Result = okngdata[0];
            re2.Station = "OP020阀套放置OK/NG"; re2.Result = okngdata[1];
            re3.Station = "OP030压装OK/NG"; re3.Result = okngdata[2];
            re4.Station = "OP040焊接OK/NG"; re4.Result = okngdata[3];
            re5.Station = "OP050提升架放置OK/NG"; re5.Result = okngdata[4];
            re6.Station = "OP060动芯上料OK/NG"; re6.Result = okngdata[5];
            re7.Station = "OP70滤网上料OK/NG"; re7.Result = okngdata[6];
            re8.Station = "OP80粗弹簧上料OK/NG"; re8.Result = okngdata[7];
            re9.Station = "OP80粗弹簧/滤网检测有无OK/NG"; re9.Result = okngdata[8];
            re10.Station = "OP090压装结果OK/NG"; re10.Result = okngdata[9];
            re11.Station = "OP100细弹簧检测有无OK/NG"; re11.Result = okngdata[10];
            re12.Station = "OP110静芯预压OK/NG"; re12.Result = okngdata[11];
            re13.Station = "OP120精压结果OK/NG"; re13.Result = okngdata[12];
            re14.Station = "OP130静芯焊接结果OK/NG"; re14.Result = okngdata[13];
            re15.Station = "OP130激光打标完成"; re15.Result = okngdata[14];
            re16.Station = "OP140读码结果OK/NG"; re16.Result = okngdata[15];
            re17.Station = "OP150行程复测结果OK/NG"; re17.Result = okngdata[16];
            re18.Station = "OP160高压测试上料OK/NG"; re18.Result = okngdata[17];
            re19.Station = "OP160高压测试结果OK/NG"; re19.Result = okngdata[18];
            re20.Station = "OP170O型圈装配OK/NG"; re20.Result = okngdata[19];
            re21.Station = "OP180低压测试上料OK/NG"; re21.Result = okngdata[20];
            re22.Station = "OP180低压测试结果OK/NG"; re22.Result = okngdata[21];


            sd.Data.Add(ld1);
            sd.Data.Add(ld2);
            sd.Data.Add(ld3);
            sd.Data.Add(ld4);
            sd.Data.Add(ld5);
            sd.Data.Add(ld6);
            sd.Data.Add(ld7);
            sd.Data.Add(ld8);
            sd.Data.Add(ld9);
            sd.Data.Add(ld10);
            sd.Data.Add(ld11);
            sd.Data.Add(ld12);
            sd.Data.Add(ld13);
            sd.Data.Add(ld14);
            sd.Data.Add(ld15);
            sd.Data.Add(ld16);
            sd.Data.Add(ld17);
            
            sd.Result.Add(re1);
            sd.Result.Add(re2);
            sd.Result.Add(re3);
            sd.Result.Add(re4);
            sd.Result.Add(re5);
            sd.Result.Add(re6);
            sd.Result.Add(re7);
            sd.Result.Add(re8);
            sd.Result.Add(re9);
            sd.Result.Add(re10);
            sd.Result.Add(re11);
            sd.Result.Add(re12);
            sd.Result.Add(re13);
            sd.Result.Add(re14);
            sd.Result.Add(re15);
            sd.Result.Add(re16);
            sd.Result.Add(re17);
            sd.Result.Add(re18);
            sd.Result.Add(re19);
            sd.Result.Add(re20);
            sd.Result.Add(re21);
            sd.Result.Add(re22);

            Insert ist = new Insert();
            ist.Add(sd);
        }
        public void shujuzhuanhuan()
        {
            change_data(data_class.byteData);
            OKNG_data(data_class.byteData);
            data_class.flag = true;
            add_data();
        }
        public void shujuzhuanhuan1()
        {

        }
        public void OKNG_data(byte [] data)
        {
            if (data[1] == 0x0A)  { okngdata[0] = true; } else{  okngdata[0] = false; }
            if (data[2] == 0x0A)  { okngdata[1] = true; } else { okngdata[1] = false; }
            if (data[4] == 0x0A)  { okngdata[2] = true; } else { okngdata[2] = false; }
            if (data[6] == 0x0A)  { okngdata[3] = true; } else { okngdata[3] = false; }
            if (data[7] == 0x0A)  { okngdata[4] = true; } else { okngdata[4] = false; }
            if (data[8] == 0x0A)  { okngdata[5] = true; } else { okngdata[5] = false; }
            if (data[9] == 0x0A)  { okngdata[6] = true; } else { okngdata[6] = false; }
            if (data[10] == 0x0A) { okngdata[7] = true; } else { okngdata[7] = false; }
            if (data[11] == 0x0A) { okngdata[8] = true; } else { okngdata[8] = false; }
            if (data[13] == 0x0A) { okngdata[9] = true; } else { okngdata[9] = false; }
            if (data[14] == 0x0A) { okngdata[10] = true; } else { okngdata[10] = false; }
            if (data[15] == 0x0A) { okngdata[11] = true; } else { okngdata[11] = false; }
            if (data[17] == 0x0A) { okngdata[12] = true; } else { okngdata[12] = false; }
            if (data[18] == 0x0A) { okngdata[13] = true; } else { okngdata[13] = false; }
            if (data[19] == 0x0A) { okngdata[14] = true; } else { okngdata[14] = false; }
            if (data[20] == 0x0A) { okngdata[15] = true; } else { okngdata[15] = false; }
            if (data[21] == 0x0A) { okngdata[16] = true; } else { okngdata[16] = false; }
            if (data[22] == 0x0A) { okngdata[17] = true; } else { okngdata[17] = false; }
            if (data[23] == 0x0A) { okngdata[18] = true; } else { okngdata[18] = false; }
            if (data[24] == 0x0A) { okngdata[19] = true; } else { okngdata[19] = false; }
            if (data[25] == 0x0A) { okngdata[20] = true; } else { okngdata[20] = false; }
            if (data[26] == 0x0A) { okngdata[21] = true; } else { okngdata[21] = false; }          
        }
        public void change_data(byte[] data)
        {
            Int64 temp = 0;
            string strtemp = "";
            temp = data[30] * 16777216 + data[31] * 65536 + data[32] * 256 + data[33];
            shujudata[0] = Convert.ToString((double)temp / 1000);  //30-33

            temp = data[34] * 16777216 + data[35] * 65536 + data[36] * 256 + data[37];
            shujudata[1] = Convert.ToString((double)temp / 1000);  //30-33

            shujudata[2] = Encoding.Default.GetString(data, 40, 20);//测滤网图片编码（ASCII）

            shujudata[3] = Encoding.Default.GetString(data, 60, 20);//粗弹簧图片编码（ASCII）

            temp = data[80] * 16777216 + data[81] * 65536 + data[82] * 256 + data[83];
            shujudata[4] = Convert.ToString((double)temp / 1000);  //OP90压机位移数据(C3 4F=49.999)转换成10进制除以1000,80-83

            temp = data[84] * 16777216 + data[85] * 65536 + data[86] * 256 + data[87];
            shujudata[5] = Convert.ToString((double)temp / 1000);  //OP90压机压力数据(C3 4F=49.999)转换成10进制除以1000,84-87

            shujudata[6] = Encoding.Default.GetString(data, 100, 20);//细弹簧图片(ASCII),100-119

            temp = data[120] * 16777216 + data[121] * 65536 + data[122] * 256 + data[123];
            shujudata[7] = Convert.ToString((double)temp / 1000);  //OP110压机位移数据(C3 4F=49.999)转换成10进制除以1000,120-123

            temp = data[124] * 16777216 + data[125] * 65536 + data[126] * 256 + data[127];
            shujudata[8] = Convert.ToString((double)temp / 1000);  //OP110压机压力数据(C3 4F=49.999)转换成10进制除以1000,124-127

            temp = data[130] * 16777216 + data[131] * 65536 + data[132] * 256 + data[133];
            shujudata[9] = Convert.ToString((double)temp / 1000);  //OP120压机行程值(C3 4F=49.999)转换成10进制除以1000,130-133

            temp = data[134] * 16777216 + data[135] * 65536 + data[136] * 256 + data[137];
            shujudata[10] = Convert.ToString((double)temp / 1000);  //OP120压机压力值(C3 4F=49.999)转换成10进制除以1000,134-137

            shujudata[11] = Encoding.Default.GetString(data, 140, 20);//二维码数据(ASCII) 140-159

            temp = data[160] * 16777216 + data[161] * 65536 + data[162] * 256 + data[163];
            shujudata[12] = Convert.ToString((double)temp / 1000);  //行程复测(C3 4F=49.999)转换成10进制除以1000,160-163

            temp = data[170] * 16777216 + data[171] * 65536 + data[172] * 256 + data[173];
            shujudata[13] = Convert.ToString((double)temp / 1000);  //高压测试（前4位是压力，后4位是ASCII泄露值）,170-179
            strtemp= Encoding.Default.GetString(data, 174, 4);
            shujudata[13] = shujudata[13] + "," + strtemp;

            shujudata[14] = Encoding.Default.GetString(data, 180, 20);//O型圈图片(ASCII)180-199

            shujudata[15] = Encoding.Default.GetString(data, 200, 10);//低压测试(ASCII)200-209

            temp = data[210] * 16777216 + data[211] * 65536 + data[212] * 256 + data[213];
            shujudata[16] = Convert.ToString((double)temp / 1000);  //性能测试(C3 4F=49.999)转换成10进制除以1000 210-213

        }

        private void Form1_Load(object sender, EventArgs e)
        {
             tcp = new sent_tcp();
             tcp.dy = shujuzhuanhuan;
             tcp.sent_connect();

            //tcp1 = new sent_tcp();
            //tcp1.dy = shujuzhuanhuan1;
            //tcp1.sent_connect();
        }
        public void qq()
        {
            B1_1.BackColor = Color.Gray;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (data_class.flag == true)
            {

                textBox1.Text = data_class.tcp_data;
                for (int t = 0; t < 17; t++)
                {
                    dataGridView1.Rows[0].Cells[t].Value = (object)shujudata[t];
                }
                Color ok, ng, baojing;
                ok = Color.Blue;
                ng = Color.Red;
                baojing = Color.Red;
                if (okngdata[0]) { B1_1.BackColor = ok; } else { B1_2.BackColor = ng; }
                if (okngdata[1]) { B2_1.BackColor = ok; } else { B2_2.BackColor = ng; }
                if (okngdata[2]) { B3_1.BackColor = ok; } else { B3_2.BackColor = ng; }
                if (okngdata[3]) { B4_1.BackColor = ok; } else { B4_2.BackColor = ng; }
                if (okngdata[4]) { B5_1.BackColor = ok; } else { B5_2.BackColor = ng; }
                if (okngdata[5]) { B6_1.BackColor = ok; } else { B6_2.BackColor = ng; }
                if (okngdata[6]) { B7_1.BackColor = ok; } else { B7_2.BackColor = ng; }
                if (okngdata[7]) { B8_1.BackColor = ok; } else { B8_2.BackColor = ng; }
                if (okngdata[8]) { B9_1.BackColor = ok; } else { B9_2.BackColor = ng; }
                if (okngdata[9]) { B10_1.BackColor = ok; } else { B10_2.BackColor = ng; }
                if (okngdata[10]) { B11_1.BackColor = ok; } else { B11_2.BackColor = ng; }
                if (okngdata[11]) { B12_1.BackColor = ok; } else { B12_2.BackColor = ng; }
                if (okngdata[12]) { B13_1.BackColor = ok; } else { B13_2.BackColor = ng; }
                if (okngdata[13]) { B14_1.BackColor = ok; } else { B14_2.BackColor = ng; }
                if (okngdata[14]) { B15_1.BackColor = ok; } else { B15_2.BackColor = ng; }
                if (okngdata[15]) { B16_1.BackColor = ok; } else { B16_2.BackColor = ng; }
                if (okngdata[16]) { B17_1.BackColor = ok; } else { B17_2.BackColor = ng; }
                if (okngdata[17]) { B18_1.BackColor = ok; } else { B18_2.BackColor = ng; }
                if (okngdata[18]) { B19_1.BackColor = ok; } else { B19_2.BackColor = ng; }
                if (okngdata[19]) { B20_1.BackColor = ok; } else { B20_2.BackColor = ng; }
                if (okngdata[20]) { B21_1.BackColor = ok; } else { B21_2.BackColor = ng; }
                data_class.flag = false;
            }
            

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcp.stop_tcp();
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }
    }
}
