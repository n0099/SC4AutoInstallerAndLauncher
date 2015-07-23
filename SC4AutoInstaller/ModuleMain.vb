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
    ''' <summary>该函数从指定菜单删除一个菜单项或分离一个子菜单。如果菜单项打开一个下拉式菜单或子菜单，RemoveMenu不消毁该菜单或其句柄，允许菜单被重用。在调用此函数前，函数GetSubMenu应当取得下拉式菜单或子菜单的句柄。</summary>
    ''' <param name="hMenu">将被修改的菜单的句柄。</param>
    ''' <param name="uPosition">指定将被删除的菜单项，其含义由参数uFlages决定。</param>
    ''' <param name="uFlags">指定参数uPosition如何解释。此参数必须为下列之一值：</param>
    ''' <returns>如果函数调用成功，返回非零值；如果函数调用失败，返回值是零。若想获得更多的错误信息，请调用GetLastError函数。</returns>
    ''' <remarks>只要一个菜单被修改，无论它是否在显示窗口里，应用程序都必须调用函数DrawMenuBar。</remarks>
    Public Declare Function RemoveMenu Lib "user32" (ByVal hMenu As IntPtr, ByVal uPosition As UInt32, ByVal uFlags As UInt32) As Integer
    ''' <summary>该函数重画指定菜单的菜单条。如果系统创建窗口以后菜单条被修改，则必须调用此函数来画修改了的菜单条。</summary>
    ''' <param name="hWnd">其菜单条需要被重画的窗口的句柄。</param>
    ''' <returns>如果函数调用成功，返回非零值：如果函数调用失败，返回值是零。若想获得更多的错误信息，请调用GetLastError函数。</returns>
    Public Declare Function DrawMenuBar Lib "user32" (ByVal hWnd As IntPtr) As Integer
    ''' <summary>Determines the number of items in the specified menu.</summary>
    ''' <param name="hMenu">A handle to the menu to be examined.</param>
    ''' <returns>If the function succeeds, the return value specifies the number of items in the menu.
    ''' <para>If the function fails, the return value is -1. To get extended error information, call GetLastError.</para>
    ''' </returns>
    ''' <remarks></remarks>
    Public Declare Function GetMenuItemCount Lib "user32" (ByVal hMenu As IntPtr) As Integer
    ''' <summary>该函数将指定的消息发送到一个或多个窗口。此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。</summary>
    ''' <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口。</param>
    ''' <param name="Msg">指定被发送的消息。</param>
    ''' <param name="wParam">指定附加的消息特定信息。</param>
    ''' <param name="lParam">指定附加的消息特定信息。</param>
    ''' <returns>返回值指定消息处理的结果，依赖于所发送的消息。</returns>
    ''' <remarks>需要用HWND_BROADCAST通信的应用程序应当使用函数RegisterWindowMessage来为应用程序间的通信取得一个唯一的消息。</remarks>
    Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As String) As IntPtr
    ''' <summary>该函数将一个消息放入（寄送）到与指定窗口创建的线程相联系消息队列里，不等待线程处理消息就返回，是异步消息模式。</summary>
    ''' <param name="hWnd">其窗口程序接收消息的窗口的句柄。</param>
    ''' <param name="Msg">指定被寄送的消息。</param>
    ''' <param name="wParam">指定附加的消息特定的信息。</param>
    ''' <param name="lParam">指定附加的消息特定的信息。</param>
    ''' <returns>如果函数调用成功，返回非零，否则函数调用返回值为零</returns>
    Public Declare Function PostMessage Lib "user32" Alias "PostMessageA" (ByVal hWnd As IntPtr, ByVal Msg As UInteger, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As Boolean
    ''' <summary>FindWindow函数检索处理顶级窗口的类名和窗口名称匹配指定的字符串。该函数不搜索子窗口。</summary>
    ''' <param name="lpClassName">指向一个以null结尾的、用来指定类名的字符串或一个可以确定类名字符串的原子。</param>
    ''' <param name="lpWindowName">指向一个以null结尾的、用来指定窗口名（即窗口标题）的字符串。如果此参数为NULL，则匹配所有窗口名。</param>
    ''' <returns>如果函数执行成功，则返回值是拥有指定窗口类名或窗口名的窗口的句柄。
    ''' <para>如果函数执行失败，则返回值为 NULL 。可以通过调用GetLastError函数获得更加详细的错误信息。</para>
    ''' </returns>
    Public Declare Function FindWindow Lib "user32" Alias "FindWindowA" (ByVal lpClassName As String, ByVal lpWindowName As String) As IntPtr
    ''' <summary>在窗口列表中寻找与指定条件相符的第一个子窗口 。
    ''' <para>该函数获得一个窗口的句柄，该窗口的类名和窗口名与给定的字符串相匹配。这个函数查找子窗口，从排在给定的子窗口后面的下一个子窗口开始。在查找时不区分大小写。</para>
    ''' </summary>
    ''' <param name="parentHandle">要查找的子窗口所在的父窗口的句柄（如果设置了hwndParent，则表示从这个hwndParent指向的父窗口中搜索子窗口）。
    ''' <para>如果hwndParent为 0 ，则函数以桌面窗口为父窗口，查找桌面窗口的所有子窗口。</para></param>
    ''' <param name="childAfter">子窗口句柄。查找从在Z序中的下一个子窗口开始。
    ''' <para>子窗口必须为hwndParent窗口的直接子窗口而非后代窗口。如果HwndChildAfter为NULL，查找从hwndParent的第一个子窗口开始。</para>
    ''' <para>如果hwndParent 和 hwndChildAfter同时为NULL，则函数查找所有的顶层窗口及消息窗口。</para>
    ''' </param>
    ''' <param name="lclassName">指向一个指定了类名的空结束字符串，或一个标识类名字符串的成员的指针。
    ''' <para>如果该参数为一个成员，则它必须为前次调用theGlobaIAddAtom函数产生的全局成员。该成员为16位，必须位于lpClassName的低16位，高位必须为0。</para>
    ''' </param>
    ''' <param name="windowTitle">指向一个指定了窗口名（窗口标题）的空结束字符串。如果该参数为 NULL，则为所有窗口全匹配。</param>
    ''' <returns>如果函数成功，返回值为具有指定类名和窗口名的窗口句柄。如果函数失败，返回值为NULL。若想获得更多错误信息，请调用GetLastError函数。</returns>
    ''' <remarks></remarks>
    Public Declare Function FindWindowEx Lib "user32" Alias "FindWindowExA" (ByVal parentHandle As IntPtr, ByVal childAfter As IntPtr, ByVal lclassName As String, ByVal windowTitle As String) As IntPtr
    ''' <summary>改变一个子窗口，弹出式窗口或顶层窗口的尺寸，位置和Z序。子窗口，弹出式窗口，及顶层窗口根据它们在屏幕上出现的顺序排序、顶层窗口设置的级别最高，并且被设置为Z序的第一个窗口。</summary>
    ''' <param name="hWnd">窗口句柄。</param>
    ''' <param name="hWndInsertAfter">在z序中的位于被置位的窗口前的窗口句柄。该参数必须为一个窗口句柄。</param>
    ''' <param name="X">以客户坐标指定窗口新位置的左边界。</param>
    ''' <param name="Y">以客户坐标指定窗口新位置的顶边界。</param>
    ''' <param name="cx">以像素指定窗口的新的宽度。</param>
    ''' <param name="cy">以像素指定窗口的新的高度。</param>
    ''' <param name="uFlags">窗口尺寸和定位的标志。</param>
    ''' <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。若想获得更多错误消息，请调用GetLastError函数。</returns>
    Public Declare Function SetWindowPos Lib "user32" (ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As UInteger) As Boolean
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
    ''' <summary>当一个非系统键被按下时该消息发送给具有键盘焦点的窗口。非系统键即不与ALT联用的情况。</summary>
    Public Const WM_KEYDOWN = &H100
    ''' <summary>Posted to the window with the keyboard focus when a nonsystem key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, or a keyboard key that is pressed when a window has the keyboard focus.</summary>
    Public Const WM_KEYUP = &H101
    ''' <summary>TAB key</summary>
    Public Const VK_TAB = &H9
    ''' <summary>当用户在window客户区域点击鼠标左键的时候发送。如果当前鼠标没有被捕捉，消息发送给鼠标下面的window窗体。否则，消息发送给当前捕捉鼠标消息的方法。</summary>
    Public Const WM_LBUTTONDOWN = &H201
    ''' <summary>当光标在窗口客户区时，用户释放鼠标左键时发出的消息。如果鼠标没有捕获，这个消息被送到光标下的窗口。否则，该消息发布到捕获鼠标的窗口。</summary>
    Public Const WM_LBUTTONUP = &H202
    ''' <summary>Sets the text of a window.</summary>
    Public Const WM_SETTEXT = &HC
    ''' <summary>将窗口置于Z序的顶部。</summary>
    Public Const HWND_TOP = 0
    ''' <summary>维持当前尺寸（忽略cx和Cy参数）。</summary>
    Public Const SWP_NOSIZE = &H1
    ''' <summary>维持当前位置（忽略X和Y参数）。</summary>
    Public Const SWP_NOMOVE = &H2
    ''' <summary>Indicates that the uIDCheckItem parameter gives the zero-based relative position of the menu item.</summary>
    Public Const MF_BYPOSITION = &H400&
    ''' <summary>使菜单项无效，以便它不能被选择，但不变灰。</summary>
    Public Const MF_DISABLED = &H2&
    ''' <summary>Flash both the window caption and taskbar button. This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.</summary>
    Public Const FLASHW_ALL = 3

End Module