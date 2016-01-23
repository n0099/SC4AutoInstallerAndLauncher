''' <summary>提供一个用于获取或设置安装选项的类，此类不能被继承</summary>
Public NotInheritable Class InstallOptions

    ''' <summary>指定模拟城市4的安装版本</summary>
    Public Enum SC4Type
        ''' <summary>镜像版模拟城市4</summary>
        CD = 1
        ''' <summary>硬盘版模拟城市4</summary>
        NoInstall = 2
    End Enum
    ''' <summary>安装模拟城市4所需要的以字节为单位的磁盘空间</summary>
    Public Const SC4NeedsDiskSpace As Integer = 1029863647
    ''' <summary>安装DAEMON Tools Lite所需要的以字节为单位的磁盘空间</summary>
    Public Const DAEMONToolsNeedsDiskSpace As Integer = 50226791
    ''' <summary>安装638补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const _638PatchNeedsDiskSpace As Integer = -535428
    ''' <summary>安装640补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const _640PatchNeedsDiskSpace As Integer = -7168
    ''' <summary>安装641补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const _641PatchNeedsDiskSpace As Integer = -883162
    ''' <summary>安装免CD补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const NoCDPatchNeedsDiskSpace As Integer = -883162
    ''' <summary>安装模拟城市4 启动器所需要的以字节为单位的磁盘空间</summary>
    Public Const SC4LauncherNeedsDiskSpace As Integer = 6731776
    ''' <summary>安装繁体中文语言补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const TraditionalChineseLanguageNeedsDiskSpace As Integer = 17548123
    ''' <summary>安装简体中文语言补丁所需要的以字节为单位的磁盘空间</summary>
    Public Const SimplifiedChineseLanguageNeedsDiskSpace As Integer = 643284

    ''' <summary>获取或设置是否快速安装</summary>
    Public IsQuickInstall As Boolean
    ''' <summary>获取或设置要安装何种版本的模拟城市4</summary>
    Public SC4InstallType As SC4Type
    ''' <summary>获取或设置模拟城市4的安装目录</summary>
    Public SC4InstallDir As String
    ''' <summary>获取或设置DAEMON Tools Lite的安装目录</summary>
    Public DAEMONToolsInstallDir As String

    ''' <summary>获取或设置是否安装DAEMON Tools Lite</summary>
    Public IsInstallDAEMONTools As Boolean
    ''' <summary>获取或设置是否安装638补丁</summary>
    Public IsInstall638Patch As Boolean
    ''' <summary>获取或设置是否安装640补丁</summary>
    Public IsInstall640Patch As Boolean
    ''' <summary>获取或设置是否安装641补丁</summary>
    Public IsInstall641Patch As Boolean
    ''' <summary>获取或设置是否安装4GB补丁</summary>
    Public IsInstall4GBPatch As Boolean
    ''' <summary>获取或设置是否安装免CD补丁</summary>
    Public IsInstallNoCDPatch As Boolean
    ''' <summary>获取或设置是否安装模拟城市4 启动器</summary>
    Public IsInstallSC4Launcher As Boolean
    ''' <summary>获取或设置模拟城市4的语言</summary>
    Public LanguagePatchOption As SC4Language
    ''' <summary>获取或设置是否添加桌面图标</summary>
    Public IsAddDesktopIcon As Boolean
    ''' <summary>获取或设置是否添加开始菜单项</summary>
    Public IsAddStartMenuItem As Boolean

    ''' <summary>返回当前InstallOptions类实例的浅层副本</summary>
    ''' <returns>返回一个当前InstallOptions类实例浅层副本的InstallOptions类实例</returns>
    Public Function Clone()
        Return MemberwiseClone()
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj Is Nothing Then Return False
        If TypeOf obj Is InstallOptions = False Then Return False
        With CType(obj, InstallOptions)
            If .IsQuickInstall <> Me.IsQuickInstall Then Return False
            If .SC4InstallDir <> Me.SC4InstallDir Then Return False
            If .DAEMONToolsInstallDir <> Me.DAEMONToolsInstallDir Then Return False
            If .SC4InstallType <> Me.SC4InstallType Then Return False
            If .IsInstallDAEMONTools <> Me.IsInstallDAEMONTools Then Return False
            If .IsInstall638Patch <> Me.IsInstall638Patch Then Return False
            If .IsInstall640Patch <> Me.IsInstall640Patch Then Return False
            If .IsInstall641Patch <> Me.IsInstall641Patch Then Return False
            If .IsInstall4GBPatch <> Me.IsInstall4GBPatch Then Return False
            If .IsInstallNoCDPatch <> Me.IsInstallNoCDPatch Then Return False
            If .IsInstallSC4Launcher <> Me.IsInstallSC4Launcher Then Return False
            If .LanguagePatchOption <> Me.LanguagePatchOption Then Return False
            If .IsAddDesktopIcon <> Me.IsAddDesktopIcon Then Return False
            If .IsAddStartMenuItem <> Me.IsAddStartMenuItem Then Return False
        End With
        Return True
    End Function

    ''' <summary>返回当前InstallOptions类实例里已选择的项所需要以GB为单位的磁盘空间</summary>
    ''' <returns>以小数形式返回当前InstallOptions类实例里已选择的项所需要以GB为单位的磁盘空间</returns>
    ''' <remarks>只有模拟城市4、638、640、641补丁、免CD补丁、模拟城市4 启动器和简体中文语言补丁会占用额外的磁盘空间</remarks>
    Public Function GetCurrentOptionsNeedsDiskSpaceInGB() As Decimal
        Dim CurrentOptionsNeedsDiskSpace As Decimal
        CurrentOptionsNeedsDiskSpace += SC4NeedsDiskSpace
        If IsInstall638Patch Then CurrentOptionsNeedsDiskSpace += _638PatchNeedsDiskSpace
        If IsInstall640Patch Then CurrentOptionsNeedsDiskSpace += _640PatchNeedsDiskSpace
        If IsInstall641Patch Then CurrentOptionsNeedsDiskSpace += _641PatchNeedsDiskSpace
        If IsInstallNoCDPatch Then CurrentOptionsNeedsDiskSpace += NoCDPatchNeedsDiskSpace
        If IsInstallSC4Launcher Then CurrentOptionsNeedsDiskSpace += SC4LauncherNeedsDiskSpace
        If LanguagePatchOption = SC4Language.SimplifiedChinese Then CurrentOptionsNeedsDiskSpace += SimplifiedChineseLanguageNeedsDiskSpace
        Return Math.Round(CurrentOptionsNeedsDiskSpace / 1024 / 1024 / 1024, 2)
    End Function

End Class