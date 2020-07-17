using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Entities.Enums
{
    public enum EnmIslemDurumu
    {
        [Description("Pasif")]
        Pasif = 0,
        [Description("Aktif")]
        Aktif = 1,
        [Description("Silindi")]
        Silindi = 2,
        [Description("Kapandı")]
        Kapandı = 3,
        [Description("Belirsiz")]
        Belirsiz = 4
    }
}
