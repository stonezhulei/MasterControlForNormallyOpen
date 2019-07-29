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

        private readonly string path;

        public Para(string path)
        {
            this.path = path;
            LoadPLCIpConfig();
            LoadWorkstationAddr();
            LoadPressAddr();
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
