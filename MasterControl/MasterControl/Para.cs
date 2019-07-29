using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace MasterControl
{
    public class Para
    {
        public const int PLCNUM = 5;
        public const int PLCADDRSIZE = 4;
        public const int OPNUM = 19;
        public const int OPTOTAL = 18;

        public const int PID = 0;
        public const int STAControl = 1;
        public const int STA_STU = 2;
        public const int STA = 3;
        public const int OKNUM = 3;
        public const int NGNUM = 4;
        public const int TOTALNUM = 5;
        public const int ClearControl = 6;
        public const int ALLDataContorl = 7;
        public const int TOPCDataStr1 = 8;
        public const int TOPCDataStr2 = 9;
        public const int TOPCDataStr3 = 10;
        public const int TOPCDataStr4 = 11;

        public const int RESTABLE = 3;

        public const int PRESSURE = 2;
        public const int POSITION = 3;
        public const int FILENAME = 4;
        public const int PRESSNUM = 5;

        public const int PLCBUFNUM = 3;
        public const int PLCBUFLEN = 120;
        public const int UISHOWNUM = 36;

        private readonly string path;

        private IniFiles dataManger;

        public Para(string path)
        {
            this.path = path;
            dataManger = new IniFiles(path + "\\data.ini");

            LoadPLCIpConfig();
            LoadWorkstationAddr();
            LoadPressAddr();
        }

        public long ParseLong(string s)
        {
            long v;
            long.TryParse(s, out v);
            return v;
        }

        public void LoadYeildData(long[] a, long[] o)
        {
            string ls = dataManger.ReadString("DATA", "A", "");
            string os = dataManger.ReadString("DATA", "O", "");

            string[] temp1 = ls.Split(',');
            for (int i = 0; i < a.Length && i < temp1.Length; i++)
            {
                a[i] = ParseLong(temp1[i]);
            }

            string[] temp2 = os.Split(',');
            for (int i = 0; i < o.Length && i < temp2.Length; i++)
            {
                o[i] = ParseLong(temp2[i]);
            }
        }

        public void SaveYeildData(long[] a, long[] o)
        {
            dataManger.WriteString("DATA", "A", string.Join(",", a));
            dataManger.WriteString("DATA", "O", string.Join(",", o));
        }

        public StringCollection LoadResultData()
        {
            string s = dataManger.ReadString("DATA", "R", "");
            StringCollection data = new StringCollection();
            data.AddRange(s.Split(','));
            return data;
        }

        public void SaveResultData(StringCollection data)
        {
            dataManger.WriteString("DATA", "R", string.Join(",", data.OfType<string>()));
        }

        public void LoadPLCIpConfig()
        {
            StringCollection sectionList = new StringCollection();
            NameValueCollection values = new NameValueCollection();
            IniFiles iniManger = new IniFiles(path + "\\config.ini");
            iniManger.ReadSectionValues("COMMON", values);
            for (int i = 0; i < values.Count; i++)
            {
                sectionList.Add(values[i]);
            }

            this.IpConfig = sectionList;
        }

        public void LoadWorkstationAddr()
        {
            StringCollection sectionList = new StringCollection();
            IniFiles iniManger = new IniFiles(path + "\\plcAddress.ini");
            iniManger.ReadSections(sectionList);

            List<ushort[]> addrList = new List<ushort[]>();
            foreach (string section in sectionList)
            {
                NameValueCollection values = new NameValueCollection();
                iniManger.ReadSectionValues(section, values);
                ushort[] plcAddr = new ushort[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    if (ushort.TryParse(values[i], out plcAddr[i]) && i == 0)
                    {
                        plcAddr[i] -= 1;
                    }  
                }

                addrList.Add(plcAddr);
            }

            this.Worklist = addrList;
            this.WorklistName = sectionList;
        }

        public void LoadPressAddr()
        {
            StringCollection sectionList = new StringCollection();
            IniFiles iniManger = new IniFiles(path + "\\plcAddress2.ini");
            iniManger.ReadSections(sectionList);

            List<ushort[]> addrList = new List<ushort[]>();
            foreach (string section in sectionList)
            {
                NameValueCollection values = new NameValueCollection();
                iniManger.ReadSectionValues(section, values);
                ushort[] plcAddr = new ushort[values.Count];
                for (int i = 0; i < values.Count; i++)
                {
                    if (ushort.TryParse(values[i], out plcAddr[i]) && i == 0)
                    {
                        plcAddr[i] -= 1;
                    }
                }

                addrList.Add(plcAddr);
            }

            this.Presslist = addrList;
        }

        public StringCollection IpConfig { get; private set; }

        public List<ushort[]> Presslist { get; private set; }

        public List<ushort[]> Worklist { get; private set; }

        public StringCollection WorklistName { get; private set; }
    }
}
