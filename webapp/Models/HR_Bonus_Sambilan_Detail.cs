using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eSPP.Models
{
    public class HR_BONUS_SAMBILAN_DETAIL
    {
        [Key]
        [Column(Order = 0)]
        [MaxLength(5)]
        public string HR_NO_PEKERJA { get; set; }

        [MaxLength(20)]
        public string HR_NO_KPBARU { get; set; }

        [Key]
        [Column(Order = 1)]
        public int HR_TAHUN_BONUS { get; set; }

        [Key]
        [Column(Order = 2)]
        public int HR_BULAN_BONUS { get; set; }

        public Nullable<decimal> HR_JANUARI { get; set; }
        public Nullable<decimal> HR_FEBRUARI { get; set; }
        public Nullable<decimal> HR_MAC { get; set; }
        public Nullable<decimal> HR_APRIL { get; set; }
        public Nullable<decimal> HR_MEI { get; set; }
        public Nullable<decimal> HR_JUN { get; set; }
        public Nullable<decimal> HR_JULAI { get; set; }
        public Nullable<decimal> HR_OGOS { get; set; }
        public Nullable<decimal> HR_SEPTEMBER { get; set; }
        public Nullable<decimal> HR_OKTOBER { get; set; }
        public Nullable<decimal> HR_NOVEMBER { get; set; }
        public Nullable<decimal> HR_DISEMBER { get; set; }

        public Nullable<decimal> HR_JUMLAH_GAJI { get; set; }
        public Nullable<decimal> HR_GAJI_PURATA { get; set; }
        public Nullable<decimal> HR_BONUS_DITERIMA { get; set; }

        [MaxLength(1000)]
        public string HR_CATATAN { get; set; }
        public int HR_MUKTAMAD { get; set; }

        public static void TestInsert()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HR_BONUS_SAMBILAN_DETAIL det = new HR_BONUS_SAMBILAN_DETAIL();
            det.HR_NO_PEKERJA = "01410";
            det.HR_NO_KPBARU = "800615085334";
            det.HR_BULAN_BONUS = 1;
            det.HR_TAHUN_BONUS = 2018;
            det.HR_JANUARI = 10;
            det.HR_FEBRUARI = 20;
            det.HR_MAC = 30;
            det.HR_APRIL = 40;
            det.HR_MEI = 50;
            det.HR_JUN = 60;
            det.HR_JULAI = 70;
            det.HR_OGOS = 0;
            det.HR_SEPTEMBER = 0;
            det.HR_OKTOBER = 0;
            det.HR_NOVEMBER = 0;
            det.HR_DISEMBER = 0;
            det.HR_CATATAN = string.Empty;
            det.HR_MUKTAMAD = 0;
            db.HR_BONUS_SAMBILAN_DETAIL.Add(det);
            db.SaveChanges();
        }

        public static void TestingUpdate()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HR_BONUS_SAMBILAN_DETAIL det = db.HR_BONUS_SAMBILAN_DETAIL
                .Where(x => x.HR_NO_PEKERJA == "01410"
                && x.HR_BULAN_BONUS == 1
                && x.HR_TAHUN_BONUS == 2018).FirstOrDefault();
            if (det != null)
            {
                det.HR_JUMLAH_GAJI = det.HR_JANUARI + det.HR_FEBRUARI + det.HR_MAC + det.HR_APRIL + det.HR_MEI + det.HR_JUN
                    + det.HR_JULAI + det.HR_OGOS + det.HR_SEPTEMBER + det.HR_OKTOBER + det.HR_NOVEMBER + det.HR_DISEMBER;
                det.HR_GAJI_PURATA = Decimal.Round((decimal)det.HR_JUMLAH_GAJI / 12, 3);
                det.HR_CATATAN = "Ini Kiraan Yang Tepat";
                det.HR_BONUS_DITERIMA = (decimal)12.00;
                db.Entry(det).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void TestDelete()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HR_BONUS_SAMBILAN_DETAIL det = db.HR_BONUS_SAMBILAN_DETAIL
                .Where(x => x.HR_NO_PEKERJA == "01410"
                && x.HR_BULAN_BONUS == 1
                && x.HR_TAHUN_BONUS == 2018).FirstOrDefault();
            if (det != null)
            {
                db.Entry(det).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static List<HR_BONUS_SAMBILAN_DETAIL> GetBonusSambilanDetailData(int month, int year)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            List<HR_BONUS_SAMBILAN_DETAIL> list = db.HR_BONUS_SAMBILAN_DETAIL
                .Where(x => x.HR_BULAN_BONUS == month
                && x.HR_TAHUN_BONUS == year).ToList();
            return list;
        }

        public static void UpdateBonusDiterima(int month, int year, string noPekerja, decimal bonus)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HR_BONUS_SAMBILAN_DETAIL det = db.HR_BONUS_SAMBILAN_DETAIL
                .Where(x => x.HR_BULAN_BONUS == month
                && x.HR_TAHUN_BONUS == year
                && x.HR_NO_PEKERJA == noPekerja).FirstOrDefault();
            if(det != null)
            {
                det.HR_BONUS_DITERIMA = bonus;
                db.Entry(det).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void UpdateCatatan(int month, int year, string noPekerja, string catatan)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            HR_BONUS_SAMBILAN_DETAIL det = db.HR_BONUS_SAMBILAN_DETAIL
                .Where(x => x.HR_BULAN_BONUS == month
                && x.HR_TAHUN_BONUS == year
                && x.HR_NO_PEKERJA == noPekerja).FirstOrDefault();
            if (det != null)
            {
                det.HR_CATATAN = catatan;
                db.Entry(det).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

    }
}