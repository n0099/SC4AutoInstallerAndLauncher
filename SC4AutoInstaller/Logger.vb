''' <summary>记录信息类</summary>
Public NotInheritable Class Logger
    Private Shared logger As log4net.ILog = log4net.LogManager.GetLogger("SC4AutoInstaller")
    ''' <summary>记录普通信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Function Info(ByVal Message As String)
        logger.Info(Message)
    End Function
    ''' <summary>记录警告信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Function Warn(ByVal Message As String)
        logger.Warn(Message)
    End Function
    ''' <summary>记录失败信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Function Fail(ByVal Message As String)
        logger.Error(Message)
    End Function
    ''' <summary>记录调试信息</summary>
    ''' <param name="Message">信息内容</param>
    Public Shared Function Trace(ByVal Message As String)
        logger.Debug(Message)
    End Function
End Class
