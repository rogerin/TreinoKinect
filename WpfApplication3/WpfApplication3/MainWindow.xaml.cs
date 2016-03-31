using System.Windows;

using Microsoft.Kinect;
using AuxiliarKinect.FuncoesBasicas;
using System.Linq;
using System;

namespace WpfApplication3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool MaoDireitaAcimaCabeca;

        public MainWindow()
        {
            InitializeComponent();
            InicializarSensor();
           


        }



        private void InicializarSensor()
        {
            KinectSensor kinect = InicializadorKinect.InicializarPrimeiroSensor(7);
            kinect.SkeletonStream.Enable();
            kinect.SkeletonFrameReady += KinectEvent;
        }

        private void KinectEvent(object sender, SkeletonFrameReadyEventArgs e)
        {
            using (SkeletonFrame quadroAtual = e.OpenSkeletonFrame())
            {
                if(quadroAtual != null)
                {
                    ExecutaRegraMaoDireitaAcimaDaCabeca(quadroAtual);
                }
            }
        }

        private void ExecutaRegraMaoDireitaAcimaDaCabeca (SkeletonFrame quadroAtual)
        {
            Skeleton[] esqueletos = new Skeleton[6];
            quadroAtual.CopySkeletonDataTo(esqueletos);

            Skeleton usuario = esqueletos.FirstOrDefault(esqueleto => esqueleto.TrackingState == SkeletonTrackingState.Tracked);


            if(usuario != null)
            {
                Joint maoDireita = usuario.Joints[JointType.HandRight];
                Joint maoEsquerda = usuario.Joints[JointType.HandLeft];
                Joint cabeca = usuario.Joints[JointType.Head];

                LmaoDireita.Content = maoDireita.Position.X.ToString();
                LmaoEsquerda.Content = maoEsquerda.Position.X.ToString();
                Lcabeca.Content = cabeca.Position.X.ToString();


                LmaoEsquerdaY.Content = maoEsquerda.Position.Y.ToString(); 
                LmaoDireitaY .Content = maoDireita.Position.Y.ToString();
                LcabecaY.Content = cabeca.Position.Y.ToString();





                bool novoTesteMaoDireitaAcimaCabeca = maoDireita.Position.Y > cabeca.Position.Y;

                if(MaoDireitaAcimaCabeca != novoTesteMaoDireitaAcimaCabeca)
                {
                    MaoDireitaAcimaCabeca = novoTesteMaoDireitaAcimaCabeca;

                    if(MaoDireitaAcimaCabeca)
                    {
                        MessageBox.Show("Mao direita acima da cabeca.");
                    }
                }
            }

        }
    }
}
