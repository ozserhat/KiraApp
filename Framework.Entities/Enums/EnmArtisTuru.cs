using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.Enums
{
    public enum EnmArtisTuru
    {
        [Display(Name = "Tüfe")]
        [Description("Tüfe")]
        Tufe = 0,
        [Description("Üfe")]
        [Display(Name = "Üfe")]
        Ufe = 1,
    }
}
