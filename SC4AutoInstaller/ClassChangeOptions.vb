''' <summary>提供一个用于获取或设置安装或卸载组件选项的类，此类不能被继承</summary>
Public NotInheritable Class ChangeOptions

    ''' <summary>指定组件的更改选项</summary>
    Public Enum ChangeOption
        ''' <summary>未更改该组件</summary>
        Unchanged = 0
        ''' <summary>安装该组件</summary>
        Install = 1
        ''' <summary>卸载该组件</summary>
        Uninstall = 2
    End Enum
    ''' <summary>获取或设置是否更改638补丁</summary>
    Public _638PatchOption As ChangeOption
    ''' <summary>获取或设置是否更改640补丁</summary>
    Public _640PatchOption As ChangeOption
    ''' <summary>获取或设置是否更改641补丁</summary>
    Public _641PatchOption As ChangeOption
    ''' <summary>获取或设置是否更改4GB补丁</summary>
    Public _4GBPatchOption As ChangeOption
    ''' <summary>获取或设置是否更改免CD补丁</summary>
    Public NoCDPatchOption As ChangeOption
    ''' <summary>获取或设置是否更改模拟城市4 启动器</summary>
    Public SC4LauncherOption As ChangeOption
    ''' <summary>获取或设置是否更改语言补丁</summary>
    Public LanguagePatchOption As SC4Language

    ''' <summary>将当前ChangeOptions类实例与指定的InstalledModule类实例进行比较</summary>
    ''' <param name="InstalledModule">要相比较的InstalledModule类实例</param>
    ''' <returns>如果某个组件的更改选项与InstalledModule类实例对应组件的值不同，则为False；否则为True</returns>
    Public Function IsSameAsInstalledModule(InstalledModule As InstalledModules) As Boolean
        If InstalledModule Is Nothing Then Return False
        With InstalledModule
            If .Is638PatchInstalled <> If(Me._638PatchOption = ChangeOption.Uninstall, False, True) Then Return False
            If .Is640PatchInstalled <> If(Me._640PatchOption = ChangeOption.Uninstall, False, True) Then Return False
            If .Is641PatchInstalled <> If(Me._641PatchOption = ChangeOption.Uninstall, False, True) Then Return False
            If .Is4GBPatchInstalled <> If(Me._4GBPatchOption = ChangeOption.Uninstall, False, True) Then Return False
            If .IsNoCDPatchInstalled <> If(Me.NoCDPatchOption = ChangeOption.Uninstall, False, True) Then Return False
            If .IsSC4LauncherInstalled <> If(Me.SC4LauncherOption = ChangeOption.Uninstall, False, True) Then Return False
            If .LanguagePatchOption <> Me.LanguagePatchOption Then Return False
        End With
        Return True
    End Function

End Class