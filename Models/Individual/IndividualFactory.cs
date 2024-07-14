using IslandProject.Models.Enums;
using IslandProject.Models.Random;
using IslandProject.Models.Setup;

namespace IslandProject.Models.Individual
{
    public class IndividualFactory
    {
        private IRandom random;

        public IndividualFactory(IRandom random)
        {
            this.random = random;
        }

        public IIndividual CrateRandomIndividual(int grace)
        {
            FunctionType functionType = RunDetails.FunctionType;
            switch (functionType)
            {
                case FunctionType.BOOLEAN:
                    return new BooleanIndividual(grace, random);
                    break;
                case FunctionType.INTEGER:
                    return new IntIndividual(grace, random);
                    break;
                case FunctionType.FLOAT:
                    return new DoubleIndividual(grace, random);
                    break;
                case FunctionType.FRACTAL:
                    //Todo create fractal individual
                    return new IntIndividual(grace, random);
                    break;
                Default: return new BooleanIndividual(grace, random);
                    break;

            }
            return new BooleanIndividual(grace, random);
        }


    }
}
