''' <summary>提供一个用于获取或设置安装选项的类，此类不能被继承</summary>
Public NotInheritable Class InstallOptions

    ''' <summary>指定模拟城市4的安装版本</summary>
    Public Enum SC4InstallType
        ''' <summary>镜像版模拟城市4</summary>
        ISO
        ''' <summary>硬盘版模拟城市4</summary>
        NoInstall
    End Enum
    ''' <summary>指定模拟城市4的语言</summary>
    Public Enum Language
        ''' <summary>繁体中文</summary>
        TraditionalChinese
        ''' <summary>简体中文</summary>
        SimplifiedChinese
        ''' <summary>英语</summary>
        English
    End Enum
    ''' <summary>安装模拟城市4所需要的以字节为单位的磁盘空间</summary>
    Public Const SC4NeedsDiskSpace As Integer = 1029863647
    ''' <summary>安装DAEMON Tools Lite所需要的以字节为单位的磁盘空间</summary>
    Public Const DAEMONToolsNeedsDiskSpace As Integer = 50226791
    ''' <summary>安装638补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const _638NeedsDiskSpace As Integer = -535428
    ''' <summary>安装640补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const _640NeedsDiskSpace As Integer = 41015
    ''' <summary>安装641补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const _641NeedsDiskSpace As Integer = -883162
    ''' <summary>安装免CD补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const NoCDNeedsDiskSpace As Integer = -883162
    ''' <summary>安装模拟城市4 启动器所需要的以字节为单位的磁盘空间</summary>
    Public Const SC4LauncherNeedsDiskSpace As Integer = 90112
    ''' <summary>安装简体中文语言补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const LanguageSimplifiedChineseNeedsDiskSpace As Integer = 643284
    ''' <summary>获取或设置模拟城市4的安装目录</summary>
    Public SC4InstallDir As String
    ''' <summary>获取或设置是否安装语言补丁</summary>
    Public LanguagePatch As Language
    ''' <summary>获取或设置DAEMON Tools Lite的安装目录</summary>
    Public DAEMONInstallDir As String
    ''' <summary>获取或设置要安装何种版本的模拟城市4</summary>
    Public SC4Type As SC4InstallType
    ''' <summary>获取或设置是否安装DAEMON Tools Lite</summary>
    Public InstallDAEMONTools As Boolean
    ''' <summary>获取或设置是否安装638补丁</summary>
    Public Install638Patch As Boolean
    ''' <summary>获取或设置是否安装640补丁</summary>
    Public Install640Patch As Boolean
    ''' <summary>获取或设置是否安装641补丁</summary>
    Public Install641Patch As Boolean
    ''' <summary>获取或设置是否安装4GB补丁</summary>
    Public Install4GBPatch As Boolean
    ''' <summary>获取或设置是否安装免CD补丁</summary>
    Public InstallNoCDPatch As Boolean
    ''' <summary>获取或设置是否安装模拟城市4 启动器</summary>
    Public InstallSC4Launcher As Boolean
    ''' <summary>获取或设置是否添加桌面图标</summary>
    Public AddDesktopIcon As Boolean
    ''' <summary>获取或设置是否添加开始菜单项</summary>
    Public AddStartMenuItem As Boolean

    ''' <summary>返回当前 InstallOptions 类的实例里已选择的项所需要以GB为单位的磁盘空间</summary>
    ''' <returns>以小数形式返回当前 InstallOptions 类的实例里已选择的项所需要以GB为单位的磁盘空间</returns>
    ''' <remarks>只有模拟城市4、638、640、641补丁、免CD补丁、模拟城市4 启动器和简体中文语言补丁会占用额外的磁盘空间</remarks>
    Public Function GetNeedsDiskSpaceByGB() As Decimal
        Dim ReturnValue As Decimal = 0
        If SC4Type = SC4InstallType.ISO Or SC4Type = SC4InstallType.NoInstall Then ReturnValue += SC4NeedsDiskSpace
        If Install638Patch = True Then ReturnValue += _638NeedsDiskSpace
        If Install640Patch = True Then ReturnValue += _640NeedsDiskSpace
        If Install641Patch = True Then ReturnValue += _641NeedsDiskSpace
        If InstallNoCDPatch = True Then ReturnValue += NoCDNeedsDiskSpace
        If InstallSC4Launcher = True Then ReturnValue += SC4LauncherNeedsDiskSpace
        If LanguagePatch = Language.SimplifiedChinese Then ReturnValue += LanguageSimplifiedChineseNeedsDiskSpace
        Return Math.Round(ReturnValue / 1024 / 1024 / 1024, 2)
    End Function

End Class