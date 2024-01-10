using PlannerAssignment.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerAssignment.Mvvm.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        public MapViewModel(RequestManager requestManager) : base(requestManager)
        {


        }

        protected override Task FetchDataInternal()
        {
            throw new NotImplementedException();
        }
    }
}
