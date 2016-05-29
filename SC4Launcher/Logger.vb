''' <summary>记录信息类</summary>
Public NotInheritable Class Logger
    Private Shared logger As log4net.ILog = log4net.LogManager.GetLogger("SC4Launcher")
    ''' <summary>记录普通信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Sub Info(ByVal Message As String)
        logger.Info(Message)
    End Sub
    ''' <summary>记录警告信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Sub Warn(ByVal Message As String)
        logger.Warn(Message)
    End Sub
    ''' <summary>记录失败信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Sub Fail(ByVal Message As String)
        logger.Error(Message)
    End Sub
    ''' <summary>记录调试信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Sub Trace(ByVal Message As String)
        logger.Debug(Message)
    End Sub
End Class
