using System;
using ProductService;
using Grpc.Net.Client;
using System.Diagnostics;
using static ProductService.gRPC;

namespace GRPCClient
{
    class Program
    {
        static readonly GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { });
        static readonly gRPCClient client = new gRPCClient(channel);
        static readonly int numberRequests = 1000000;

        public static void Main()
        {
            try
            {
                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                for (var i = 1; i <= numberRequests; i++)
                {
                    InsertNewProduct();
                    Console.WriteLine("gRPC - " + i);
                }
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;

                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);

                Console.WriteLine("Tempo total: " + elapsedTime);
                Console.ReadKey();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private static void InsertNewProduct()
        {
            try
            {
                Product product = new Product
                {
                    Name = "McLaren Senna",
                    Description = "O McLaren Senna foi produzido pela marca inglesa como uma justa homenagem ao tricampeão da Fórmula 1, o brasileiro Ayrton Senna. O superesportivo tem motor 4.0 V8 biturbo de 800 cv, capaz de levá-lo de 0 a 100 km/h em apenas 2,8 segundos e chegar a uma velocidade máxima de 340 km/h. Foram apenas 500 unidades produzidas e todas já foram vendidas, colocando o modelo como o mais caro do Brasil.",
                    Price = "R$ 7,2 milhões",
                    CategoryId = "1"
                };

                client.PostAsync(product);
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }
    }
}
