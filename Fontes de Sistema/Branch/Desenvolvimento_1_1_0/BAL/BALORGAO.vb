'------------------------------------------------
' 02/08/2007 11:45:47
' Código Gerado com o programa GeradorClasses 
' Engine: BLLGeradorClasses, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
'------------------------------------------------

Imports ServicoDadosODPNET
Imports Oracle.DataAccess.Client
Imports Entidade
Imports Utilitarios.DadosUtil
Imports System.Configuration.ConfigurationManager

Public Class BALORGAO
    Inherits BaseBal

    Public Sub New(ByVal Usuario As EstruturaPadrao.EstruturaIdentificacaoUsuario)
        MyBase.New(Usuario)
    End Sub

    Public Sub New(ByVal sDados As ServicoDadosOracle)
        MyBase.New(sDados)
    End Sub

    Public Function Listar(ByVal sFiltro As String, ByVal sOrdenacao As String) As EntORGAO()
        Dim lst() As EntORGAO = New EntORGAO() {}
        Dim dr As OracleDataReader
        Dim sSQL As String

        Try
            sFiltro = "%" + sFiltro + "%"
            'sSQL = "select * from ORGAO where ind_ativo='S' and ind_hab_prot = 'S' and UPPER(nome_dtl) like ? " + IIf(sOrdenacao = String.Empty, String.Empty, " order by " + sOrdenacao)
            sSQL = "Select O.COD_ORG, NOME, COMPL_NOME, NOME_DTL, NOME_REDU,  NOME_ABREV, COD_GRUP_LOC_ORG, NUM_SEQ_REL, " & _
                    "COD_ORG_HLL, COD_COM, NOME_LOGR,  NUM_LOGR, COMPL_LOGR, COD_CID, COD_BAI,  CEP,  DDD, TEL1, RAMAL1, " & _
                    "TEL2, RAMAL2, TEL_FAX, RAMAL_FAX, DT_INATIV, E_MAIL, COD_SIT_ORG, DT_INST, IND_ORG_OFICLZ, " & _
                    "IND_ORG_JUDIC, IND_ORG_EXT_TJ, TIP_GRAU, TXT_OBS, DT_END, COD_TIP_ORG, COD_ORG_SUP, TIP_VINC, " & _
                    "COD_AREA_ATU, IND_ATIVO,  TIP_ATRIB_BOL_CGJ, IND_HAB_PROT, NUM_MATR_MAG, COD_IMOV, TIP_AREA_ATIV, " & _
                    "IND_FISCAL, IND_INSTALADA, IND_ORG_FORMAL, IND_SUB_LOT, IND_UNID_PATR, COD_GRUP_CAD_GPES, IND_SOLUC, " & _
                    "TIP_VAGA_TIT, COD_TIP_LOGR, NUM_BLOC_LOGR, NUM_AND_LOGR, NUM_CORR_LOGR, NUM_SALA_LOGR, TXT_REFER_LOGR, " & _
                    "CNPJ, OBS_RELS, IND_VAGA_TITULAR, IND_INSALUBRI, IND_RESOLUCAO_ADM_JUD,  IND_PREV_VAGA_TITULAR, " & _
                    "COD_LEI_LEGISLATIVO, DT_PUBLIC_LEGISLATIVO, NUM_ORGAO, ZONA, DISTRITO,  SUBDISTRITO, CIRCUNSCRICAO, " & _
                    "NOME_OFICIAL, DESCR_INSTALA, COD_TIPO_SERVENTIA, COD_TIPO_VAGA_TIT, COD_MODIFICA_ORGAO, COD_QUALIFICA, " & _
                    "COD_CATEGORIA, COD_NIVEL_ESTRUTURA, COD_TIP_DENOM_SERV,COD_POS_ORG, USU_ATUALZ, COD_ORG_NOVO, COD_CLASS, " & _
                    "DESCR_SETOR_PROTOCOLO Descr_Grup_Cad_Gpes " & _
                    "from ORGAO O,SETORPROTOCOLO SP " & _
                    "where ind_ativo='S' and ind_hab_prot = 'S' and sp.COD_SETOR_PROTOCOLO(+) = COD_GRUP_CAD_GPES and " & _
                    "UPPER(nome_dtl) like ? " + IIf(sOrdenacao = String.Empty, String.Empty, " order by " + sOrdenacao)

            sd.Open()
            dr = sd.ExecuteReader(sSQL, sFiltro)
            While dr.Read
                Array.Resize(Of EntORGAO)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While
            dr.Close()
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return lst
    End Function

    Public Function ListaHierarquica(ByVal pCodOrg As String, ByVal pSomenteAtivos As String, ByVal pNivelHierarquia As String) As EntORGAO()
        Dim lst() As EntORGAO = New EntORGAO() {}
        Dim dr As OracleDataReader
        Dim sSQL As String
        Dim sWhere As String = ""

        Try
            sWhere = "where level <= " & IIf(pNivelHierarquia = String.Empty, "9", pNivelHierarquia)

            If pSomenteAtivos = "S" Then
                sWhere = sWhere & " and ind_ativo='S' and ind_hab_prot = 'S' "
            End If

            'sSQL = "select * from ORGAO " & sWhere & _
            '       "Start With cod_org = ? " & _
            '       "Connect By Prior cod_org = cod_org_sup"

            sSQL = "Select O.COD_ORG, NOME, COMPL_NOME, NOME_DTL, NOME_REDU,  NOME_ABREV, COD_GRUP_LOC_ORG, NUM_SEQ_REL, " & _
                    "COD_ORG_HLL, COD_COM, NOME_LOGR,  NUM_LOGR, COMPL_LOGR, COD_CID, COD_BAI,  CEP,  DDD, TEL1, RAMAL1, " & _
                    "TEL2, RAMAL2, TEL_FAX, RAMAL_FAX, DT_INATIV, E_MAIL, COD_SIT_ORG, DT_INST, IND_ORG_OFICLZ, " & _
                    "IND_ORG_JUDIC, IND_ORG_EXT_TJ, TIP_GRAU, TXT_OBS, DT_END, COD_TIP_ORG, COD_ORG_SUP, TIP_VINC, " & _
                    "COD_AREA_ATU, IND_ATIVO,  TIP_ATRIB_BOL_CGJ, IND_HAB_PROT, NUM_MATR_MAG, COD_IMOV, TIP_AREA_ATIV, " & _
                    "IND_FISCAL, IND_INSTALADA, IND_ORG_FORMAL, IND_SUB_LOT, IND_UNID_PATR, COD_GRUP_CAD_GPES, IND_SOLUC, " & _
                    "TIP_VAGA_TIT, COD_TIP_LOGR, NUM_BLOC_LOGR, NUM_AND_LOGR, NUM_CORR_LOGR, NUM_SALA_LOGR, TXT_REFER_LOGR, " & _
                    "CNPJ, OBS_RELS, IND_VAGA_TITULAR, IND_INSALUBRI, IND_RESOLUCAO_ADM_JUD,  IND_PREV_VAGA_TITULAR, " & _
                    "COD_LEI_LEGISLATIVO, DT_PUBLIC_LEGISLATIVO, NUM_ORGAO, ZONA, DISTRITO,  SUBDISTRITO, CIRCUNSCRICAO, " & _
                    "NOME_OFICIAL, DESCR_INSTALA, COD_TIPO_SERVENTIA, COD_TIPO_VAGA_TIT, COD_MODIFICA_ORGAO, COD_QUALIFICA, " & _
                    "COD_CATEGORIA, COD_NIVEL_ESTRUTURA, COD_TIP_DENOM_SERV,COD_POS_ORG, USU_ATUALZ, COD_ORG_NOVO, COD_CLASS, " & _
                    "DESCR_SETOR_PROTOCOLO Descr_Grup_Cad_Gpes " & _
                    "from ORGAO O,SETORPROTOCOLO SP " & sWhere & _
                    "and sp.COD_SETOR_PROTOCOLO(+) = COD_GRUP_CAD_GPES " & _
                    "Start With O.cod_org = ? " & _
                    "Connect By Prior O.cod_org = cod_org_sup"

            sd.Open()
            dr = sd.ExecuteReader(sSQL, pCodOrg)
            While dr.Read
                Array.Resize(Of EntORGAO)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While
            dr.Close()
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return lst
    End Function

    Public Function ListaHierarquicaNome(ByVal sFiltro As String, ByVal sOrdenacao As String, ByVal pCodOrg As String, ByVal pSomenteAtivos As String, ByVal pNivelHierarquia As String) As EntORGAO()
        Dim lst() As EntORGAO = New EntORGAO() {}
        Dim dr As OracleDataReader
        Dim sSQL As String
        Dim sWhere As String = ""

        Try

            sFiltro = "%" + sFiltro + "%"
            'sWhere = "where level <= " & IIf(pNivelHierarquia = String.Empty, "9", pNivelHierarquia) & " and UPPER(nome_dtl) like ? "

            sWhere = "where UPPER(nome_dtl) like ? " & IIf(pNivelHierarquia = String.Empty, String.Empty, " and level <=" & pNivelHierarquia)


            If pSomenteAtivos = "S" Then
                sWhere = sWhere & " and ind_ativo='S' and ind_hab_prot = 'S' "
            End If

            sSQL = "Select O.COD_ORG, NOME, COMPL_NOME, NOME_DTL, NOME_REDU,  NOME_ABREV, COD_GRUP_LOC_ORG, NUM_SEQ_REL, " & _
                    "COD_ORG_HLL, COD_COM, NOME_LOGR,  NUM_LOGR, COMPL_LOGR, COD_CID, COD_BAI,  CEP,  DDD, TEL1, RAMAL1, " & _
                    "TEL2, RAMAL2, TEL_FAX, RAMAL_FAX, DT_INATIV, E_MAIL, COD_SIT_ORG, DT_INST, IND_ORG_OFICLZ, " & _
                    "IND_ORG_JUDIC, IND_ORG_EXT_TJ, TIP_GRAU, TXT_OBS, DT_END, COD_TIP_ORG, COD_ORG_SUP, TIP_VINC, " & _
                    "COD_AREA_ATU, IND_ATIVO,  TIP_ATRIB_BOL_CGJ, IND_HAB_PROT, NUM_MATR_MAG, COD_IMOV, TIP_AREA_ATIV, " & _
                    "IND_FISCAL, IND_INSTALADA, IND_ORG_FORMAL, IND_SUB_LOT, IND_UNID_PATR, COD_GRUP_CAD_GPES, IND_SOLUC, " & _
                    "TIP_VAGA_TIT, COD_TIP_LOGR, NUM_BLOC_LOGR, NUM_AND_LOGR, NUM_CORR_LOGR, NUM_SALA_LOGR, TXT_REFER_LOGR, " & _
                    "CNPJ, OBS_RELS, IND_VAGA_TITULAR, IND_INSALUBRI, IND_RESOLUCAO_ADM_JUD,  IND_PREV_VAGA_TITULAR, " & _
                    "COD_LEI_LEGISLATIVO, DT_PUBLIC_LEGISLATIVO, NUM_ORGAO, ZONA, DISTRITO,  SUBDISTRITO, CIRCUNSCRICAO, " & _
                    "NOME_OFICIAL, DESCR_INSTALA, COD_TIPO_SERVENTIA, COD_TIPO_VAGA_TIT, COD_MODIFICA_ORGAO, COD_QUALIFICA, " & _
                    "COD_CATEGORIA, COD_NIVEL_ESTRUTURA, COD_TIP_DENOM_SERV,COD_POS_ORG, USU_ATUALZ, COD_ORG_NOVO, COD_CLASS, " & _
                    "DESCR_SETOR_PROTOCOLO Descr_Grup_Cad_Gpes " & _
                    "from ORGAO O,SETORPROTOCOLO SP " & sWhere & _
                    " and sp.COD_SETOR_PROTOCOLO(+) = COD_GRUP_CAD_GPES " & _
                    "Start With O.cod_org = ? " & _
                    "Connect By Prior cod_org_sup = O.cod_org " & _
                    IIf(sOrdenacao = String.Empty, String.Empty, " order by " + sOrdenacao)

            sd.Open()
            dr = sd.ExecuteReader(sSQL, sFiltro, pCodOrg)
            While dr.Read
                Array.Resize(Of EntORGAO)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While
            dr.Close()
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return lst
    End Function

    Public Function Consultar(ByVal pCodOrg As Decimal) As EntORGAO
        Dim nState As ConnectionState
        Dim sSQL As String
        'sSQL = "select * from ORGAO where COD_ORG = ? and ind_ativo='S' and ind_hab_prot = 'S'"
        sSQL = "Select O.COD_ORG, NOME, COMPL_NOME, NOME_DTL, NOME_REDU,  NOME_ABREV, COD_GRUP_LOC_ORG, NUM_SEQ_REL, " & _
                "COD_ORG_HLL, COD_COM, NOME_LOGR,  NUM_LOGR, COMPL_LOGR, COD_CID, COD_BAI,  CEP,  DDD, TEL1, RAMAL1, " & _
                "TEL2, RAMAL2, TEL_FAX, RAMAL_FAX, DT_INATIV, E_MAIL, COD_SIT_ORG, DT_INST, IND_ORG_OFICLZ, " & _
                "IND_ORG_JUDIC, IND_ORG_EXT_TJ, TIP_GRAU, TXT_OBS, DT_END, COD_TIP_ORG, COD_ORG_SUP, TIP_VINC, " & _
                "COD_AREA_ATU, IND_ATIVO,  TIP_ATRIB_BOL_CGJ, IND_HAB_PROT, NUM_MATR_MAG, COD_IMOV, TIP_AREA_ATIV, " & _
                "IND_FISCAL, IND_INSTALADA, IND_ORG_FORMAL, IND_SUB_LOT, IND_UNID_PATR, COD_GRUP_CAD_GPES, IND_SOLUC, " & _
                "TIP_VAGA_TIT, COD_TIP_LOGR, NUM_BLOC_LOGR, NUM_AND_LOGR, NUM_CORR_LOGR, NUM_SALA_LOGR, TXT_REFER_LOGR, " & _
                "CNPJ, OBS_RELS, IND_VAGA_TITULAR, IND_INSALUBRI, IND_RESOLUCAO_ADM_JUD,  IND_PREV_VAGA_TITULAR, " & _
                "COD_LEI_LEGISLATIVO, DT_PUBLIC_LEGISLATIVO, NUM_ORGAO, ZONA, DISTRITO,  SUBDISTRITO, CIRCUNSCRICAO, " & _
                "NOME_OFICIAL, DESCR_INSTALA, COD_TIPO_SERVENTIA, COD_TIPO_VAGA_TIT, COD_MODIFICA_ORGAO, COD_QUALIFICA, " & _
                "COD_CATEGORIA, COD_NIVEL_ESTRUTURA, COD_TIP_DENOM_SERV,COD_POS_ORG, USU_ATUALZ, COD_ORG_NOVO, COD_CLASS, " & _
                "DESCR_SETOR_PROTOCOLO DESCR_GRUP_CAD_GPES " & _
                "from ORGAO O,SETORPROTOCOLO SP " & _
                "where  O.COD_ORG = ? and ind_ativo='S' and ind_hab_prot = 'S' and sp.COD_SETOR_PROTOCOLO(+) = COD_GRUP_CAD_GPES "
        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntORGAO
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pCodOrg)
            If dr.Read Then
                ent = GerarEntidade(dr)
            End If
            dr.Close()
            Return ent
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


    Public Function ConsultarHierarquica(ByVal pCodOrgSup As Decimal, ByVal pCodOrg As Decimal, ByVal pSomenteAtivos As String, ByVal pNivelHierarquia As String) As EntORGAO
        Dim nState As ConnectionState
        Dim sSQL As String
        Dim sWhere As String = ""

        sWhere = " where o.cod_org = ? " & IIf(pNivelHierarquia = String.Empty, String.Empty, " and level <=" & pNivelHierarquia)

        If pSomenteAtivos = "S" Then
            sWhere = sWhere & " and ind_ativo='S' and ind_hab_prot = 'S' "
        End If

        'sSQL = "select * from ORGAO where COD_ORG = ? and ind_ativo='S' and ind_hab_prot = 'S'"
        sSQL = "Select O.COD_ORG, NOME, COMPL_NOME, NOME_DTL, NOME_REDU,  NOME_ABREV, COD_GRUP_LOC_ORG, NUM_SEQ_REL, " & _
                "COD_ORG_HLL, COD_COM, NOME_LOGR,  NUM_LOGR, COMPL_LOGR, COD_CID, COD_BAI,  CEP,  DDD, TEL1, RAMAL1, " & _
                "TEL2, RAMAL2, TEL_FAX, RAMAL_FAX, DT_INATIV, E_MAIL, COD_SIT_ORG, DT_INST, IND_ORG_OFICLZ, " & _
                "IND_ORG_JUDIC, IND_ORG_EXT_TJ, TIP_GRAU, TXT_OBS, DT_END, COD_TIP_ORG, COD_ORG_SUP, TIP_VINC, " & _
                "COD_AREA_ATU, IND_ATIVO,  TIP_ATRIB_BOL_CGJ, IND_HAB_PROT, NUM_MATR_MAG, COD_IMOV, TIP_AREA_ATIV, " & _
                "IND_FISCAL, IND_INSTALADA, IND_ORG_FORMAL, IND_SUB_LOT, IND_UNID_PATR, COD_GRUP_CAD_GPES, IND_SOLUC, " & _
                "TIP_VAGA_TIT, COD_TIP_LOGR, NUM_BLOC_LOGR, NUM_AND_LOGR, NUM_CORR_LOGR, NUM_SALA_LOGR, TXT_REFER_LOGR, " & _
                "CNPJ, OBS_RELS, IND_VAGA_TITULAR, IND_INSALUBRI, IND_RESOLUCAO_ADM_JUD,  IND_PREV_VAGA_TITULAR, " & _
                "COD_LEI_LEGISLATIVO, DT_PUBLIC_LEGISLATIVO, NUM_ORGAO, ZONA, DISTRITO,  SUBDISTRITO, CIRCUNSCRICAO, " & _
                "NOME_OFICIAL, DESCR_INSTALA, COD_TIPO_SERVENTIA, COD_TIPO_VAGA_TIT, COD_MODIFICA_ORGAO, COD_QUALIFICA, " & _
                "COD_CATEGORIA, COD_NIVEL_ESTRUTURA, COD_TIP_DENOM_SERV,COD_POS_ORG, USU_ATUALZ, COD_ORG_NOVO, COD_CLASS, " & _
                "DESCR_SETOR_PROTOCOLO DESCR_GRUP_CAD_GPES " & _
                "from ORGAO O,SETORPROTOCOLO SP " & sWhere & _
                " and sp.COD_SETOR_PROTOCOLO(+) = COD_GRUP_CAD_GPES " & _
                "Start With O.cod_org = ? " & _
                "Connect By Prior cod_org_sup = O.cod_org "
        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntORGAO
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pCodOrg, pCodOrgSup)
            If dr.Read Then
                ent = GerarEntidade(dr)
            End If
            dr.Close()
            Return ent
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

    Public Function ListarOrgaoVincEstruturaDJE(ByVal sFiltro As String, ByVal sOrdenacao As String) As EntORGAO()
        Dim lst() As EntORGAO = New EntORGAO() {}
        Dim dr As OracleDataReader
        Dim sSQL As String

        sFiltro = "%" + sFiltro + "%"

        sSQL = "Select distinct O.COD_ORG, O.NOME, COMPL_NOME, NOME_DTL, NOME_REDU,  NOME_ABREV, COD_GRUP_LOC_ORG, NUM_SEQ_REL, " & _
                   "COD_ORG_HLL, COD_COM, NOME_LOGR,  NUM_LOGR, COMPL_LOGR, COD_CID, COD_BAI,  CEP,  DDD, TEL1, RAMAL1, " & _
                   "TEL2, RAMAL2, TEL_FAX, RAMAL_FAX, DT_INATIV, E_MAIL, COD_SIT_ORG, DT_INST, IND_ORG_OFICLZ, " & _
                   "IND_ORG_JUDIC, IND_ORG_EXT_TJ, TIP_GRAU, TXT_OBS, DT_END, COD_TIP_ORG, COD_ORG_SUP, TIP_VINC, " & _
                   "COD_AREA_ATU, o.IND_ATIVO,  TIP_ATRIB_BOL_CGJ, IND_HAB_PROT, NUM_MATR_MAG, COD_IMOV, TIP_AREA_ATIV, " & _
                   "IND_FISCAL, IND_INSTALADA, IND_ORG_FORMAL, IND_SUB_LOT, IND_UNID_PATR, COD_GRUP_CAD_GPES, IND_SOLUC, " & _
                   "TIP_VAGA_TIT, COD_TIP_LOGR, NUM_BLOC_LOGR, NUM_AND_LOGR, NUM_CORR_LOGR, NUM_SALA_LOGR, TXT_REFER_LOGR, " & _
                   "CNPJ, OBS_RELS, IND_VAGA_TITULAR, IND_INSALUBRI, IND_RESOLUCAO_ADM_JUD,  IND_PREV_VAGA_TITULAR, " & _
                   "COD_LEI_LEGISLATIVO, DT_PUBLIC_LEGISLATIVO, NUM_ORGAO, ZONA, DISTRITO,  SUBDISTRITO, CIRCUNSCRICAO, " & _
                   "NOME_OFICIAL, DESCR_INSTALA, COD_TIPO_SERVENTIA, COD_TIPO_VAGA_TIT, COD_MODIFICA_ORGAO, COD_QUALIFICA, " & _
                   "COD_CATEGORIA, COD_NIVEL_ESTRUTURA, COD_TIP_DENOM_SERV,COD_POS_ORG, USU_ATUALZ, COD_ORG_NOVO, COD_CLASS, " & _
                   " '' DESCR_GRUP_CAD_GPES " & _
                   "from ORGAO O, ESTRUTURADJE E " & _
                   "where o.ind_ativo='S' and e.ind_ativo='S' and o.cod_org = e.cod_org and upper(O.NOME_DTL) Like '" & sFiltro & "' " & IIf(sOrdenacao = String.Empty, String.Empty, " order by " & sOrdenacao)
        Try
            sd.Open()
            dr = sd.ExecuteReader(sSQL)
            While dr.Read
                Array.Resize(Of EntORGAO)(lst, lst.Length + 1)
                lst(lst.Length - 1) = GerarEntidade(dr)
            End While
            dr.Close()
        Catch ex As Exception
            Throw ex
        Finally
            sd.Close()
        End Try

        Return lst
    End Function

    Public Function ConsultarOrgaoVincEstruturaDJE(ByVal pCodOrg As Decimal) As EntORGAO
        Dim nState As ConnectionState
        Dim sSQL As String
        ' Dim sWhere As String = ""

        sSQL = "Select distinct O.COD_ORG, O.NOME, COMPL_NOME, NOME_DTL, NOME_REDU,  NOME_ABREV, COD_GRUP_LOC_ORG, NUM_SEQ_REL, " & _
                 "COD_ORG_HLL, COD_COM, NOME_LOGR,  NUM_LOGR, COMPL_LOGR, COD_CID, COD_BAI,  CEP,  DDD, TEL1, RAMAL1, " & _
                 "TEL2, RAMAL2, TEL_FAX, RAMAL_FAX, DT_INATIV, E_MAIL, COD_SIT_ORG, DT_INST, IND_ORG_OFICLZ, " & _
                 "IND_ORG_JUDIC, IND_ORG_EXT_TJ, TIP_GRAU, TXT_OBS, DT_END, COD_TIP_ORG, COD_ORG_SUP, TIP_VINC, " & _
                 "COD_AREA_ATU, o.IND_ATIVO,  TIP_ATRIB_BOL_CGJ, IND_HAB_PROT, NUM_MATR_MAG, COD_IMOV, TIP_AREA_ATIV, " & _
                 "IND_FISCAL, IND_INSTALADA, IND_ORG_FORMAL, IND_SUB_LOT, IND_UNID_PATR, COD_GRUP_CAD_GPES, IND_SOLUC, " & _
                 "TIP_VAGA_TIT, COD_TIP_LOGR, NUM_BLOC_LOGR, NUM_AND_LOGR, NUM_CORR_LOGR, NUM_SALA_LOGR, TXT_REFER_LOGR, " & _
                 "CNPJ, OBS_RELS, IND_VAGA_TITULAR, IND_INSALUBRI, IND_RESOLUCAO_ADM_JUD,  IND_PREV_VAGA_TITULAR, " & _
                 "COD_LEI_LEGISLATIVO, DT_PUBLIC_LEGISLATIVO, NUM_ORGAO, ZONA, DISTRITO,  SUBDISTRITO, CIRCUNSCRICAO, " & _
                 "NOME_OFICIAL, DESCR_INSTALA, COD_TIPO_SERVENTIA, COD_TIPO_VAGA_TIT, COD_MODIFICA_ORGAO, COD_QUALIFICA, " & _
                 "COD_CATEGORIA, COD_NIVEL_ESTRUTURA, COD_TIP_DENOM_SERV,COD_POS_ORG, USU_ATUALZ, COD_ORG_NOVO, COD_CLASS, " & _
                 " '' DESCR_GRUP_CAD_GPES " & _
                 "from ORGAO O, ESTRUTURADJE E " & _
                 "where o.ind_ativo='S' and e.ind_ativo='S' and o.cod_org = e.cod_org and o.cod_org = ?"

        Try
            nState = sd.State
            sd.Open()
            Dim ent As New EntORGAO
            Dim dr As OracleDataReader = sd.ExecuteReader(sSQL, pCodOrg)
            If dr.Read Then
                ent = GerarEntidade(dr)
            End If
            dr.Close()
            Return ent
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

    Private Function GerarEntidade(ByVal row As OracleDataReader) As EntORGAO
        Dim Ent As New EntORGAO

        Ent.CodOrg = CInt(NVL(row("COD_ORG"), 0))
        Ent.Nome = CStr(NVL(row("NOME"), String.Empty))
        Ent.ComplNome = CStr(NVL(row("COMPL_NOME"), String.Empty))
        Ent.NomeDtl = CStr(NVL(row("NOME_DTL"), String.Empty))
        Ent.NomeRedu = CStr(NVL(row("NOME_REDU"), String.Empty))
        Ent.NomeAbrev = CStr(NVL(row("NOME_ABREV"), String.Empty))
        Ent.CodGrupLocOrg = CInt(NVL(row("COD_GRUP_LOC_ORG"), 0))
        Ent.NumSeqRel = CStr(NVL(row("NUM_SEQ_REL"), String.Empty))
        Ent.CodOrgHll = CStr(NVL(row("COD_ORG_HLL"), String.Empty))
        Ent.CodCom = CInt(NVL(row("COD_COM"), 0))
        Ent.NomeLogr = CStr(NVL(row("NOME_LOGR"), String.Empty))
        Ent.NumLogr = CStr(NVL(row("NUM_LOGR"), String.Empty))
        Ent.ComplLogr = CStr(NVL(row("COMPL_LOGR"), String.Empty))
        Ent.CodCid = CLng(NVL(row("COD_CID"), 0))
        Ent.CodBai = CInt(NVL(row("COD_BAI"), 0))
        Ent.Cep = CInt(NVL(row("CEP"), 0))
        Ent.Ddd = CStr(NVL(row("DDD"), String.Empty))
        Ent.Tel1 = CStr(NVL(row("TEL1"), String.Empty))
        Ent.Ramal1 = CStr(NVL(row("RAMAL1"), String.Empty))
        Ent.Tel2 = CStr(NVL(row("TEL2"), String.Empty))
        Ent.Ramal2 = CStr(NVL(row("RAMAL2"), String.Empty))
        Ent.TelFax = CStr(NVL(row("TEL_FAX"), String.Empty))
        Ent.RamalFax = CStr(NVL(row("RAMAL_FAX"), String.Empty))
        Ent.DtInativ = CDate(NVL(row("DT_INATIV"), #12:00:00 AM#))
        Ent.EMail = CStr(NVL(row("E_MAIL"), String.Empty))
        Ent.CodSitOrg = CInt(NVL(row("COD_SIT_ORG"), 0))
        Ent.DtInst = CDate(NVL(row("DT_INST"), #12:00:00 AM#))
        Ent.IndOrgOficlz = CStr(NVL(row("IND_ORG_OFICLZ"), String.Empty))
        Ent.IndOrgJudic = CStr(NVL(row("IND_ORG_JUDIC"), String.Empty))
        Ent.IndOrgExtTj = CStr(NVL(row("IND_ORG_EXT_TJ"), String.Empty))
        Ent.TipGrau = CStr(NVL(row("TIP_GRAU"), String.Empty))
        Ent.TxtObs = CStr(NVL(row("TXT_OBS"), String.Empty))
        Ent.DtEnd = CDate(NVL(row("DT_END"), #12:00:00 AM#))
        Ent.CodTipOrg = CInt(NVL(row("COD_TIP_ORG"), 0))
        Ent.CodOrgSup = CInt(NVL(row("COD_ORG_SUP"), 0))
        Ent.TipVinc = CInt(NVL(row("TIP_VINC"), 0))
        Ent.CodAreaAtu = CInt(NVL(row("COD_AREA_ATU"), 0))
        Ent.IndAtivo = CStr(NVL(row("IND_ATIVO"), String.Empty))
        Ent.TipAtribBolCgj = CInt(NVL(row("TIP_ATRIB_BOL_CGJ"), 0))
        Ent.IndHabProt = CStr(NVL(row("IND_HAB_PROT"), String.Empty))
        Ent.NumMatrMag = CStr(NVL(row("NUM_MATR_MAG"), String.Empty))
        Ent.CodImov = CInt(NVL(row("COD_IMOV"), 0))
        Ent.TipAreaAtiv = CStr(NVL(row("TIP_AREA_ATIV"), String.Empty))
        Ent.IndFiscal = CStr(NVL(row("IND_FISCAL"), String.Empty))
        Ent.IndInstalada = CStr(NVL(row("IND_INSTALADA"), String.Empty))
        Ent.IndOrgFormal = CStr(NVL(row("IND_ORG_FORMAL"), String.Empty))
        Ent.IndSubLot = CStr(NVL(row("IND_SUB_LOT"), String.Empty))
        Ent.IndUnidPatr = CStr(NVL(row("IND_UNID_PATR"), String.Empty))
        Ent.CodGrupCadGpes = CInt(NVL(row("COD_GRUP_CAD_GPES"), 0))
        Ent.IndSoluc = CStr(NVL(row("IND_SOLUC"), String.Empty))
        Ent.TipVagaTit = CStr(NVL(row("TIP_VAGA_TIT"), String.Empty))
        Ent.DescrGrupCadGpes = CStr(NVL(row("Descr_Grup_Cad_Gpes"), String.Empty))
        Return Ent
    End Function

    ' Função utilizada pelo controle DGTECCodNome do ClientePadrao
    Public Function PesqPorDescr(ByVal str As String) As DataSet
        Return sd.CreateDataSet("Select COD_ORG, NOME from ORGAO where NOME like ? ", str)
    End Function

End Class
