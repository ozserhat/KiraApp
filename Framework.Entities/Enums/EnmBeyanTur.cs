using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Framework.Entities.Enums
{
    public enum EnmBeyanTur
    {
        [Display(Name = "Kira")]
        [Description("Kira")]
        Kira = 1,

        [Description("Otopark")]
        [Display(Name = "Otopark")]
        Otopark = 2,

        [Description("Ecrimisil")]
        [Display(Name = "Ecrimisil")]
        Ecrimisil = 3,
    }
}
