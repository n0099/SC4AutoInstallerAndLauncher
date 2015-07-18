Public Class InstalledModule

    Public Enum Language
        SimplifiedChinese
        TraditionalChinese
        English
    End Enum
    Public SC4InstallDir As String
    Public LanguagePatch As Language
    Public Is638PatchInstalled As Boolean
    Public Is640PatchInstalled As Boolean
    Public Is641PatchInstalled As Boolean
    Public Is4GBPatchInstalled As Boolean
    Public IsNoCDPatchInstalled As Boolean
    Public IsSC4LauncherInstalled As Boolean

End Class