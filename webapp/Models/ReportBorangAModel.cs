using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eSPP.Models
{
    public class PekerjaReportModel
    {
        public int Bil { get; set; }
        public string NoKadPengenalan { get; set; }
        public string NoKesSosial { get; set; }
        public string NamaPekerja { get; set; }
        private decimal _carumanRM;
        public decimal CarumanRM
        {
            get
            {
                try
                {
                    return Decimal.Round(_carumanRM, 2);
                }
                catch
                {
                    return 0;
                }
            }
            set
            {
                _carumanRM = value;
            }
        }
    }

    public class ReportBorangAModel
    {
        public ReportBorangAModel()
        {
            PekerjaSambilan = new List<PekerjaReportModel>();
        }

        public List<PekerjaReportModel> PekerjaSambilan { get; set; }

        public static ReportBorangAModel GetReport(int bulan, int tahun, string jenisLaporan)
        {
            ApplicationDbContext db = new ApplicationDbContext();

            List<HR_TRANSAKSI_SAMBILAN_DETAIL> transaksisambilandetail 
                = db.HR_TRANSAKSI_SAMBILAN_DETAIL.AsEnumerable()
                    .Where(s => s.HR_BULAN_DIBAYAR == bulan 
                    && s.HR_TAHUN == tahun 
                    && s.HR_KOD == "C0020").ToList();
            decimal? caruman = db.HR_CARUMAN
                .Where(c => c.HR_KOD_CARUMAN == "C0020").Select(c => c.HR_PERATUS).First();

            List<PekerjaReportModel> pekerja = new List<PekerjaReportModel>();

            int counter = 0;
            foreach (HR_TRANSAKSI_SAMBILAN_DETAIL item in transaksisambilandetail)
            {
                HR_MAKLUMAT_PERIBADI maklumatPeribat = db.HR_MAKLUMAT_PERIBADI
                    .Where(s => s.HR_NO_PEKERJA == item.HR_NO_PEKERJA).FirstOrDefault();

                if(maklumatPeribat != null)
                {
                    PekerjaReportModel m = new PekerjaReportModel();
                    counter++;
                    m.Bil = counter;
                    m.NamaPekerja = maklumatPeribat.HR_NAMA_PEKERJA;
                    m.NoKesSosial = string.Empty;
                    m.NoKadPengenalan = maklumatPeribat.HR_NO_KPBARU;
                    try
                    {
                        decimal totalVal = Convert.ToDecimal(item.HR_JUMLAH); //* Convert.ToDecimal(caruman);
                        totalVal = decimal.Round(totalVal);
                        m.CarumanRM = totalVal;
                    }
                    catch
                    {
                        m.CarumanRM = 0;
                    }
                    pekerja.Add(m);
                }
            }

            ReportBorangAModel reportData = new ReportBorangAModel();
            reportData.PekerjaSambilan = pekerja;

            return reportData;
        }
    }    
}

/*
 * #region get Elaun & Caruman
    HR_ELAUN elaun1 = db.HR_ELAUN.SingleOrDefault(s => s.HR_KOD_ELAUN == item.HR_KOD);
    if (elaun1 != null)
    {
        maklumatelaunpotongan = elaun1.HR_PENERANGAN_ELAUN;
    }
    HR_POTONGAN potongan1 = db.HR_POTONGAN.SingleOrDefault(s => s.HR_KOD_POTONGAN == item.HR_KOD);
    if (potongan1 != null)
    {
        maklumatelaunpotongan = potongan1.HR_PENERANGAN_POTONGAN;
    }
    HR_CARUMAN caruman1 = db.HR_CARUMAN.SingleOrDefault(s => s.HR_KOD_CARUMAN == item.HR_KOD);
    if (caruman1 != null)
    {
        maklumatelaunpotongan = caruman1.HR_PENERANGAN_CARUMAN;
    }
    HR_GAJI_UPAHAN gaji1 = db.HR_GAJI_UPAHAN.SingleOrDefault(s => s.HR_KOD_UPAH == item.HR_KOD);
    if (gaji1 != null)
    {
        maklumatelaunpotongan = gaji1.HR_PENERANGAN_UPAH;
    }
#endregion
 */
