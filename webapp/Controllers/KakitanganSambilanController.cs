﻿using eSPP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Data.Entity;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using iTextSharp.tool.xml;
using Oracle.ManagedDataAccess.Client;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Core.Objects;
using Oracle.ManagedDataAccess.Types;
using Microsoft.Office.Interop.Word;
using ClosedXML.Excel;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using eSPP.App_Helpers.ExcelHelper;

namespace eSPP.Controllers
{
    public class KakitanganSambilanController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filtercontext)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var GE_PEKERJA = db.HR_MAKLUMAT_PERIBADI.Include(s => s.HR_MAKLUMAT_PEKERJAAN)
              .Where(s => s.HR_MAKLUMAT_PEKERJAAN.HR_GAJI_IND == "Y"
              && s.HR_AKTIF_IND == ("Y")
              && (s.HR_MAKLUMAT_PEKERJAAN.HR_TARAF_JAWATAN.Contains("N") ||
              s.HR_MAKLUMAT_PEKERJAAN.HR_TARAF_JAWATAN.Contains("A"))).ToList();
            var PEKERJA = db.HR_MAKLUMAT_PERIBADI.ToList();
            ViewBag.TUNGGAKAN = db.HR_MAKLUMAT_ELAUN_POTONGAN.ToList();
            ViewBag.ELAUN = db.HR_ELAUN.ToList();
            ViewBag.POTONGAN = db.HR_POTONGAN.ToList();
            ViewBag.CARUMAN = db.HR_CARUMAN.ToList();
            ViewBag.GAJI = db.HR_GAJI_UPAHAN.ToList();
            ViewBag.PEKERJA = PEKERJA;
            ViewBag.HR_PEKERJA = GE_PEKERJA; //ini dropdown
        }

        public enum ManageMessageId
        {
            Tambah,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            ResetPassword,
            Kemaskini,
            KemaskiniTunggakan,
            BayarTunggakan,
            TambahBonus,
            Error,
            Muktamad
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Sejarah(ManageMessageId? message,
            string HR_PEKERJA, string tahunbekerja, string bulanbekerja, string tahundibayar, string bulandibayar)
        {
            ViewBag.StatusMessage =
              message == ManageMessageId.Tambah ? "Permohonan Telah Berjaya Disimpan."
              : message == ManageMessageId.Muktamad ? "Permohonan Anda Telah Muktamad."
              : message == ManageMessageId.Kemaskini ? "Permohonan Telah Berjaya Dikemaskini."
              : message == ManageMessageId.BayarTunggakan ? "Permohonan Telah Berjaya Dikemaskini."
              : "";

            PageSejarahModel page = new PageSejarahModel();
            if (!string.IsNullOrEmpty(HR_PEKERJA))
            {
                page = new PageSejarahModel(HR_PEKERJA, tahunbekerja, bulanbekerja,
                          tahundibayar, bulandibayar);
            }

            List<SelectListItem> ddlbulan = new List<SelectListItem>
            {
                new SelectListItem { Text = "JANUARI", Value = "1" },
                new SelectListItem { Text = "FEBRUARI", Value = "2" },
                new SelectListItem { Text = "MAC", Value = "3" },
                new SelectListItem { Text = "APRIL", Value = "4" },
                new SelectListItem { Text = "MAY", Value = "5" },
                new SelectListItem { Text = "JUN", Value = "6" },
                new SelectListItem { Text = "JULAI", Value = "7" },
                new SelectListItem { Text = "OGOS", Value = "8" },
                new SelectListItem { Text = "SEPTEMBER", Value = "9" },
                new SelectListItem { Text = "OKTOBER", Value = "10" },
                new SelectListItem { Text = "NOVEMBER", Value = "11" },
                new SelectListItem { Text = "DISEMBER", Value = "12" }
            };
            ViewBag.bulandibayar =
                new SelectList(ddlbulan, "Value", "Text", page.bulandibayar);
            ViewBag.tunggakanbulandibayar = ViewBag.bulandibayar;
            ViewBag.bulanbekerja =
                new SelectList(ddlbulan, "Value", "Text", page.bulanbekerja);
            ViewBag.tunggakanbulanbekerja =
                new SelectList(ddlbulan, "Value", "Text", page.tunggakanbulanbekerja);

            return View(page);
        }

        [HttpPost]
        public ActionResult Sejarah(PageSejarahModel page, string Command)
        {
            var user = User.Identity.GetUserId();
            PageSejarahModel.Insert(page, user, Command);
            string info = string.Empty;
            if (Command == "Hantar")
            {
                info = ManageMessageId.Tambah.ToString();
            }
            else if (Command == "Kemaskini")
            {
                info = ManageMessageId.Kemaskini.ToString();
            }
            else //Muktamad
            {
                info = ManageMessageId.Muktamad.ToString();
            }
            return RedirectToAction("Sejarah",
                new
                {
                    message = info,
                    page.HR_PEKERJA,
                    page.tahunbekerja,
                    page.tahundibayar,
                    page.bulandibayar,
                    page.bulanbekerja
                });
        }

        public ActionResult BonusSambilanDetail(string month, string year, ManageMessageId? message)
        {
            ViewBag.StatusMessage =
              message == ManageMessageId.Tambah ? "Data Telah Berjaya Disimpan."
              : message == ManageMessageId.Muktamad ? "Data Anda Telah Muktamad."
              : message == ManageMessageId.Kemaskini ? "Data Telah Berjaya Dikemaskini."
              : "";

            List<BonusSambilanDetailModel> list = new List<BonusSambilanDetailModel>();
            try
            {
                int monthInt = Convert.ToInt32(month);
                int yearInt = Convert.ToInt32(year);
                list = BonusSambilanDetailModel.GetBonusSambilanDetailData(monthInt, yearInt);
            }
            catch
            {

            }

            return View(list);
        }

        [HttpPost]
        public ActionResult UpdateBonus(string bonusDiterima, string bulanBonus, string tahunBonus, string noPekerja)
        {
            ManageMessageId outputMsg;
            try
            {
                int month = Convert.ToInt32(bulanBonus);
                int year = Convert.ToInt32(tahunBonus);
                decimal bonusDiterima_dec = Convert.ToDecimal(bonusDiterima);
                HR_BONUS_SAMBILAN_DETAIL.UpdateBonusDiterima(month, year, noPekerja, bonusDiterima_dec);
                outputMsg = ManageMessageId.Kemaskini;
            }
            catch
            {
                outputMsg = ManageMessageId.Error;
            }
            
            return RedirectToAction("BonusSambilanDetail", new { month = bulanBonus, year = tahunBonus, message = outputMsg });
        }

        [HttpPost]
        public ActionResult UpdateCatatan(string catatan, string bulanBonus, string tahunBonus, string noPekerja)
        {
            ManageMessageId outputMsg;
            try
            {
                int month = Convert.ToInt32(bulanBonus);
                int year = Convert.ToInt32(tahunBonus);
                HR_BONUS_SAMBILAN_DETAIL.UpdateCatatan(month, year, noPekerja, catatan);
                outputMsg = ManageMessageId.Kemaskini;
            }
            catch
            {
                outputMsg = ManageMessageId.Error;
            }

            return RedirectToAction("BonusSambilanDetail", new { month = bulanBonus, year = tahunBonus, message = outputMsg });
        }
    }
}