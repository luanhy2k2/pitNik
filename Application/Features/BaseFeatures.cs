using Core.Interface.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features
{
    public class BaseFeatures
    {
        protected IPitNikRepositoryWrapper _pitNikRepo;
        protected BaseFeatures(IPitNikRepositoryWrapper pitNikRepo)
        {
            _pitNikRepo = pitNikRepo;
        }
    }
}
