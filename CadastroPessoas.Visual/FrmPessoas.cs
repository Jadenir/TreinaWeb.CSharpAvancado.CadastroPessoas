using CadastroPessoas.Dominio;
using CadastroPessoas.Repositorio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastroPessoas.Visual
{
    public partial class FrmPessoas : Form
    {
        private List<Pessoa> _pessoas = new List<Pessoa>();
        private static readonly object locker = new Object();

        public FrmPessoas()
        {
            InitializeComponent();
        }

        private void FrmPessoas_Load(object sender, EventArgs e)
        {
            //Thread thread = new Thread(PreencherDataGridView);
            //thread.Start();
            //PreencherDataGridView();
            txbPesquisa.Text = string.Empty;
            PreencherDataGridView();
            //PreencherDataGridViewAsync();

        }
        private async void PreencherDataGridViewAsync()
        {
            int quantidadeLinhas = await PreencherDataGridView();
            dgvPessoas.Invoke((MethodInvoker)delegate
            {
                dgvPessoas.DataSource = _pessoas;
                dgvPessoas.Refresh();
            });
            MessageBox.Show(string.Format("Há {0} registros de pessoas", quantidadeLinhas));
        }
        private Task<int> PreencherDataGridView()
        {
            ////Thread.Sleep(5000);
            //Thread thread = new Thread(PreencherListPessoas);
            ////Thread thread2 = new Thread(PreencherListPessoas2);
            //thread.Start();
            ////thread2.Start();
            //thread.Join();
            ////thread2.Join();
            //dgvPessoas.Invoke((MethodInvoker)delegate { dgvPessoas.DataSource = _pessoas; dgvPessoas.Refresh(); });
            return Task<int>.Run(() =>
            {
                //Thread.Sleep(5000);
                IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
                _pessoas = new List<Pessoa>();
                List<Pessoa> temp = repositorioPessoas.SelecionarTodos();

                Parallel.ForEach(temp, (pessoa) =>
                {
                    pessoa.Nome += " - Paralelizar";
                    _pessoas.Add(pessoa);
                });
                return _pessoas.Count();
            });/*.ContinueWith((taskAnterior) =>
            {
                try
                {
                    dgvPessoas.Invoke((MethodInvoker)delegate
                    {
                        dgvPessoas.DataSource = _pessoas;
                        dgvPessoas.Refresh();
                    });
                    MessageBox.Show(string.Format("Há {0} registros de pessoas", taskAnterior.Result));
                }
                catch (AggregateException ex)
                {
                    foreach (Exception exception in ex.InnerExceptions)
                    {
                        MessageBox.Show(exception.Message);
                    }
                }
            });*/
        }
        private void PreencherListPessoas()
        {
            IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
            List<Pessoa> pessoas = repositorioPessoas.SelecionarTodos();
            lock (locker)
            {
                foreach (Pessoa p in pessoas)
                {
                    p.Nome += "Thread 1";
                    _pessoas.Add(p);
                    //Thread.Sleep(300);
                }
            }
        }
        //private void PreencherListPessoas2()
        //{
        //    try
        //    {
        //        throw new Exception("TESTE");
        //        IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
        //        List<Pessoa> pessoas = repositorioPessoas.SelecionarTodos();
        //        lock (locker)
        //        {
        //            foreach (Pessoa p in pessoas)
        //            {
        //                p.Nome += "Thread 2";
        //                _pessoas.Add(p);
        //                //Thread.Sleep(300);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ExibirErro(ex);
        //    }
        //}
        private void ExibirErro(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
        private void btnAdicionarPessoa_Click(object sender, EventArgs e)
        {
            FrmAdicionarPessoa frmAdicionarPessoa = new FrmAdicionarPessoa();
            frmAdicionarPessoa.ShowDialog();
            PreencherDataGridViewAsync();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
            dgvPessoas.DataSource = repositorioPessoas.Selecionar(pessoa => pessoa.Nome.Contains(txbPesquisa.Text));
            dgvPessoas.Refresh();
        }

        private void txbPesquisa_TextChanged(object sender, EventArgs e)
        {
            IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
            dgvPessoas.DataSource = repositorioPessoas.Selecionar(pessoa => pessoa.Nome.Contains(txbPesquisa.Text));
            dgvPessoas.Refresh();
        }
    }
}

