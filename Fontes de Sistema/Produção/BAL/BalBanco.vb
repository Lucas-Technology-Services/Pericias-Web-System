﻿'INCLUIR PARAMETROS PARA INCLUSAO E ALTERAÇÃO / REGRAS DE NEGÓCIOS(VALIDAÇÃO)...

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

'COD_CID, NOME, Sit_Cid, Sigla_UF

Public Class BalBanco

    Inherits BaseBAL

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function ExibirDadosSet(Optional ByVal sFiltro As String = "") As DataSet
        Dim ds As DataSet = Nothing
        
        Try
            Return sd.ExecutaProcDS("ExibirDados_Banco", sd.CriaRefCursor, sFiltro)
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return ds
    End Function

    Public Function ExibirDadosUFSet() As DataSet
        Dim ds As DataSet = Nothing
 
        Try
            Return sd.ExecutaProcDS("ExibirDados_Banco", sd.CriaRefCursor, "Nome")
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
        sSQL = "select * from Banco where Cod_Banco = ?"
        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntBanco
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, ent.Cod_Banco)
            If dr.Read Then
                ent = GerarEntidade(dr)
                Return True
            Else
                Return False
            End If
            dr.Close()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If nState <> ConnectionState.Open And sd.State = ConnectionState.Open Then
                sd.Close()
            End If
        End Try

    End Function

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntBanco
        Dim Ent As New EntBanco

        Ent.Cod_Banco = row("COD_BANCO")
        Ent.Nome = row("NOME")

        Return Ent
    End Function

End Class



