using System;
using System.Collections.Generic;
using System.Linq;

namespace fixing_an_n_plus_1_perf_problem_in_xaf_xpo.Module.BusinessObjects
{
    public interface IOffer
    {
        string Name { get; set; }

        void AddRange(IEnumerable<IOfferItem> items);
    }
}