using App.Metrics;
using App.Metrics.Counter;

namespace ProductMicroservice
{
    public class MetricsRegistry
    {
        public static CounterOptions CreateProductsCounter => new CounterOptions
        {
            Name = "Created Products",
            Context = "REST",
            MeasurementUnit = Unit.Calls
        };
        public static CounterOptions InsertProductDbConnectionCounter => new CounterOptions
        {
            Name = "Created Database Connections for insert product",
            Context = "Database",
            MeasurementUnit = Unit.Calls
        };
    }
}