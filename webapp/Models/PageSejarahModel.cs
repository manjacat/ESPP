using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace eSPP.Models
{

    public class PageSejarahModel
    {
        public PageSejarahModel()
        {
            bulandibayar = DateTime.Now.Month;
            bulanbekerja = DateTime.Now.AddMonths(-1).Month;
            tahundibayar = DateTime.Now.Year;
            tahunbekerja = DateTime.Now.AddMonths(-1).Year;
            kelulusanydp = "P";
            kelulusanydptunggakan = "P";
            IsMuktamad = false;
        }

        public PageSejarahModel(string HR_PEKERJAstr,
            string tahunbekerjastr,
            string bulanbekerjastr,
            string tahundibayarstr,
            string bulandibayarstr)
        {
            HR_PEKERJA = HR_PEKERJAstr;
            tahunbekerja = string.IsNullOrEmpty(tahunbekerjastr) ? DateTime.Now.Year : Convert.ToInt32(tahunbekerjastr);
            bulanbekerja = string.IsNullOrEmpty(bulanbekerjastr) ? DateTime.Now.AddMonths(-1).Month : Convert.ToInt32(bulanbekerjastr);
            tahundibayar = string.IsNullOrEmpty(tahundibayarstr) ? DateTime.Now.Year : Convert.ToInt32(tahundibayarstr);
            bulandibayar = string.IsNullOrEmpty(bulandibayarstr) ? DateTime.Now.Month : Convert.ToInt32(bulandibayarstr);
            try
            {
                //HR_TRANSAKSI_SAMBILAN_DETAIL gaji = db.HR_TRANSAKSI_SAMBILAN_DETAIL.SingleOrDefault(s => s.HR_NO_PEKERJA == HR_PEKERJA && s.HR_BULAN_BEKERJA == bulanbekerja && s.HR_BULAN_DIBAYAR == bulandibayar && s.HR_TAHUN == tahundibayar && s.HR_TAHUN_BEKERJA == tahunbekerja && s.HR_KOD_IND == "G");
                ApplicationDbContext db = new ApplicationDbContext();
                int? muktamad = db.HR_TRANSAKSI_SAMBILAN_DETAIL
                    .Where(x => x.HR_NO_PEKERJA == HR_PEKERJA
                    && x.HR_TAHUN_BEKERJA == tahunbekerja
                    && x.HR_TAHUN == tahundibayar
                    && x.HR_BULAN_BEKERJA == bulanbekerja
                    && x.HR_BULAN_DIBAYAR == bulandibayar
                    && x.HR_KOD_IND == "G").Select(x => x.HR_MUKTAMAD).FirstOrDefault();
                if (muktamad == 1) //if muktamad = 1
                {
                    IsMuktamad = true;
                }
            }
            catch
            {
                IsMuktamad = false;
            }

        }

        // bulan bekerja (current)
        [Display(Name = "No. Pekerja")]
        public string HR_PEKERJA { get; set; }
        [Display(Name = "Tahun Bekerja")]
        public int tahunbekerja { get; set; }
        [Display(Name = "Bahagian")]
        public string bahagian { get; set; }
        [Display(Name = "Bulan Bekerja")]
        public int bulanbekerja { get; set; }
        [Display(Name = "Jabatan")]
        public string jabatan { get; set; }
        [Display(Name = "Tahun Dibayar")]
        public int tahundibayar { get; set; }
        [Display(Name = "Tunggakan")]
        public string tunggakan { get; set; } //values: Y/T
        [Display(Name = "Bulan Dibayar")]
        public int bulandibayar { get; set; }
        [Display(Name = "Gaji Pokok")]
        public decimal gajipokok { get; set; }
        [Display(Name = "Jumlah Hari")]
        public int jumlahhari { get; set; }
        [Display(Name = "Elaun Khidmat Awam")]
        public decimal elaunkhidmatawam { get; set; }
        [Display(Name = "Jumlah OT")]
        public int jumlahot { get; set; }
        [Display(Name = "Elaun Lain")]
        public decimal elaunlain { get; set; }
        [Display(Name = "Jumlah Bayar OT")]
        public decimal jumlahbayaranot { get; set; }
        [Display(Name = "Potongan KDSK")]
        public decimal potonganksdk { get; set; }
        [Display(Name = "1/3 Gaji Pokok")]
        public decimal gajiper3 { get; set; }
        [Display(Name = "Potongan KSWP")]
        public decimal potongankwsp { get; set; }
        [Display(Name = "SOCSO")]
        public decimal socso { get; set; }
        [Display(Name = "Lain-lain Potongan")]
        public decimal potonganlain { get; set; }
        [Display(Name = "Gaji Sebelum Potongan KWSP")]
        public decimal gajisebelumkwsp { get; set; }
        [Display(Name = "Gaji Kasar Bulan Semasa")]
        public decimal gajikasar { get; set; }
        [Display(Name = "Gaji Bersih")]
        public decimal gajibersih { get; set; }
        [Display(Name = "Kelulusan Datuk Bandar")]
        public string kelulusanydp { get; set; } //values Y, T, P

        //bulan sebelum
        [Display(Name = "Tahun Bekerja")]
        public int tunggakantahunbekerja
        {
            get
            {
                try
                {
                    if (bulanbekerja == 1)
                    {
                        return tahunbekerja - 1;
                    }
                    return tahunbekerja;
                }
                catch
                {
                    return 2000;
                }
            }
        }

        [Display(Name = "Bulan Bekerja")]
        public int tunggakanbulanbekerja
        {
            get
            {
                try
                {
                    if (bulanbekerja == 1)
                    {
                        return 12;
                    }
                    return bulanbekerja - 1;
                }
                catch
                {
                    return 1;
                }
            }
        }
        [Display(Name = "Tahun Dibayar")]
        public int tunggakantahundibayar
        {
            get
            {
                return tahundibayar;
            }
        }
        [Display(Name = "Bulan Dibayar")]
        public int tunggakanbulandibayar
        {
            get
            {
                return bulandibayar;
            }
        }

        [Display(Name = "Gaji Pokok")]
        public decimal tunggakangajipokok { get; set; }
        [Display(Name = "Jumlah Hari")]
        public int tunggakanjumlahhari { get; set; }
        [Display(Name = "Elaun Khidmat Awam")]
        public decimal tunggakanelaunkhidmatawam { get; set; }
        [Display(Name = "Jumlah OT")]
        public int tunggakanjumlahot { get; set; }
        [Display(Name = "Elaun Lain")]
        public decimal tunggakanelaunlain { get; set; }
        [Display(Name = "Jumlah Bayar OT")]
        public decimal tunggakanjumlahbayaranot { get; set; }
        [Display(Name = "Potongan KDSK")]
        public decimal tunggakanpotonganksdk { get; set; }
        [Display(Name = "1/3 Gaji Pokok")]
        public decimal tunggakangajiper3 { get; set; }
        [Display(Name = "Potongan KSWP")]
        public decimal tunggakanpotongankwsp { get; set; }
        [Display(Name = "SOCSO")]
        public decimal tunggakansocso { get; set; }
        [Display(Name = "Lain-lain Potongan")]
        public decimal tunggakanpotonganlain { get; set; }
        [Display(Name = "Gaji Sebelum Potongan KWSP")]
        public decimal tunggakangajisebelumkwsp { get; set; }
        [Display(Name = "Gaji Kasar Bulan Semasa")]
        public decimal tunggakangajikasar { get; set; }
        [Display(Name = "Gaji Bersih")]
        public decimal tunggakangajibersih { get; set; }
        [Display(Name = "Kelulusan Datuk Bandar")]
        public string kelulusanydptunggakan { get; set; } //values Y, T, P

        public bool IsMuktamad { get; set; }

        //List Sjarah Pembayaran?

        //Methods
        public static PageSejarahModel Insert(PageSejarahModel agree, string user, string command)
        {
            if (agree.bulandibayar != 7 && agree.bulanbekerja != 5)
            {
                return agree;
            }

            ApplicationDbContext db = new ApplicationDbContext();
            //MajlisContext mc = new MajlisContext();
            List<HR_KWSP> listkwsp = db.HR_KWSP.ToList();
            HR_MAKLUMAT_PEKERJAAN mpekerjaan = db.HR_MAKLUMAT_PEKERJAAN.Where(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA).SingleOrDefault();
            List<HR_MAKLUMAT_ELAUN_POTONGAN> maklumatelaun = db.HR_MAKLUMAT_ELAUN_POTONGAN.Where(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA && s.HR_KOD_ELAUN_POTONGAN.Contains("E") && DateTime.Now >= s.HR_TARIKH_MULA && DateTime.Now <= s.HR_TARIKH_AKHIR && s.HR_AKTIF_IND == "Y" && s.HR_KOD_ELAUN_POTONGAN != "E0164").ToList();
            List<HR_MAKLUMAT_ELAUN_POTONGAN> maklumatpotongan1 = db.HR_MAKLUMAT_ELAUN_POTONGAN.Where(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA && s.HR_KOD_ELAUN_POTONGAN.Contains("P") && DateTime.Now >= s.HR_TARIKH_MULA && DateTime.Now <= s.HR_TARIKH_AKHIR && s.HR_AKTIF_IND == "Y" && s.HR_KOD_ELAUN_POTONGAN != "P0015").ToList();
            HR_MAKLUMAT_ELAUN_POTONGAN maklumatpotongan = db.HR_MAKLUMAT_ELAUN_POTONGAN.SingleOrDefault(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA && s.HR_KOD_ELAUN_POTONGAN.Contains("P") && DateTime.Now >= s.HR_TARIKH_MULA && DateTime.Now <= s.HR_TARIKH_AKHIR && s.HR_AKTIF_IND == "Y" && s.HR_KOD_ELAUN_POTONGAN == "P0015");
            List<HR_MAKLUMAT_ELAUN_POTONGAN> maklumatcaruman = db.HR_MAKLUMAT_ELAUN_POTONGAN.Where(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA && s.HR_KOD_ELAUN_POTONGAN.Contains("C") && DateTime.Now >= s.HR_TARIKH_MULA && DateTime.Now <= s.HR_TARIKH_AKHIR && s.HR_AKTIF_IND == "Y" && s.HR_KOD_ELAUN_POTONGAN != "C0020").ToList();
            agree.gajipokok = mpekerjaan.HR_GAJI_POKOK != null ? Convert.ToDecimal(mpekerjaan.HR_GAJI_POKOK) : 0;
            var gajisehari = (mpekerjaan.HR_GAJI_POKOK / 23) * agree.jumlahhari;
            decimal gajipokok = gajisehari != null ? Convert.ToDecimal(gajisehari) : 0;
            gajipokok = Decimal.Round(gajipokok, 2);
            var gajisehariot = (((agree.gajipokok / 23) * agree.jumlahhari) * 12 / 2504);
            var gajisehariot1 = gajisehariot * agree.jumlahot;

            var tbl = db.Users.Where(p => p.Id == user).SingleOrDefault();
            var emel = db.HR_MAKLUMAT_PERIBADI.Where(s => s.HR_NO_KPBARU == tbl.UserName).SingleOrDefault();
            var role1 = db.UserRoles.Where(d => d.UserId == tbl.Id).SingleOrDefault();
            var role = db.Roles.Where(e => e.Id == role1.RoleId).SingleOrDefault();

            switch (command.ToLower())
            {
                case ("hantar"):
                    InsertHantar(db, agree, listkwsp, maklumatelaun, maklumatpotongan, maklumatcaruman, gajipokok, gajisehariot1);
                    TrailLog(emel, role,
                        emel.HR_NAMA_PEKERJA + " Telah menambah data untuk pekerja " + agree.HR_PEKERJA);
                    break;
                case ("kemaskini"):
                    InsertKemaskini(db, agree, gajisehariot1);
                    TrailLog(emel, role,
                        emel.HR_NAMA_PEKERJA + " Telah mengubah data untuk pekerja " + agree.HR_PEKERJA);
                    break;
                case ("muktamad"):
                default:
                    InsertMuktamad(db, agree);
                    TrailLog(emel, role,
                        emel.HR_NAMA_PEKERJA + " Telah mengubah data untuk pekerja " + agree.HR_PEKERJA);
                    break;
            }
            try
            {
                UpdateSambilanDetail(db, agree);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return agree;
        }

        private static void InsertMuktamad(ApplicationDbContext db, PageSejarahModel agree)
        {
            HR_TRANSAKSI_SAMBILAN_DETAIL gaji = db.HR_TRANSAKSI_SAMBILAN_DETAIL
                .Where(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA
                && s.HR_BULAN_BEKERJA == agree.bulanbekerja
                && s.HR_TAHUN_BEKERJA == agree.tahunbekerja
                && s.HR_BULAN_DIBAYAR == agree.bulandibayar
                && s.HR_TAHUN == agree.tahundibayar
                && s.HR_KOD == "GAJPS").SingleOrDefault();
            gaji.HR_MUKTAMAD = 1;

            db.Entry(gaji).State = EntityState.Modified;
            db.SaveChanges();
        }

        private static void InsertKemaskini(ApplicationDbContext db, PageSejarahModel agree,
            decimal gajisehariot1)
        {
            HR_TRANSAKSI_SAMBILAN_DETAIL gaji = db.HR_TRANSAKSI_SAMBILAN_DETAIL
                .Where(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA
                && s.HR_BULAN_BEKERJA == agree.bulanbekerja
                && s.HR_TAHUN_BEKERJA == agree.tahunbekerja
                && s.HR_BULAN_DIBAYAR == agree.bulandibayar
                && s.HR_TAHUN == agree.tahundibayar
                && s.HR_KOD == "GAJPS").SingleOrDefault();
            HR_TRANSAKSI_SAMBILAN_DETAIL ot = db.HR_TRANSAKSI_SAMBILAN_DETAIL
                .Where(s => s.HR_NO_PEKERJA == agree.HR_PEKERJA
                && s.HR_BULAN_BEKERJA == agree.bulanbekerja
                && s.HR_TAHUN_BEKERJA == agree.tahunbekerja
                && s.HR_BULAN_DIBAYAR == agree.bulandibayar
                && s.HR_TAHUN == agree.tahundibayar
                && s.HR_KOD == "E0164").SingleOrDefault();
            if (gaji != null)
            {
                gaji.HR_JUMLAH = agree.gajipokok;
                gaji.HR_JAM_HARI = agree.jumlahhari;
                gaji.HR_YDP_LULUS_IND = agree.kelulusanydptunggakan;
                db.Entry(gaji).State = EntityState.Modified;
            }
            else
            {
                gaji = new HR_TRANSAKSI_SAMBILAN_DETAIL
                {
                    HR_NO_PEKERJA = agree.HR_PEKERJA,
                    HR_BULAN_BEKERJA = agree.bulanbekerja,
                    HR_TAHUN_BEKERJA = agree.tahunbekerja,
                    HR_BULAN_DIBAYAR = agree.bulandibayar,
                    HR_TAHUN = agree.tahundibayar,
                    HR_KOD = "GAJPS",
                    HR_KOD_IND = "G",
                    HR_JUMLAH = agree.gajipokok,
                    HR_JAM_HARI = agree.jumlahhari,
                    HR_YDP_LULUS_IND = agree.kelulusanydptunggakan
                };
                db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(gaji);
            }
            if (ot != null)
            {
                ot.HR_JUMLAH = gajisehariot1;
                ot.HR_JAM_HARI = agree.jumlahot;
                ot.HR_YDP_LULUS_IND = agree.kelulusanydptunggakan;
                db.Entry(ot).State = EntityState.Modified;
            }
            else
            {
                ot = new HR_TRANSAKSI_SAMBILAN_DETAIL
                {
                    HR_NO_PEKERJA = agree.HR_PEKERJA,
                    HR_BULAN_BEKERJA = agree.bulanbekerja,
                    HR_TAHUN_BEKERJA = agree.tahunbekerja,
                    HR_BULAN_DIBAYAR = agree.bulandibayar,
                    HR_TAHUN = agree.tahundibayar,
                    HR_KOD = "E0164",
                    HR_KOD_IND = "E",
                    HR_JUMLAH = gajisehariot1,
                    HR_JAM_HARI = agree.jumlahot,
                    HR_YDP_LULUS_IND = agree.kelulusanydptunggakan
                };
                db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(ot);
            }

            db.SaveChanges();
        }

        private static void TrailLog(HR_MAKLUMAT_PERIBADI emel, IdentityRole role,
            string message)
        {
            try
            {
                System.Web.HttpContext httpContext = System.Web.HttpContext.Current;
                new AuditTrailModels().Log(emel.HR_EMAIL, emel.HR_NAMA_PEKERJA,
                   httpContext.Request.UserHostAddress,
                   role.Name,
                   message,
                   System.Net.Dns.GetHostName(),
                   emel.HR_TELBIMBIT, httpContext.Request.RawUrl, "Sejarah");
            }
            catch
            {

            }

        }

        private static void InsertHantar(ApplicationDbContext db, PageSejarahModel agree,
            List<HR_KWSP> listkwsp,
            List<HR_MAKLUMAT_ELAUN_POTONGAN> maklumatelaun,
            HR_MAKLUMAT_ELAUN_POTONGAN maklumatpotongan,
            List<HR_MAKLUMAT_ELAUN_POTONGAN> maklumatcaruman,
            decimal gajipokok, decimal gajisehariot1)
        {
            foreach (var kwsp in listkwsp)
            {
                if (gajipokok >= kwsp.HR_UPAH_DARI && gajipokok <= kwsp.HR_UPAH_HINGGA)
                {
                    HR_TRANSAKSI_SAMBILAN sambilan = new HR_TRANSAKSI_SAMBILAN
                    {
                        HR_NO_PEKERJA = agree.HR_PEKERJA,
                        HR_BULAN_BEKERJA = agree.bulanbekerja,
                        HR_TAHUN_BEKERJA = agree.tahunbekerja,
                        HR_TAHUN = agree.tahundibayar,
                        HR_BULAN_DIBAYAR = agree.bulandibayar
                    };
                    db.HR_TRANSAKSI_SAMBILAN.Add(sambilan);
                    db.SaveChanges();
                    HR_TRANSAKSI_SAMBILAN_DETAIL majikankwsp = new HR_TRANSAKSI_SAMBILAN_DETAIL
                    {
                        HR_NO_PEKERJA = agree.HR_PEKERJA,
                        HR_BULAN_DIBAYAR = agree.bulandibayar,
                        HR_TAHUN = agree.tahundibayar,
                        HR_KOD = "C0020",
                        HR_BULAN_BEKERJA = agree.bulanbekerja,
                        HR_JUMLAH = kwsp.HR_CARUMAN_MAJIKAN,
                        HR_KOD_IND = "C",
                        HR_TUNGGAKAN_IND = "T",
                        HR_TAHUN_BEKERJA = agree.tahunbekerja,
                        HR_YDP_LULUS_IND = agree.kelulusanydp,
                        HR_MUKTAMAD = 0
                    };
                    db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(majikankwsp);
                    db.SaveChanges();
                    HR_TRANSAKSI_SAMBILAN_DETAIL pekerjakwsp = new HR_TRANSAKSI_SAMBILAN_DETAIL
                    {
                        HR_NO_PEKERJA = agree.HR_PEKERJA,
                        HR_BULAN_DIBAYAR = agree.bulandibayar,
                        HR_TAHUN = agree.tahundibayar,
                        HR_KOD = "P0035",
                        HR_BULAN_BEKERJA = agree.bulanbekerja,
                        HR_JUMLAH = kwsp.HR_CARUMAN_PEKERJA,
                        HR_KOD_IND = "P",
                        HR_TUNGGAKAN_IND = "T",
                        HR_TAHUN_BEKERJA = agree.tahunbekerja,
                        HR_MUKTAMAD = 0
                    };
                    db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(pekerjakwsp);
                    db.SaveChanges();
                    if (maklumatpotongan != null)
                    {
                        HR_TRANSAKSI_SAMBILAN_DETAIL ksdk = new HR_TRANSAKSI_SAMBILAN_DETAIL
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_DIBAYAR = agree.bulandibayar,
                            HR_TAHUN = agree.tahundibayar,
                            HR_KOD = maklumatpotongan.HR_KOD_ELAUN_POTONGAN,
                            HR_BULAN_BEKERJA = agree.bulanbekerja,
                            HR_JUMLAH = maklumatpotongan.HR_JUMLAH,
                            HR_KOD_IND = maklumatpotongan.HR_ELAUN_POTONGAN_IND,
                            HR_TUNGGAKAN_IND = "T",
                            HR_YDP_LULUS_IND = agree.kelulusanydp,
                            HR_TAHUN_BEKERJA = agree.tahunbekerja,
                            HR_MUKTAMAD = 0
                        };
                        db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(ksdk);
                        db.SaveChanges();
                    }
                    HR_TRANSAKSI_SAMBILAN_DETAIL elaunot = new HR_TRANSAKSI_SAMBILAN_DETAIL
                    {
                        HR_NO_PEKERJA = agree.HR_PEKERJA,
                        HR_BULAN_DIBAYAR = agree.bulandibayar,
                        HR_TAHUN = agree.tahundibayar,
                        HR_KOD = "E0164",
                        HR_BULAN_BEKERJA = agree.bulanbekerja,
                        HR_JUMLAH = gajisehariot1,
                        HR_KOD_IND = "E",
                        HR_TUNGGAKAN_IND = "T",
                        HR_JAM_HARI = agree.jumlahot,
                        HR_YDP_LULUS_IND = agree.kelulusanydp,
                        HR_TAHUN_BEKERJA = agree.tahunbekerja,
                        HR_MUKTAMAD = 0
                    };
                    db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(elaunot);
                    db.SaveChanges();
                    HR_TRANSAKSI_SAMBILAN_DETAIL gajipekerja = new HR_TRANSAKSI_SAMBILAN_DETAIL
                    {
                        HR_NO_PEKERJA = agree.HR_PEKERJA,
                        HR_BULAN_DIBAYAR = agree.bulandibayar,
                        HR_TAHUN = agree.tahundibayar,
                        HR_KOD = "GAJPS",
                        HR_BULAN_BEKERJA = agree.bulanbekerja,
                        HR_JUMLAH = agree.gajipokok,
                        HR_KOD_IND = "G",
                        HR_JAM_HARI = agree.jumlahhari,
                        HR_TUNGGAKAN_IND = "T",
                        HR_TAHUN_BEKERJA = agree.tahunbekerja,
                        HR_MUKTAMAD = 0
                    };
                    db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(gajipekerja);
                    db.SaveChanges();
                    foreach (var sum in maklumatelaun)
                    {
                        HR_TRANSAKSI_SAMBILAN_DETAIL elaunlain = new HR_TRANSAKSI_SAMBILAN_DETAIL
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_DIBAYAR = agree.bulandibayar,
                            HR_TAHUN = agree.tahundibayar,
                            HR_KOD = sum.HR_KOD_ELAUN_POTONGAN,
                            HR_BULAN_BEKERJA = agree.bulanbekerja,
                            HR_JUMLAH = sum.HR_JUMLAH,
                            HR_KOD_IND = sum.HR_ELAUN_POTONGAN_IND,
                            HR_TUNGGAKAN_IND = "T",
                            HR_TAHUN_BEKERJA = agree.tahunbekerja,
                            HR_MUKTAMAD = 0
                        };
                        db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(elaunlain);
                        db.SaveChanges();
                    }
                    foreach (var sum2 in maklumatcaruman)
                    {
                        HR_TRANSAKSI_SAMBILAN_DETAIL potonganlain = new HR_TRANSAKSI_SAMBILAN_DETAIL
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_DIBAYAR = agree.bulandibayar,
                            HR_TAHUN = agree.tahundibayar,
                            HR_KOD = sum2.HR_KOD_ELAUN_POTONGAN,
                            HR_BULAN_BEKERJA = agree.bulanbekerja,
                            HR_JUMLAH = sum2.HR_JUMLAH,
                            HR_KOD_IND = sum2.HR_ELAUN_POTONGAN_IND,
                            HR_TUNGGAKAN_IND = "T",
                            HR_TAHUN_BEKERJA = agree.tahunbekerja,
                            HR_MUKTAMAD = 0
                        };
                        db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(potonganlain);
                        db.SaveChanges();
                    }
                }
            }

            if (agree.tunggakan == "Y")
            {
                foreach (var kwsp in listkwsp)
                {
                    if (gajipokok >= kwsp.HR_UPAH_DARI && gajipokok <= kwsp.HR_UPAH_HINGGA)
                    {
                        HR_TRANSAKSI_SAMBILAN sambilan = new HR_TRANSAKSI_SAMBILAN
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_BEKERJA = agree.tunggakanbulanbekerja,
                            HR_TAHUN_BEKERJA = agree.tunggakantahunbekerja,
                            HR_TAHUN = agree.tunggakantahundibayar,
                            HR_BULAN_DIBAYAR = agree.tunggakantahundibayar
                        };
                        db.HR_TRANSAKSI_SAMBILAN.Add(sambilan);
                        db.SaveChanges();
                        HR_TRANSAKSI_SAMBILAN_DETAIL majikankwsp = new HR_TRANSAKSI_SAMBILAN_DETAIL
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_DIBAYAR = agree.tunggakanbulandibayar,
                            HR_TAHUN = agree.tunggakantahundibayar,
                            HR_KOD = "C0020",
                            HR_BULAN_BEKERJA = agree.tunggakanbulanbekerja,
                            HR_JUMLAH = kwsp.HR_CARUMAN_MAJIKAN,
                            HR_KOD_IND = "C",
                            HR_TUNGGAKAN_IND = "Y",
                            HR_TAHUN_BEKERJA = agree.tunggakantahunbekerja,
                            HR_MUKTAMAD = 0
                        };
                        db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(majikankwsp);
                        db.SaveChanges();
                        HR_TRANSAKSI_SAMBILAN_DETAIL pekerjakwsp = new HR_TRANSAKSI_SAMBILAN_DETAIL
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_DIBAYAR = agree.tunggakanbulandibayar,
                            HR_TAHUN = agree.tunggakantahundibayar,
                            HR_KOD = "P0035",
                            HR_BULAN_BEKERJA = agree.tunggakanbulanbekerja,
                            HR_JUMLAH = kwsp.HR_CARUMAN_PEKERJA,
                            HR_KOD_IND = "P",
                            HR_TUNGGAKAN_IND = "Y",
                            HR_TAHUN_BEKERJA = agree.tunggakantahunbekerja,
                            HR_MUKTAMAD = 0
                        };
                        db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(pekerjakwsp);
                        db.SaveChanges();
                        if (maklumatpotongan != null)
                        {
                            HR_TRANSAKSI_SAMBILAN_DETAIL ksdk = new HR_TRANSAKSI_SAMBILAN_DETAIL
                            {
                                HR_NO_PEKERJA = agree.HR_PEKERJA,
                                HR_BULAN_DIBAYAR = agree.tunggakanbulandibayar,
                                HR_TAHUN = agree.tunggakantahundibayar,
                                HR_KOD = maklumatpotongan.HR_KOD_ELAUN_POTONGAN,
                                HR_BULAN_BEKERJA = agree.tunggakanbulanbekerja,
                                HR_JUMLAH = maklumatpotongan.HR_JUMLAH,
                                HR_KOD_IND = maklumatpotongan.HR_ELAUN_POTONGAN_IND,
                                HR_TUNGGAKAN_IND = "Y",
                                HR_TAHUN_BEKERJA = agree.tahunbekerja,
                                HR_MUKTAMAD = 0
                            };
                            db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(ksdk);
                            db.SaveChanges();
                        }
                        HR_TRANSAKSI_SAMBILAN_DETAIL elaunot = new HR_TRANSAKSI_SAMBILAN_DETAIL
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_DIBAYAR = agree.tunggakanbulandibayar,
                            HR_TAHUN = agree.tunggakantahundibayar,
                            HR_KOD = "E0164",
                            HR_BULAN_BEKERJA = agree.tunggakanbulanbekerja,
                            HR_JUMLAH = gajisehariot1,
                            HR_KOD_IND = "E",
                            HR_TUNGGAKAN_IND = "Y",
                            HR_JAM_HARI = agree.tunggakanjumlahot,
                            HR_YDP_LULUS_IND = agree.kelulusanydptunggakan,
                            HR_TAHUN_BEKERJA = agree.tunggakantahunbekerja,
                            HR_MUKTAMAD = 0
                        };
                        db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(elaunot);
                        db.SaveChanges();
                        HR_TRANSAKSI_SAMBILAN_DETAIL gajipekerja = new HR_TRANSAKSI_SAMBILAN_DETAIL
                        {
                            HR_NO_PEKERJA = agree.HR_PEKERJA,
                            HR_BULAN_DIBAYAR = agree.tunggakanbulandibayar,
                            HR_TAHUN = agree.tunggakantahundibayar,
                            HR_KOD = "GAJPS",
                            HR_BULAN_BEKERJA = agree.tunggakanbulanbekerja,
                            HR_JUMLAH = gajipokok,
                            HR_KOD_IND = "G",
                            HR_JAM_HARI = agree.tunggakanjumlahhari,
                            HR_TUNGGAKAN_IND = "Y",
                            HR_TAHUN_BEKERJA = agree.tunggakantahunbekerja,
                            HR_MUKTAMAD = 0
                        };
                        db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(gajipekerja);
                        db.SaveChanges();
                        foreach (var sum in maklumatelaun)
                        {
                            HR_TRANSAKSI_SAMBILAN_DETAIL elaunlain = new HR_TRANSAKSI_SAMBILAN_DETAIL
                            {
                                HR_NO_PEKERJA = agree.HR_PEKERJA,
                                HR_BULAN_DIBAYAR = agree.tunggakanbulandibayar,
                                HR_TAHUN = agree.tahundibayar,
                                HR_KOD = sum.HR_KOD_ELAUN_POTONGAN,
                                HR_BULAN_BEKERJA = agree.bulanbekerja,
                                HR_JUMLAH = sum.HR_JUMLAH,
                                HR_KOD_IND = sum.HR_ELAUN_POTONGAN_IND,
                                HR_TUNGGAKAN_IND = "Y",
                                HR_TAHUN_BEKERJA = agree.tunggakantahunbekerja,
                                HR_MUKTAMAD = 0
                            };
                            db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(elaunlain);
                            db.SaveChanges();
                        }
                        foreach (var sum2 in maklumatcaruman)
                        {
                            HR_TRANSAKSI_SAMBILAN_DETAIL potonganlain = new HR_TRANSAKSI_SAMBILAN_DETAIL
                            {
                                HR_NO_PEKERJA = agree.HR_PEKERJA,
                                HR_BULAN_DIBAYAR = agree.tunggakanbulandibayar,
                                HR_TAHUN = agree.tunggakantahundibayar,
                                HR_KOD = sum2.HR_KOD_ELAUN_POTONGAN,
                                HR_BULAN_BEKERJA = agree.tunggakanbulanbekerja,
                                HR_JUMLAH = sum2.HR_JUMLAH,
                                HR_KOD_IND = sum2.HR_ELAUN_POTONGAN_IND,
                                HR_TUNGGAKAN_IND = "Y",
                                HR_TAHUN_BEKERJA = agree.tunggakantahunbekerja,
                                HR_MUKTAMAD = 0
                            };
                            db.HR_TRANSAKSI_SAMBILAN_DETAIL.Add(potonganlain);
                            db.SaveChanges();
                        }
                    }
                }
            }


        }

        private static void UpdateSambilanDetail(ApplicationDbContext db, PageSejarahModel agree)
        {
            bool isNew = false;
            HR_BONUS_SAMBILAN_DETAIL det = db.HR_BONUS_SAMBILAN_DETAIL
                .Where(x => x.HR_NO_PEKERJA == agree.HR_PEKERJA
                && x.HR_TAHUN_BONUS == agree.tahundibayar
                && x.HR_BULAN_BONUS == agree.bulandibayar).FirstOrDefault();
            if (det == null)
            {
                isNew = true;
                //insert det
                det = new HR_BONUS_SAMBILAN_DETAIL();
                det.HR_NO_PEKERJA = agree.HR_PEKERJA;
                det.HR_TAHUN_BONUS = agree.tahundibayar;
                det.HR_BULAN_BONUS = agree.bulandibayar;
                det.HR_NO_KPBARU = db.HR_MAKLUMAT_PERIBADI
                    .Where(p => p.HR_NO_PEKERJA == agree.HR_PEKERJA)
                    .Select(p => p.HR_NO_KPBARU).FirstOrDefault();
            }

            det = UpdateBonusSambilanInfo(db, det, agree);

            if (isNew)
            {
                db.HR_BONUS_SAMBILAN_DETAIL.Add(det);
                db.SaveChanges();
            }
            else
            {
                //update det
                db.Entry(det).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        private static HR_BONUS_SAMBILAN_DETAIL UpdateBonusSambilanInfo
                (ApplicationDbContext db, HR_BONUS_SAMBILAN_DETAIL det, PageSejarahModel agree)
        {
            List<HR_TRANSAKSI_SAMBILAN_DETAIL> elaunlain =
               db.HR_TRANSAKSI_SAMBILAN_DETAIL.
               Where(x => x.HR_NO_PEKERJA == agree.HR_PEKERJA
               && x.HR_TAHUN == agree.tahundibayar
               && x.HR_BULAN_DIBAYAR == agree.bulandibayar).ToList();

            det.HR_JANUARI = elaunlain
                    .Where(c => c.HR_BULAN_BEKERJA == 1).Sum(c => c.HR_JUMLAH);
            det.HR_FEBRUARI = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 2).Sum(c => c.HR_JUMLAH);
            det.HR_MAC = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 3).Sum(c => c.HR_JUMLAH);
            det.HR_APRIL = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 4).Sum(c => c.HR_JUMLAH);
            det.HR_MEI = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 5).Sum(c => c.HR_JUMLAH);
            det.HR_JUN = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 6).Sum(c => c.HR_JUMLAH);
            det.HR_JULAI = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 7).Sum(c => c.HR_JUMLAH);
            det.HR_OGOS = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 8).Sum(c => c.HR_JUMLAH);
            det.HR_SEPTEMBER = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 9).Sum(c => c.HR_JUMLAH);
            det.HR_OKTOBER = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 10).Sum(c => c.HR_JUMLAH);
            det.HR_NOVEMBER = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 11).Sum(c => c.HR_JUMLAH);
            det.HR_DISEMBER = elaunlain
                .Where(c => c.HR_BULAN_BEKERJA == 12).Sum(c => c.HR_JUMLAH);
            det.HR_JUMLAH_GAJI = elaunlain.Sum(c => c.HR_JUMLAH);
            int totalBulan = elaunlain.Select(c => c.HR_BULAN_BEKERJA).Distinct().Count();
            if (totalBulan > 0)
            {
                det.HR_GAJI_PURATA = det.HR_JUMLAH_GAJI == null ?
                    0 : decimal.Round(Convert.ToDecimal(det.HR_JUMLAH_GAJI) / totalBulan, 3);
            }
            det.HR_MUKTAMAD = 0;

            return det;
        }
    }
}