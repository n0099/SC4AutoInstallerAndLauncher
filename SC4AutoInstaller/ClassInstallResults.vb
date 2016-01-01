''' <summary>提供一个用于获取或设置安装结果的类，此类不能被继承</summary>
Public NotInheritable Class InstallResults

    ''' <summary>指定安装结果</summary>
    Public Enum InstallResult
        ''' <summary>安装成功</summary>
        Success = 0
        ''' <summary>安装失败</summary>
        Fail = 1
    End Enum
    ''' <summary>获取或设置模拟城市4的安装结果</summary>
    Public SC4InstallResult As InstallResult
    ''' <summary>获取或设置DAEMON Tools Lite的安装结果</summary>
    Public DAEMONToolsResult As InstallResult
    ''' <summary>获取或设置638补丁的安装结果</summary>
    Public _638PatchResult As InstallResult
    ''' <summary>获取或设置640补丁的安装结果</summary>
    Public _640PatchResult As InstallResult
    ''' <summary>获取或设置641补丁的安装结果</summary>
    Public _641PatchResult As InstallResult
    ''' <summary>获取或设置4GB补丁的安装结果</summary>
    Public _4GBPatchResult As InstallResult
    ''' <summary>获取或设置免CD补丁的安装结果</summary>
    Public NoCDPatchResult As InstallResult
    ''' <summary>获取或设置模拟城市4 启动器的安装结果</summary>
    Public SC4LauncherResult As InstallResult
    ''' <summary>获取或设置语言补丁的安装结果</summary>
    Public LanguagePatchResult As InstallResult

End Class