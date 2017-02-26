using Nancy;

namespace Kontur.GameStats.Server.NancyModules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = _ => "Hello World";
        }
    }
}