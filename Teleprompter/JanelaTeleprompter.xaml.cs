using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Teleprompter
{
    /// <summary>
    /// Lógica interna para JanelaTeleprompter.xaml
    /// </summary>
    public partial class JanelaTeleprompter : Window
    {
        private DispatcherTimer _timer;
        private double _velocidade;
        private double _posicaoY;

        public JanelaTeleprompter(
            string texto,
            string fonte,
            double tamanhoFonte,
            double velocidade,
            string corTexto,
            string corFundo)
        {
            InitializeComponent();

            txtTeleprompter.Text = texto;
            txtTeleprompter.FontFamily = new System.Windows.Media.FontFamily(fonte);
            txtTeleprompter.FontSize = tamanhoFonte;
            txtTeleprompter.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(corTexto));
            this.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(corFundo));
            _velocidade = velocidade;

            //Inverter o texto para sair correto no teleprompter, afinal ele é espelhado....e, particulamente, ele não é nada leve
            txtTeleprompter.RenderTransform = new ScaleTransform(-1, 1);
            txtTeleprompter.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);

            // Configurar o timer para atualizar a posição do texto
           this.Loaded += (s, e) =>
            {
                _posicaoY = this.ActualHeight; // Começa na parte inferior da janela
                txtTeleprompter.Width = this.ActualWidth -160;
                Canvas.SetTop(txtTeleprompter, _posicaoY);
                _timer?.Start();
            };

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(16); // Atualiza a cada 16ms (~60 FPS)
            _timer.Tick += Timer_Texto;
            _timer.Start();
        }

        private void Timer_Texto(object? sender, EventArgs e)
        {
            _posicaoY -= _velocidade; // Move o texto para cima
            Canvas.SetTop(txtTeleprompter, _posicaoY);
            // Se o texto sair completamente da tela, reinicia a posição
            if (_posicaoY < -txtTeleprompter.ActualHeight)
            {
                _timer.Stop();
            }
        }
            

        private void Passador_Slide (object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.PageDown)
            {
                _velocidade = Math.Abs(_velocidade);

            }else if (e.Key == Key.PageUp)
            {
                _velocidade = -Math.Abs(_velocidade);

            }else if(e.Key == Key.Escape)
            {
                _timer.Stop();
                this.Close();
            }
        }
    } 
}
