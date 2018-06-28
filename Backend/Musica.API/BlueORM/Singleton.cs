using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueORM
{
    public static class Singleton
    {
        private static bool instancia = false;
        private static int codUsuario;
        private static int codEtiqueta;
        private static string nomeUsuario;
        private static string email;
        private static string sistema;
        private static string versao;
        private static DateTime dataVersao;
        private static int tempoParadaMS;
        private static bool classificaFalhaOperacional;
        private static bool classificaGrauContusao;
        private static bool classificaDenticao;


        private static string ip;
        private static string banco;

        private static int ponto;
        private static string executavel;
        private static int codFilial;

        private static string acessoTemplateServer;
        private static string acessoTemplateLocal;
        private static int acessoQtdReadCad;
        private static bool indLeituraDigital;

        private static string descFilial;
        private static bool sistemaPesagemAutomatica;
        private static bool sistemaCorrecaoEstabilizacao;
        private static bool sistemaUtilizaDllToledo;
        private static string sistemaFatorConversao;
        private static int impTraseiro;
        private static int impDianteiro;
        private static int impPA;
        private static bool calhaImpTraseiro_A, calhaImpDianteiro_A, calhaImpPA_A;
        private static bool calhaImpTraseiro_B, calhaImpDianteiro_B, calhaImpPA_B;

        private static decimal afericaoAbatePeso, afericaoAbateTolerancia;

        private static decimal tara;

        private static bool indManejo;

        public static string Email
        {
            get { return Singleton.email; }
        }

        public static string NomeUsuario
        {
            get { return Singleton.nomeUsuario; }
        }

        public static int CodUsuario
        {
            get { return Singleton.codUsuario; }
        }

        public static int CodEtiqueta
        {
            get { return Singleton.codEtiqueta; }
        }

        public static string Sistema
        {
            get { return Singleton.sistema; }
        }

        public static string Versao
        {
            get { return Singleton.versao; }
        }

        public static DateTime DataVersao
        {
            get { return Singleton.dataVersao; }
        }

        public static int TempoParadaMS
        {
            get { return Singleton.tempoParadaMS; }
        }

        public static bool ClassificaFalhaOperacional
        {
            get { return Singleton.classificaFalhaOperacional; }
        }

        public static bool ClassificaGrauContusao
        {
            get { return Singleton.classificaGrauContusao; }
        }

        public static bool ClassificaDenticao
        {
            get { return Singleton.classificaDenticao; }
        }

        public static decimal AfericaoAbatePeso
        {
            get { return Singleton.afericaoAbatePeso; }
        }

        public static decimal AfericaoAbateTolerancia
        {
            get { return Singleton.afericaoAbateTolerancia; }
        }

        public static decimal Tara
        {
            get { return Singleton.tara; }
        }

        public static bool IndManejo
        {
            get { return Singleton.indManejo; }
        }

        public static string IP
        {
            get { return Singleton.ip; }
            set { Singleton.ip = value; }
        }

        public static string Banco
        {
            get { return Singleton.banco; }
            set { Singleton.banco = value; }
        }

        public static int Ponto
        {
            get { return Singleton.ponto; }
            set { Singleton.ponto = value; }
        }

        public static string Executavel
        {
            get { return Singleton.executavel; }
            set { Singleton.executavel = value; }
        }

        public static int CodFilial
        {
            get { return Singleton.codFilial; }
            set { Singleton.codFilial = value; }
        }

        public static string AcessoTemplateServer
        {
            get { return Singleton.acessoTemplateServer; }
            set { Singleton.acessoTemplateServer = value; }
        }

        public static string AcessoTemplateLocal
        {
            get { return Singleton.acessoTemplateLocal; }
            set { Singleton.acessoTemplateLocal = value; }
        }

        public static int AcessoQtdReadCad
        {
            get { return Singleton.acessoQtdReadCad; }
            set { Singleton.acessoQtdReadCad = value; }
        }

        public static bool IndLeituraDigital
        {
            get { return Singleton.indLeituraDigital; }
            set { Singleton.indLeituraDigital = value; }
        }



        public static string DescFilial
        {
            get { return Singleton.descFilial; }
            set { Singleton.descFilial = value; }
        }

        public static bool SistemaPesagemAutomatica
        {
            get { return Singleton.sistemaPesagemAutomatica; }
            set { Singleton.sistemaPesagemAutomatica = value; }
        }

        public static bool SistemaCorrecaoEstabilizacao
        {
            get { return Singleton.sistemaCorrecaoEstabilizacao; }
            set { Singleton.sistemaCorrecaoEstabilizacao = value; }
        }

        public static bool SistemaUtilizaDllToledo
        {
            get { return Singleton.sistemaUtilizaDllToledo; }
            set { Singleton.sistemaUtilizaDllToledo = value; }
        }

        public static string SistemaFatorConversao
        {
            get { return Singleton.sistemaFatorConversao; }
            set { Singleton.sistemaFatorConversao = value; }
        }

        public static int ImpTraseiro
        {
            get { return Singleton.impTraseiro; }
            set { Singleton.impTraseiro = value; }
        }

        public static int ImpDianteiro
        {
            get { return Singleton.impDianteiro; }
            set { Singleton.impDianteiro = value; }
        }

        public static int ImpPA
        {
            get { return Singleton.impPA; }
            set { Singleton.impPA = value; }
        }

        public static bool CalhaImpTraseiro_A
        {
            get { return Singleton.calhaImpTraseiro_A; }
            set { Singleton.calhaImpTraseiro_A = value; }
        }

        public static bool CalhaImpDianteiro_A
        {
            get { return Singleton.calhaImpDianteiro_A; }
            set { Singleton.calhaImpDianteiro_A = value; }
        }

        public static bool CalhaImpPA_A
        {
            get { return Singleton.calhaImpPA_A; }
            set { Singleton.calhaImpPA_A = value; }
        }



        public static bool CalhaImpTraseiro_B
        {
            get { return Singleton.calhaImpTraseiro_B; }
            set { Singleton.calhaImpTraseiro_B = value; }
        }

        public static bool CalhaImpDianteiro_B
        {
            get { return Singleton.calhaImpDianteiro_B; }
            set { Singleton.calhaImpDianteiro_B = value; }
        }

        public static bool CalhaImpPA_B
        {
            get { return Singleton.calhaImpPA_B; }
            set { Singleton.calhaImpPA_B = value; }
        }



        public static void SetIntance(int codUsuario, string nomeUsuario, string email, string sistema, string versao, DateTime dataVersao, int tempoParadaMS, decimal afericaoAbatePeso, decimal afericaoAbateTolerancia, decimal tara, bool indManejo, int codEtiqueta, bool classificaFalhaOperacional, bool classificaGrauContusao, bool classificaDenticao)
        {
            if (!Singleton.instancia)
            {
                Singleton.codUsuario = codUsuario;
                Singleton.nomeUsuario = nomeUsuario;
                Singleton.email = email;
                Singleton.sistema = sistema;
                Singleton.versao = versao;
                Singleton.dataVersao = dataVersao;
                Singleton.tempoParadaMS = tempoParadaMS;
                Singleton.instancia = true;
                Singleton.afericaoAbatePeso = afericaoAbatePeso;
                Singleton.afericaoAbateTolerancia = afericaoAbateTolerancia;
                Singleton.tara = tara;
                Singleton.indManejo = indManejo;
                Singleton.codEtiqueta = codEtiqueta;
                Singleton.classificaFalhaOperacional = classificaFalhaOperacional;
                Singleton.classificaGrauContusao = classificaGrauContusao;
                Singleton.classificaDenticao = classificaDenticao;
            }
        }
    }
}