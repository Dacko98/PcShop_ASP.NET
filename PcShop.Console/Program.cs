using PcShop.Console.Api;
using System.Net.Http;
using System.Threading.Tasks;

namespace PcShop.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var httpClient = new HttpClient();
            var ingredientClient = new EvaluationClient(httpClient);
            var ingredients = await ingredientClient.EvaluationGetAsync();
        }
    }
}
