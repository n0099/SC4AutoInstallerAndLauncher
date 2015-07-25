''' <summary>提供一个用于获取或设置已安装的组件的类，此类不能被继承</summary>
Public NotInheritable Class InstalledModule

    ''' <summary>指定模拟城市4的语言</summary>
    Public Enum Language
        ''' <summary>繁体中文</summary>
        TraditionalChinese
        ''' <summary>简体中文</summary>
        SimplifiedChinese
        ''' <summary>英语</summary>
        English
    End Enum
    ''' <summary>获取或设置模拟城市4的安装目录</summary>
    Public SC4InstallDir As String
    ''' <summary>获取或设置已安装的语言补丁的语言</summary>
    Public LanguagePatch As Language
    ''' <summary>获取或设置是否已经安装638补丁</summary>
    Public Is638PatchInstalled As Boolean
    ''' <summary>获取或设置是否已经安装640补丁</summary>
    Public Is640PatchInstalled As Boolean
    ''' <summary>获取或设置是否已经安装641补丁</summary>
    Public Is641PatchInstalled As Boolean
    ''' <summary>获取或设置是否已经安装4GB补丁</summary>
    Public Is4GBPatchInstalled As Boolean
    ''' <summary>获取或设置是否已经安装免CD补丁</summary>
    Public IsNoCDPatchInstalled As Boolean
    ''' <summary>获取或设置是否已经安装模拟城市4 启动器</summary>
    Public IsSC4LauncherInstalled As Boolean

End Class