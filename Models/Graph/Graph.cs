using IslandProject.Models.Random;

namespace IslandProject.Models.Graph
{
    public class Graph
    {
        public List<GraphEdge> Edges { get; set; }
        private int NumberOfNodes { get; set; }
        private IRandom random;

        //emptyGraph
        public Graph( int numberOfNodes, IRandom random)
        {
            NumberOfNodes = numberOfNodes;
            this.random = random;
        }
        public Graph(List<GraphEdge> edges, int numberOfNodes, IRandom random)
        {
            Edges = edges;
            NumberOfNodes = numberOfNodes;
            this.random = random;
        }

        public List<GraphEdge> FindToNighbors(int from)
        {
            return Edges.Where(e => e.From == from).OrderByDescending(e => e.Weight).ToList();
        }


        public int FindToNighborsMaxWeight(int from)
        {
            return Edges.Where(e=>e.From ==from).Max(e => e.Weight);
        }

        public void CrateSunGraphEdges (int weight, bool isTwoWay, int mainNode =-1)
        {
            Edges = new List<GraphEdge>();
            if (mainNode == -1) 
            {
                mainNode = random.NextInt(0, NumberOfNodes - 1);
            }
            for (int i = 0; i < NumberOfNodes; i++)
            {
                if (i != mainNode)
                {
                    Edges.Add(new GraphEdge()
                    {
                        From = i,
                        To = mainNode,
                        Weight = weight,
                    });
                    if (isTwoWay) {
                        Edges.Add(new GraphEdge()
                        {
                            From = mainNode,
                            To = i,
                            Weight = weight,
                        });
                    }
                }
            }
        }

        public void CreateRandomCyclicGraph(int wieght)
        {
            int originalFrom = random.NextInt (0, NumberOfNodes - 1);
            int to,toIndex;
            Edges = new List<GraphEdge>();
            List<int > leftNodes = new List<int>();
            for (int i = 0;i < NumberOfNodes;i++)
            {
                leftNodes.Add(i);
            }
            int from  =originalFrom;
            for (int i = 0; i < NumberOfNodes - 1; i++) 
            {
               toIndex =random .NextInt (0, NumberOfNodes - 1 -i) ;
                to = leftNodes[toIndex];
                // dont allow self edges
                while (to == from) 
                {
                    toIndex = random.NextInt(0, NumberOfNodes - 1 - i);
                    to = leftNodes[toIndex];
                }
                Edges.Add(new GraphEdge() { From = from, To = to,Weight =wieght });
                leftNodes.RemoveAt(toIndex );
                from = to;
            }
            Edges.Add(new GraphEdge() {From =from, To =originalFrom,Weight =wieght});
        }
    }
}
