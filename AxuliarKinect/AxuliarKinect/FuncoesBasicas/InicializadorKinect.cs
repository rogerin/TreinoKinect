
using System.Linq;


using Microsoft.Kinect;

namespace AuxiliarKinect.FuncoesBasicas
{

    public class InicializadorKinect
    {
        public static KinectSensor InicializarPrimeiroSensor (int anguloElevacaoInicial)
        {
            KinectSensor kinect = KinectSensor.KinectSensors.First(sensor => sensor.Status == KinectStatus.Connected );
            kinect.Start();
            kinect.ElevationAngle = anguloElevacaoInicial;
            return kinect;
        }
    }
}
 