﻿

'INCLUIR PARAMETROS PARA INCLUSAO E ALTERAÇÃO / REGRAS DE NEGÓCIOS(VALIDAÇÃO)...

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

'COD_CID, NOME, Sit_Cid, Sigla_UF

Public Class BalTipoLogradouro

    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet() As DataSet
        Dim ds As DataSet
        Dim oParam(1) As ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo
        Dim sOrdenacao As String
        Try
            sd.Open()
            sOrdenacao = "Descr"
            oParam(0) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("cRet", OracleDbType.RefCursor, ParameterDirection.Output)
            oParam(1) = New ServicoDadosODPNET.ServicoDadosOracle.ParameterInfo("Ordenacao", OracleDbType.Varchar2, ParameterDirection.Input)
            oParam(1).Valor = sOrdenacao
            ds = sd.CreateDataSet("ExibirDados_Tip_Logr", oParam)
            Return ds
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function
    Public Function Validar() As Boolean
        Dim nState As ConnectionState
        Dim sSQL As String
        sSQL = "select * from TipoLogradouro where Cod_Tip_Logr = ?"
        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntTipoLogradouro
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, ent.Cod_Tip_Logr)
            If dr.Read Then
                ent = GerarEntidade(dr)
                Return True
            Else
                Return False
            End If
            dr.Close()

        Catch ex As ServicoDadosException
            Throw New ApplicationException(ex.Message)
        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If nState <> ConnectionState.Open And sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

Private Function GerarEntidade(ByVal row As OracleDataReader) As EntTipoLogradouro
    Dim Ent As New EntTipoLogradouro

    Ent.Cod_Tip_Logr = row("Cod_Tip_Logr")
    Ent.Descr = row("Descr")
    Return Ent
End Function

End Class
