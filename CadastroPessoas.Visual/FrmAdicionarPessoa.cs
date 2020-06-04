using CadastroPessoas.Dominio;
using CadastroPessoas.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroPessoas.Visual
{
    public partial class FrmAdicionarPessoa : Form
    {
        public FrmAdicionarPessoa()
        {
            InitializeComponent();
        }

        private void FrmAdicionarPessoa_Load(object sender, EventArgs e)
        {

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Pessoa pessoa = new Pessoa
            {
                Nome = txbNome.Text,
                Idade = Convert.ToInt32(txbIdade.Text),
                Endereco = txbEndereco.Text
            };
            IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
            repositorioPessoas.Adicionar(pessoa, (linhasAfetadas) => {
                MessageBox.Show(string.Format("Foram inseridos {0} registros", linhasAfetadas));
            });
            Close();
        }
    }
}
