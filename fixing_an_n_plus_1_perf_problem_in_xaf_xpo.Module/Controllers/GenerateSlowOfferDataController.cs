using fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.Controllers
{
    public class GenerateSlowOfferDataController : GenerateDataController<SlowOffer, SlowOfferItem> { }
    public class GenerateSlowOfferWithLinqDataController : GenerateDataController<SlowOfferWithLinq, SlowOfferItemWithLinq> { }
    public class GenerateSlowOfferWithPersistentAliasDataController : GenerateDataController<SlowOfferWithPersistentAlias, SlowOfferItemWithPersistentAlias> { }
    public class GenerateFasterOfferWithLinqDataController : GenerateDataController<FasterOfferWithLinq, FasterOfferItemWithLinq> { }
    public class GenerateFasterOfferWithCQRSDataController : GenerateDataController<FasterOfferWithCQRS, FasterOfferItemWithCQRS> { }
}
