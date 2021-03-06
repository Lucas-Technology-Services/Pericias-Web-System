﻿Imports System.Web.SessionState
Imports log4net

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Dim logger As log4net.ILog = log4net.LogManager.GetLogger(Me.GetType().ToString)

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        log4net.Config.XmlConfigurator.Configure()
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
        'Dim erro As Exception = Server.GetLastError()
        'logger = log4net.LogManager.GetLogger("LogInFile")
        'logger.Error("Exception: " & erro.ToString())
        ''Server.Transfer("erro.aspx?msg=" & erro.ToString())
        'If Session("erro") Is Nothing Then
        '    Session.Add("erro", Nothing)
        'End If
        'Session("erro") = IIf(erro.InnerException Is Nothing, erro.Message, erro.InnerException.Message.ToString())
        ''Server.Transfer("erro.aspx")
        ''Response.Redirect("erro.aspx?msg=" & erro.InnerException.Message.ToString)
        'Response.Redirect("erro.aspx")

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
    End Sub

End Class