using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSPP.Models
{
    public class BonusSambilanDetailModel
    {
        public int BulanBonus { get; set; }
        public int TahunBonus { get; set; }
        public string Nama { get; set; }
        public string NoPekerja { get; set; }
        public string NoKadPengenalan { get; set; }
        public string NoAkaunBank { get; set; }
        public string NoKWSP { get; set; }
        public DateTime? TarikhLantikan { get; set; }
        public string TarikhLantikanString
        {
            get
            {
                if(TarikhLantikan != null)
                {
                    return string.Format("{0:dd-mm-yyyy}", TarikhLantikan);
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }
        public decimal? Mac { get; set; }
        public decimal? April { get; set; }
        public decimal? Mei { get; set; }
        public decimal? Jun { get; set; }
        public decimal? Julai { get; set; }
        public decimal? Ogos { get; set; }
        public decimal? September { get; set; }
        public decimal? Oktober { get; set; }
        public decimal? November { get; set; }
        public decimal? Disember { get; set; }
        public decimal? JumlahGaji { get; set; }
        public decimal? GajiPurata { get; set; }
        public decimal? BonusDiterima { get; set; }
        public string Catatan { get; set; }
        public bool IsMuktamad { get; set; }

        public static List<BonusSambilanDetailModel> GetBonusSambilanDetailData(int month, int year)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<HR_BONUS_SAMBILAN_DETAIL> tableList = HR_BONUS_SAMBILAN_DETAIL.GetBonusSambilanDetailData(month, year);

            List<BonusSambilanDetailModel> outputList = new List<BonusSambilanDetailModel>();
            foreach(HR_BONUS_SAMBILAN_DETAIL y in tableList)
            {
                BonusSambilanDetailModel d = new BonusSambilanDetailModel();
                HR_MAKLUMAT_PERIBADI maklumat = db.HR_MAKLUMAT_PERIBADI.Where(m => m.HR_NO_PEKERJA == y.HR_NO_PEKERJA).FirstOrDefault();
                HR_MAKLUMAT_PEKERJAAN kerja = db.HR_MAKLUMAT_PEKERJAAN.Where(m => m.HR_NO_PEKERJA == y.HR_NO_PEKERJA).FirstOrDefault();
                d.BulanBonus = y.HR_BULAN_BONUS;
                d.TahunBonus = y.HR_TAHUN_BONUS;
                d.Nama = maklumat.HR_NAMA_PEKERJA;
                d.NoPekerja = y.HR_NO_PEKERJA;
                d.NoKadPengenalan = y.HR_NO_KPBARU;
                d.NoAkaunBank = kerja.HR_NO_AKAUN_BANK;
                d.NoKWSP = kerja.HR_NO_KWSP;
                d.TarikhLantikan = kerja.HR_TARIKH_LANTIKAN;
                d.Jan = y.HR_JANUARI;
                d.Feb = y.HR_FEBRUARI;
                d.Mac = y.HR_MAC;
                d.April = y.HR_APRIL;
                d.Mei = y.HR_MEI;
                d.Jun = y.HR_JUN;
                d.Julai = y.HR_JULAI;
                d.Ogos = y.HR_OGOS;
                d.September = y.HR_SEPTEMBER;
                d.Oktober = y.HR_OKTOBER;
                d.November = y.HR_NOVEMBER;
                d.Disember = y.HR_DISEMBER;
                d.JumlahGaji = y.HR_JUMLAH_GAJI;
                d.GajiPurata = y.HR_GAJI_PURATA;
                d.BonusDiterima = y.HR_BONUS_DITERIMA;
                d.Catatan = y.HR_CATATAN;
                if(y.HR_MUKTAMAD > 0)
                {
                    d.IsMuktamad = true;
                }
                else
                {
                    d.IsMuktamad = false;
                }
                outputList.Add(d);
            }
            return outputList;
        }
    }
}