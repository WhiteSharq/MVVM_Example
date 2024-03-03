using Contracts;
using System.Linq;

public class GetEntitiesService : IGetEntities
{
    //private int _id = 0;
    private double _d = 0;

    public EntityDTO[] GetEntities(string namePart)
    {
        var res = new EntityDTO[]
            {
                    new EntityDTO($"1", "First", new PointDTO(_d, _d, _d++)),
                    new EntityDTO($"2", "Second", new PointDTO(_d, _d, _d++)),
                    new EntityDTO($"3", "Third", new PointDTO(_d, _d, _d++)),
            }
            .Where(e => e.Name
                         .ToUpper()
                         .Contains(namePart.ToUpper()))
            .ToArray();

        return res;
    }
}
