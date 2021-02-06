using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaAtx
{
    static class Program
    {
        //DECLARAR AS VARIAVEIS GLOBAIS DO SISTEMA
        public static string nomeUsuario;
        public static string statusAtivacao;
        public static string cargoUsuario;
        public static string codigoAtivacao;
        public static string nomePc;
        public static string chamadaClientes;



        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login.FrmSplash());
        }
    }
}
