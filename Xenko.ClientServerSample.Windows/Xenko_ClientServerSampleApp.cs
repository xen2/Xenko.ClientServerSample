using Stride.Engine;

namespace Stride.ClientServerSample.Windows
{
    class Xenko_ClientServerSampleApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
