using System.ComponentModel;

namespace Topicos.Models.Enums
{
    public enum CategoriaProduto
    {
        [Description("Weiss")]
        Weiss = 0,
        [Description("IPA")]
        Ipa = 1,
        [Description("APA")]
        Apa = 2,
        [Description("Pilsen")]
        Pilsen = 3,
        [Description("Lager")]
        Lager = 4
    }
}
