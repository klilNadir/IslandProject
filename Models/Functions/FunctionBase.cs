using IslandProject.Models.Enums;
using IslandProject.Models.Individual;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Functions
{
    public abstract class FunctionBase :IFunction
    {
        private BestDetails BestDetails;
       
        public IIndividual Best { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool CheckBestChange (double value,IIndividual individual){
            if ((RunDetails.IsMax && value > BestDetails.BestValue) || (!RunDetails.IsMax && value < BestDetails.BestValue))
            {
                BestDetails.BestValue = value;
                BestDetails.BestValueChangeCount++;
                BestDetails.TimeFromLastChange = 0;
                BestDetails.Best = individual;
                return true;
            }
            return false;
        }

        public virtual  double Eval(IIndividual target)
        {
            throw new NotImplementedException();
        }

        public BestDetails GetBestDetails()
        {
            throw new NotImplementedException();
        }
    }
}
