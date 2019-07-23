using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OmronPlc;

namespace MasterControl
{
    public class PlcItem
    {
        PlcItem(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public uint STAControl { get; set; }
        public uint STA { get; set; }
    }
}
