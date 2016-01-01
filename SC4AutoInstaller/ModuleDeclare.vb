Imports System.Runtime.InteropServices

''' <summary>ModuleDeclare模块提供各种类实例、枚举和Windows API方法、结构和常数声明</summary>
Public Module ModuleDeclare

    ''' <summary>一个用于获取或设置安装选项类实例</summary>
    Public InstallOptions As New InstallOptions
    ''' <summary>一个用于获取或设置安装结果类实例</summary>
    Public InstallResult As New InstallResults
    ''' <summary>一个用于获取或设置已安装的组件类实例</summary>
    Public InstalledModule As New InstalledModule
    ''' <summary>一个用于获取或设置更改组件选项类实例</summary>
    Public ChangeOptions As New ChangeOptions
    ''' <summary>指定模拟城市4的语言</summary>
    Public Enum SC4Language
        ''' <summary>繁体中文</summary>
        TraditionalChinese = 18
        ''' <summary>简体中文</summary>
        SimplifiedChinese = 17
        ''' <summary>英语</summary>
        English = 1
    End Enum

    <DllImport("user32.dll", CallingConvention:=CallingConvention.Cdecl)> Public Function GetSystemMenu(ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As IntPtr : End Function
    <DllImport("user32.dll")> Public Function GetMenuItemCount(ByVal hMenu As IntPtr) As Int32 : End Function
    <DllImport("user32.dll")> Public Function RemoveMenu(ByVal hMenu As IntPtr, ByVal uPosition As UInt32, ByVal uFlags As UInt32) As Boolean : End Function
    <DllImport("user32.dll")> Public Function DrawMenuBar(ByVal hWnd As IntPtr) As Boolean : End Function
    Public Const MF_BYPOSITION = &H400&
    Public Const MF_DISABLED = &H2&

    <DllImport("user32.dll")> Public Function FlashWindowEx(ByRef pwfi As FLASHWINFO) As Boolean : End Function
    Public Structure FLASHWINFO
        Public cbSize As UInt32
        Public hwnd As IntPtr
        Public dwFlags As UInt32
        Public uCount As UInt32
        Public dwTimeout As UInt32
    End Structure
    Public Const FLASHW_ALL = 3

End Module