using IslandProject.Models.Enums;
using IslandProject.Models.Individual;

namespace IslandProject.Models.Functions
{
    public interface IFunction
    {
        public IIndividual Best { get; set; }
       

        public  double Eval(IIndividual target);
        public BestDetails GetBestDetails();
       
    }
}
