''' <summary>提供一个用于获取或设置安装结果的类，此类不能被继承</summary>
Public NotInheritable Class InstallResult

    ''' <summary>指定安装结果</summary>
    Public Enum Result
        ''' <summary>安装成功</summary>
        Success
        ''' <summary>安装失败</summary>
        Fail
    End Enum
    ''' <summary>获取或设置模拟城市4的安装结果</summary>
    Public SC4InstallResult As Result
    ''' <summary>获取或设置语言补丁的安装结果</summary>
    Public LanguagePatchInstallResult As Result
    ''' <summary>获取或设置DAEMON Tools Lite的安装结果</summary>
    Public DAEMONToolsInstallResult As Result
    ''' <summary>获取或设置638补丁的安装结果</summary>
    Public _638PatchInstallResult As Result
    ''' <summary>获取或设置640补丁的安装结果</summary>
    Public _640PatchInstallResult As Result
    ''' <summary>获取或设置641补丁的安装结果</summary>
    Public _641PatchInstallResult As Result
    ''' <summary>获取或设置4GB补丁的安装结果</summary>
    Public _4GBPatchInstallResult As Result
    ''' <summary>获取或设置免CD补丁的安装结果</summary>
    Public NoCDPatchInstallResult As Result
    ''' <summary>获取或设置模拟城市4 启动器的安装结果</summary>
    Public SC4LauncherInstallResult As Result
    ''' <summary>获取或设置添加桌面图标的结果</summary>
    Public AddDesktopIconResult As Result
    ''' <summary>获取或设置添加开始菜单项的结果</summary>
    Public AddStartMenuItemResult As Result

    ''' <summary>初始化 InstallResult 类的新实例</summary>
    Public Sub New()
        SC4InstallResult = Result.Success
        DAEMONToolsInstallResult = Result.Success
        _638PatchInstallResult = Result.Success
        _640PatchInstallResult = Result.Success
        _641PatchInstallResult = Result.Success
        _4GBPatchInstallResult = Result.Success
        NoCDPatchInstallResult = Result.Success
        SC4LauncherInstallResult = Result.Success
        LanguagePatchInstallResult = Result.Success
        AddDesktopIconResult = Result.Success
        AddStartMenuItemResult = Result.Success
    End Sub

End Class