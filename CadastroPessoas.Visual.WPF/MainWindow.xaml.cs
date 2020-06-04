using CadastroPessoas.Dominio;
using CadastroPessoas.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CadastroPessoas.Visual.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CarregarDataGrid()
        {
            IRepositorio<Pessoa> repositorioPessoas = new PessoaRepositorio();
            List<Pessoa> pessoas = repositorioPessoas.SelecionarTodos();
            dgvPessoas.ItemsSource = pessoas;
        }

        private void WdwMain_Loaded(object sender, RoutedEventArgs e)
        {
            CarregarDataGrid();
        }

        private void btnCadastrarPessoa_Click(object sender, RoutedEventArgs e)
        {
            WndCadastrarPessoa wndCadastrarPessoa = new WndCadastrarPessoa();
            wndCadastrarPessoa.ShowDialog();
            CarregarDataGrid();
        }
    }
}
