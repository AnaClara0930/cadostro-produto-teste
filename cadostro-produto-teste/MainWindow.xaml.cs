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

namespace cadostro_produto_teste
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Base de dados da memória
        List<Produto> ListaDeProdutos = new List<Produto>();

        public MainWindow()
        {
            InitializeComponent();
            /*
            Produto produto = new Produto()
            {
                id = 1,
                nome = "Notebook",
                descricao = "Notebook de 4gb",
                quantidade = 100,
                fabricante = "Positivo"
            };
            ListaDeProdutos.Add(produto);

            // Atualizar o datagrid
            dvgProdutos.ItemsSource = ListaDeProdutos;
            dvgProdutos.Items.Refresh();*/
            
        }

        private void NovoProduto(object sender, RoutedEventArgs e)
        {
            if (VerificaCampos() == true)
            {
                AdicionaProduto();
                AtualizaDataGrid();
                MessageBoxResult result = MessageBox.Show(
                    "Produto cadastrado",
                    "Atenção",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                LimpaTodosOsCampos();
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(
                "Preencha todos os campos!",
                "Atenção",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
            }
        }

        private void AdicionaProduto()
        {
            Produto produto = new Produto();
            produto.id = RetornaUltimoIncrementadoDaLista();
            produto.nome = txtNome.Text;
            produto.descricao = txtDescricao.Text;
            produto.fabricante = txtFabricante.Text;
            produto.quantidade = int.Parse(txtQuantidade.Text);
            ListaDeProdutos.Add(produto);
            dvgProdutos.ItemsSource = ListaDeProdutos;
            dvgProdutos.Items.Refresh();
        }

        private void AtualizaDataGrid()
        {
            dvgProdutos.ItemsSource = ListaDeProdutos;
            dvgProdutos.Items.Refresh();
        }
        
        private bool VerificaCampos()
        {
            if (txtNome.Text !="" && txtDescricao.Text !="" 
                && txtFabricante.Text !="" && txtQuantidade.Text !="")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int RetornaUltimoIncrementadoDaLista()
        {
            int id = 0;
            if(ListaDeProdutos.Count > 0)
            {
                int index = ListaDeProdutos.Count - 1;
                id = ListaDeProdutos[index].id;
            }
            id++;
            return id;
        }

        private void LimpaTodosOsCampos()
        {
            txtId.Text = "";
            txtNome.Text = "";
            txtDescricao.Text = "";
            txtFabricante.Text = "";
            txtQuantidade.Text = "";
        }

        private void AtualizarProduto(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                MessageBoxResult result = MessageBox.Show(
                    $"Deseja alterar o produto id:{id}",
                    "Alterar Produto",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {

                    for (int i = 0; i < ListaDeProdutos.Count; i++)
                    {
                        if (ListaDeProdutos[i].id == id)
                        {
                            ListaDeProdutos[i].nome = txtNome.Text;
                            ListaDeProdutos[i].descricao = txtDescricao.Text;
                            ListaDeProdutos[i].fabricante = txtFabricante.Text;
                            ListaDeProdutos[i].quantidade = int.Parse(txtQuantidade.Text);
                            break;
                        }
                    }
                    foreach (Produto produto in ListaDeProdutos)
                    {
                        if (produto.id == id)
                        {
                            ListaDeProdutos.Remove(produto);
                            break;
                        }
                    }

                    AtualizaDataGrid();

                    MessageBoxResult result2 = MessageBox.Show(
                    "Produto atualizado!",
                    "Informação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                    LimpaTodosOsCampos();
                }
            }
        }

        private void ExcluirProduto(object sender, RoutedEventArgs e)
        {
            if(txtId.Text != "")
            {
                int id = int.Parse(txtId.Text);
                MessageBoxResult result = MessageBox.Show(
                    $"Deseja excluir o produto id:{id}",
                    "Excluir Produto",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);    
                if(result == MessageBoxResult.Yes)
                {
                   foreach(Produto produto in ListaDeProdutos)
                   {
                        if(produto.id == id)
                        {
                            ListaDeProdutos.Remove(produto);
                            break;
                        }
                   }

                   AtualizaDataGrid();

                    MessageBoxResult result2 = MessageBox.Show(
                    "Produto excluido!",
                    "Informação",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                    LimpaTodosOsCampos();
                }             
            }
        }

        private void LimparCampos(object sender, RoutedEventArgs e)
        {
            LimpaTodosOsCampos();
        }

        private void PegarItemNoGrid(object sender, MouseButtonEventArgs e)
        {
            Produto produto = (Produto)dvgProdutos.SelectedItem;
            txtId.Text = produto.id.ToString();
            txtNome.Text = produto.nome;
            txtDescricao.Text = produto.descricao;
            txtFabricante.Text = produto.fabricante;
            txtQuantidade.Text = produto.quantidade.ToString();
        }

        private void Sair(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
