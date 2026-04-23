using System.Windows.Forms;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Teleprompter
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

        //Configurando Cor do texto
        private void btnCorTexto_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dialogoCor = new ColorDialog();
            if (dialogoCor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color corSelecionada = dialogoCor.Color;
                string hex = $"#{corSelecionada.R:X2}{corSelecionada.G:X2}{corSelecionada.B:X2}";
                btnCorTexto.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hex));
                btnCorTexto.Tag = hex; // Armazena a cor selecionada para uso posterior
            }
        }

        // Configurando cor de fundo
        private void btnCorFundo_Click(object sender, RoutedEventArgs e)
        {
            ColorDialog dialogoCor = new ColorDialog();
            if (dialogoCor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                System.Drawing.Color corSelecionada = dialogoCor.Color;
                string hex = $"#{corSelecionada.R:X2}{corSelecionada.G:X2}{corSelecionada.B:X2}";
                btnCorFundo.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(hex));
                btnCorFundo.Tag = hex; 
            }   
        }

        //Começa o teleprompter
        private void btnIniciar_Click(object sender, RoutedEventArgs e)
        {
            //Obrigatório ter algum texto para iniciar
            if (string.IsNullOrWhiteSpace(txtRoteiro.Text))
            {
                System.Windows.Forms.MessageBox.Show("Por favor, insira o texto para o teleprompter.", "Erro", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            string texto = txtRoteiro.Text;
            string fonte = (cmbFonte.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Arial";
            double tamanho = double.Parse((cmbTamanhoFonte.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "56");
            double velocidade = sldVelocidade.Value;
            string corTexto = btnCorTexto.Tag as string ?? "#FFFFFF";
            string corFundo = btnCorFundo.Tag as string ?? "#000000";

            JanelaTeleprompter janela = new JanelaTeleprompter(texto, fonte, tamanho, velocidade, corTexto, corFundo);
            janela.Show();
        }
    }
}