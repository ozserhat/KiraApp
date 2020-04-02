using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Framework.WebUI.Models
{
    public static class VMErrors
    {
        public const string NotFound = "İstenen veri bulunamadı";

        public const string UnAuthorized = "Veriye erişim izniniz yok";

        public const string ValidationError = "İstenen bilgileri giriniz";

        public const string GeneralError = "İşlem sırasında hata oluştu";

        public const string UserError = "Eklemek istediğiniz kullanıcı sistemde kayıtlı";
    }

    public abstract class VMBase
    {
        public VMBase()
        {
            this.Errors = new List<string>();
        }
        public bool HideContent { get; set; }

        public List<string> Errors { get; set; }
    }

    public abstract class PagingVMBase : VMBase
    {
        public int PageNumber { get; set; }

        public int TotalRecordCount { get; set; }

        public int PageSize { get; set; }
    }
}