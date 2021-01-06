using BlockGame.Glue;

namespace BlockGame
{
    class Program
    {
        static void Main(string[] args)
        {
            using ( Engine engine = new Engine() )
            {
                engine.Run();
            }
        }
    }
}
