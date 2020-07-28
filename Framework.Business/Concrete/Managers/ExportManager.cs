using Framework.Business.Abstract;
using Framework.DataAccess.Abstract;
using Framework.Entities.Concrete;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Business.Concrete.Managers
{
    public class ExportManager: IExportService
    {
        private IKira_BeyanDal _beyanDal;

        public ExportManager(IKira_BeyanDal beyanDal)
        {
            _beyanDal = beyanDal;
        }

        public byte[] ExcelExportBeyan(KiraBeyanRequest request)
        {
            
            var result = _beyanDal.GetListByCriteriasActive(request);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            ExcelPackage excel = new ExcelPackage();
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");
            workSheet.TabColor = System.Drawing.Color.Black;
            workSheet.DefaultRowHeight = 12;
            //Header of table  
            //  
            int recordIndex = 1;
            workSheet.Row(recordIndex).Height = 20;
            workSheet.Row(recordIndex).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(recordIndex).Style.Font.Bold = true;

            int ind = 1;

            if (result != null)
            {
                workSheet.Cells[recordIndex, ind++].Value = "Tür Adı";
                workSheet.Cells[recordIndex, ind++].Value = "Beyan Yıl";
                workSheet.Cells[recordIndex, ind++].Value = "Beyan No";
                workSheet.Cells[recordIndex, ind++].Value = "Sicil No";
                workSheet.Cells[recordIndex, ind++].Value = "Kiracı Bilgi";
                workSheet.Cells[recordIndex, ind++].Value = "Gayrimenkul No";
                workSheet.Cells[recordIndex, ind++].Value = "Encümen Karar No";
                workSheet.Cells[recordIndex, ind++].Value = "Noter Sözleşme No";
                workSheet.Cells[recordIndex, ind++].Value = "Teminat No";
                workSheet.Cells[recordIndex, ind++].Value = "İhale Tutarı";
                workSheet.Cells[recordIndex, ind++].Value = "Kira Tutarı";
                workSheet.Cells[recordIndex, ind++].Value = "Damga Alınsın Mı?";
                workSheet.Cells[recordIndex, ind++].Value = "Damga Karar Artış Türü";
                workSheet.Cells[recordIndex, ind++].Value = "Teminat Artış Türü";
                workSheet.Cells[recordIndex, ind++].Value = "Kdv";
                workSheet.Cells[recordIndex, ind++].Value = "Beyan Tarihi";
                workSheet.Cells[recordIndex, ind++].Value = "Kira Başlangıç Tarihi";
                workSheet.Cells[recordIndex, ind++].Value = "Beyan Kapatma Tarihi";
                workSheet.Cells[recordIndex, ind++].Value = "İhale Encümen Tarihi";
                workSheet.Cells[recordIndex, ind++].Value = "Sözleşme Tarihi";
                workSheet.Cells[recordIndex, ind++].Value = "Sözleşme Bitiş Tarihi";
                workSheet.Cells[recordIndex, ind++].Value = "Teminat Tarihi";
                workSheet.Cells[recordIndex, ind++].Value = "Il";
                workSheet.Cells[recordIndex, ind++].Value = "Ilçe";
                workSheet.Cells[recordIndex, ind++].Value = "Mahalle";
                workSheet.Cells[recordIndex, ind++].Value = "Kira Durumu";
                workSheet.Cells[recordIndex, ind++].Value = "Ödeme Periyot Türü";

                workSheet.Cells[recordIndex, ind++].Value = "Artış Mı";
                recordIndex = 2;
                foreach (var beyan in result)
                {

                    ind = 1;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.BeyanTur!=null?beyan.BeyanTur.Ad:"-";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.BeyanYil.HasValue?beyan.BeyanYil:null;
                    workSheet.Cells[recordIndex, ind++].Value = !string.IsNullOrEmpty(beyan.BeyanNo)?beyan.BeyanNo:"-";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.SicilNo.HasValue?beyan.SicilNo:null;
                    workSheet.Cells[recordIndex, ind++].Value = !string.IsNullOrEmpty(beyan.Kiracilar.Ad )?beyan.Kiracilar.Ad +" "+beyan.Kiracilar.Soyad:"-";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.Gayrimenkuller!=null?beyan.Gayrimenkuller.GayrimenkulNo:"-";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.EncumenKararNo;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.NoterSozlesmeNo;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.TeminatNo;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.IhaleTutari;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.KiraTutari;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.DamgaAlinsinMi == true ? "Evet" : "Hayır";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.DamgaKararArtisTuru == 1 ? "Üfe Oranı" : "Tüfe Oranı";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.TeminatArtisTuru == 1 ? "Tam Al" : "Fark Al";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.Kdv;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.BeyanTarihi;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.KiraBaslangicTarihi;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.BeyanKapatmaTarihi;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.IhaleEncumenTarihi;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.SozlesmeTarihi;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.SozlesmeBitisTarihi;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.TeminatTarihi;
                    workSheet.Cells[recordIndex, ind++].Value = beyan.Gayrimenkuller != null ? beyan.Gayrimenkuller.Iller.Ad : "-";

                   if (beyan.Gayrimenkuller!=null && beyan.Gayrimenkuller.Ilce_Id!=null)
                        workSheet.Cells[recordIndex, ind++].Value = beyan.Gayrimenkuller.Ilceler.Ad;
                    else
                        workSheet.Cells[recordIndex, ind++].Value = "-";

                    if (beyan.Gayrimenkuller != null && beyan.Gayrimenkuller.Mahalle_Id != null)
                        workSheet.Cells[recordIndex, ind++].Value = beyan.Gayrimenkuller.Mahalleler.Ad;
                    else
                        workSheet.Cells[recordIndex, ind++].Value = "-";

                    workSheet.Cells[recordIndex, ind++].Value = beyan.KiraDurum != null ? beyan.KiraDurum.Ad : "-";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.OdemePeriyotTur != null ? beyan.OdemePeriyotTur.Ad : "-";
                    workSheet.Cells[recordIndex, ind++].Value = beyan.OncekiBeyanId!=null?"Evet" : "Hayır";
                    recordIndex++;
                }

            }


            var memoryStream = new MemoryStream();
            excel.SaveAs(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
