using connectToMongoDbRawMaterials.Interfaces;
using connectToMongoDbRawMaterials.Models;
using ConnectToMongoDbRawMaterials.Core;
using ConnectToMongoDbRawMaterials.Core.Interfaces;

namespace connectToMongoDbRawMaterials
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMongoClient<RawMaterial> mongoRepository = new MongoRepository<RawMaterial>();
            IAppConsole console = new appConsole();
            IMenu menu = new Menu();

            var appRunner = new appRunner(mongoRepository, console, menu);

            appRunner.startApp();
        }
    }
}
