using System;
using PagedList;
using System.Web.Mvc;
using Framework.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Framework.WebUI.Models.ViewModels
{
    public class LogsVM : PagingVMBase
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }

        [Display(Name = "Tür")]
        public string LogType { get; set; }

        [Display(Name = "Detay")]
        public string Detail { get; set; }

        [Display(Name = "Metod Adı")]
        public string ActionName { get; set; }

        [Display(Name = "Sayfa Adı")]
        public string ControllerName { get; set; }

        [Display(Name = "Url")]
        public string Url { get; set; }
       
        [Display(Name = "Url")]
        public string HttpType { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Açıklama")]
        public string Message { get; set; }


        [Display(Name = "Hata Açıklama")]
        public string StackTrace { get; set; }

        [Display(Name = "Tarih")]
        public string Date { get; set; }

        public List<LogsVM> JsonDeserializeLog { get; set; }
    }
}