Public Class InstallOptions

    Public Enum SC4InstallType
        ISO
        NoInstall
    End Enum
    Public Enum Language
        SimplifiedChinese
        TraditionalChinese
        English
    End Enum
    Public Const SC4NeedsDiskSpace As Integer = 1029863647
    Public Const DAEMONNeedsDiskSpace As Integer = 50226791
    Public Const _638NeedsDiskSpace As Integer = -535428
    Public Const _640NeedsDiskSpace As Integer = 41015
    Public Const NoCDNeedsDiskSpace As Integer = -883162
    Public Const SC4LauncherNeedsDiskSpace As Integer = 90112
    Public Const LanguageSimplifiedChineseNeedsDiskSpace As Integer = 643284
    Public SC4InstallDir As String
    Public DAEMONInstallDir As String
    Public LanguagePatch As Language
    Public SC4Type As SC4InstallType
    Public IsInstallDAEMONTools As Boolean
    Public IsInstall638Patch As Boolean
    Public IsInstall640Patch As Boolean
    Public IsInstall4GBPatch As Boolean
    Public IsInstallNoCDPatch As Boolean
    Public IsInstallSC4Launcher As Boolean
    Public IsAddDesktopIcon As Boolean
    Public IsAddStartMenuItem As Boolean

    Public Function GetNeedsDiskSpace(ByVal unit As String) As Decimal
        Dim ReturnValue As Decimal = 0
        If SC4Type = SC4InstallType.ISO Or SC4Type = SC4InstallType.NoInstall Then ReturnValue += SC4NeedsDiskSpace
        If IsInstall638Patch = True Then ReturnValue += _638NeedsDiskSpace
        If IsInstall640Patch = True Then ReturnValue += _640NeedsDiskSpace
        If IsInstallNoCDPatch = True Then ReturnValue += NoCDNeedsDiskSpace
        If IsInstallSC4Launcher = True Then ReturnValue += SC4LauncherNeedsDiskSpace
        If LanguagePatch = Language.SimplifiedChinese Then ReturnValue += LanguageSimplifiedChineseNeedsDiskSpace
        Select Case unit
            Case "MB" : Return Math.Round(ReturnValue / 1024 / 1024, 2)
            Case "GB" : Return Math.Round(ReturnValue / 1024 / 1024 / 1024, 2)
            Case Else : Return Nothing
        End Select
    End Function

End Class