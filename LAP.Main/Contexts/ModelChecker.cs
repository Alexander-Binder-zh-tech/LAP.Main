using Lap.Model;

namespace LAP.Main.Contexts;

public class ModelChecker
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ModelChecker> _logger;

    public ModelChecker(IServiceProvider serviceProvider, ILogger<ModelChecker> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<MasterDataCheckResult> CheckMasterData(ModelBase data, int? id)
    {
        return data switch
        {
            Lap.Model.Models.Customer.Customer customer => await CheckCustomer(customer, id),
            _ => throw new ArgumentException("unknown master-data type")
        };
    }
    
    private async Task<MasterDataCheckResult> CheckCustomer(Lap.Model.Models.Customer.Customer customer, int? id)
    {
        _logger.LogDebug("Checking Customer");

        //todo add checks if necessary

        return MasterDataCheckResult.Ok();
    }
    
    public class MasterDataCheckResult
    {
        public ModelCheckError Error { get; set; }
        public string AdditionalInfo { get; set; } = null!;

        public static MasterDataCheckResult Ok()
        {
            return new MasterDataCheckResult();
        }

        public static MasterDataCheckResult NotOk(ModelCheckError error, string info)
        {
            return new MasterDataCheckResult
            {
                Error = error,
                AdditionalInfo = info
            };
        }
    }

    public enum ModelCheckError
    {
        None,
        NotFound,
        AlreadyExists
    }
}