using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NtripShare.Model
{
  public  class MountPoint
    {
        public string LeiXing { get; set; }
        public string JieRuDian { get; set; }
        public string ShiBieHao { get; set; }
        public string ChaFenGeShi { get; set; }
        public string PinLv { get; set; }
        public string ZaiBoXiangWei { get; set; }
        public string DaoHangXiTong { get; set; }
        public string WangLuo { get; set; }
        public string GuoJia { get; set; }
        public string WeiDu { get; set; }
        public string JingDu { get; set; }
        public string NMEA { get; set; }
        public string JiZhanLeiXing { get; set; }
        public string RuanJianMingCheng { get; set; }
        public string YaSuoSuanFa { get; set; }
        public string FangWenBaoHu { get; set; }
        public string YN { get; set; }
        public string BiTeLv { get; set; }

        public MountPoint(string str) {
            string[] objects = Strings.Split(str, ";");

            LeiXing = objects[0];
            JieRuDian = objects[1];
            ShiBieHao = objects[2];
            ChaFenGeShi  = objects[3];
            PinLv = objects[4];
            ZaiBoXiangWei = objects[5];
            DaoHangXiTong = objects[6];
            WangLuo = objects[7];
            GuoJia = objects[8];
            WeiDu = objects[9];
            JingDu = objects[10];
            NMEA = objects[11];
            JiZhanLeiXing = objects[12];
            RuanJianMingCheng = objects[13];
            YaSuoSuanFa = objects[14];
            FangWenBaoHu = objects[15];
            YN = objects[16];
            BiTeLv = objects[17];
        }
        public string getString() {
            return LeiXing + ";" +
            JieRuDian + ";" +
            ShiBieHao + ";" +
            ChaFenGeShi + ";" +
            PinLv + ";" +
            ZaiBoXiangWei + ";" +
            DaoHangXiTong + ";" +
            WangLuo + ";" +
            GuoJia + ";" +
            WeiDu + ";" +
            JingDu + ";" +
            NMEA + ";" +
            JiZhanLeiXing + ";" +
            RuanJianMingCheng + ";" +
            YaSuoSuanFa + ";" +
            FangWenBaoHu + ";" +
            YN + ";" +
            BiTeLv;
        }
    }
}
