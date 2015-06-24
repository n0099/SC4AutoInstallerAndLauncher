Public Class InstallResult

    Public Enum Result
        Success
        Fail
    End Enum
    Public SC4InstallResult As Result
    Public DAEMONToolsInstallResult As Result
    Public _638PatchInstallResult As Result
    Public _640PatchInstallResult As Result
    Public _4GBPatchInstallResult As Result
    Public NoCDPatchInstallResult As Result
    Public SC4LauncherInstallResult As Result
    Public LanguagePatchInstallResult As Result
    Public AddDesktopIconResult As Result
    Public AddStartMenuItemResult As Result

    Public Sub New()
        SC4InstallResult = Result.Success
        DAEMONToolsInstallResult = Result.Success
        _638PatchInstallResult = Result.Success
        _640PatchInstallResult = Result.Success
        _4GBPatchInstallResult = Result.Success
        NoCDPatchInstallResult = Result.Success
        SC4LauncherInstallResult = Result.Success
        LanguagePatchInstallResult = Result.Success
        AddDesktopIconResult = Result.Success
        AddStartMenuItemResult = Result.Success
    End Sub

End Class