''' <summary>ModuleMain 模块提供各种类的实例和Windows API方法、结构和常数</summary>
Module ModuleMain

    ''' <summary>一个用于获取或设置安装选项的类的实例</summary>
    Public InstallOptions As New InstallOptions
    ''' <summary>一个用于获取或设置已安装的组件的类的实例</summary>
    Public InstalledModule As New InstalledModule
    ''' <summary>一个用于获取或设置安装结果的类的实例</summary>
    Public InstallResult As New InstallResult

    ''' <summary>该函数允许应用程序为复制或修改而访问窗口菜单（系统菜单或控制菜单）。</summary>
    ''' <param name="hWnd">拥有窗口菜单拷贝的窗口的句柄。</param>
    ''' <param name="bRevert">指定将执行的操作。如果此参数为FALSE，GetSystemMenu返回当前使用窗口菜单的拷贝的句柄。该拷贝初始时与窗口菜单相同，但可以被修改。</param>
    ''' <returns>如果参数bRevert为FALSE，返回值是窗口菜单的拷贝的句柄：如果参数bRevert为TRUE，返回值是NULL。</returns>
    ''' <remarks>任何没有用GetSystemMenu函数来生成自己的窗口菜单拷贝的窗口将接受标准窗口菜单。</remarks>
    Public Declare Function GetSystemMenu Lib "user32" (ByVal hWnd As IntPtr, ByVal bRevert As Boolean) As Integer
    ''' <summary>Determines the number of items in the specified menu.</summary>
    ''' <param name="hMenu">A handle to the menu to be examined.</param>
    ''' <returns>If the function succeeds, the return value specifies the number of items in the menu.If the function fails, the return value is -1.</returns>
    Public Declare Function GetMenuItemCount Lib "user32" (ByVal hMenu As IntPtr) As Integer
    ''' <summary>该函数从指定菜单删除一个菜单项或分离一个子菜单。如果菜单项打开一个下拉式菜单或子菜单，RemoveMenu不消毁该菜单或其句柄，允许菜单被重用。在调用此函数前，函数GetSubMenu应当取得下拉式菜单或子菜单的句柄。</summary>
    ''' <param name="hMenu">将被修改的菜单的句柄。</param>
    ''' <param name="uPosition">指定将被删除的菜单项，其含义由参数uFlages决定。</param>
    ''' <param name="uFlags">指定参数uPosition如何解释。此参数必须为下列之一值：</param>
    ''' <returns>如果函数调用成功，返回非零值；如果函数调用失败，返回值是零。</returns>
    ''' <remarks>只要一个菜单被修改，无论它是否在显示窗口里，应用程序都必须调用函数DrawMenuBar。</remarks>
    Public Declare Function RemoveMenu Lib "user32" (ByVal hMenu As IntPtr, ByVal uPosition As UInt32, ByVal uFlags As UInt32) As Integer
    ''' <summary>该函数重画指定菜单的菜单条。如果系统创建窗口以后菜单条被修改，则必须调用此函数来画修改了的菜单条。</summary>
    ''' <param name="hWnd">其菜单条需要被重画的窗口的句柄。</param>
    ''' <returns>如果函数调用成功，返回非零值：如果函数调用失败，返回值是零。</returns>
    Public Declare Function DrawMenuBar Lib "user32" (ByVal hWnd As IntPtr) As Integer
    ''' <summary>Indicates that the uIDCheckItem parameter gives the zero-based relative position of the menu item.</summary>
    Public Const MF_BYPOSITION = &H400&
    ''' <summary>使菜单项无效，以便它不能被选择，但不变灰。</summary>
    Public Const MF_DISABLED = &H2&

    ''' <summary>Flashes the specified window. It does not change the active state of the window.</summary>
    ''' <param name="pwfi">A pointer to a FLASHWINFO structure.</param>
    ''' <returns>The return value specifies the window's state before the call to the FlashWindowEx function. If the window caption was drawn as active before the call, the return value is nonzero. Otherwise, the return value is zero.</returns>
    ''' <remarks>Typically, you flash a window to inform the user that the window requires attention but does not currently have the keyboard focus. When a window flashes, it appears to change from inactive to active status. An inactive caption bar changes to an active caption bar; an active caption bar changes to an inactive caption bar.</remarks>
    Public Declare Function FlashWindowEx Lib "user32" (ByRef pwfi As FLASHINFO) As Boolean
    ''' <summary>Contains the flash status for a window and the number of times the system should flash the window.</summary>
    Public Structure FLASHINFO
        ''' <summary>The size of the structure, in bytes.</summary>
        Public cbSize As UInt32
        ''' <summary>A handle to the window to be flashed. The window can be either opened or minimized.</summary>
        Public hwnd As IntPtr
        ''' <summary>The flash status.</summary>
        Public dwFlags As UInt32
        ''' <summary>The number of times to flash the window.</summary>
        Public uCount As UInt32
        ''' <summary>The rate at which the window is to be flashed, in milliseconds. If dwTimeout is zero, the function uses the default cursor blink rate.</summary>
        Public dwTimeout As UInt32
    End Structure
    ''' <summary>Flash both the window caption and taskbar button. This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.</summary>
    Public Const FLASHW_ALL = 3

End Module