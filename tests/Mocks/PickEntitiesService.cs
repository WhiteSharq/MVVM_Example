using Contracts;
using System.Linq;

public class PickEntitiesService : IPickEntities
{
    private int _id = 1000;
    private double _d = 0;

    public EntityDTO[] Select(object? parameters)
    {
        var res = new EntityDTO[]
            {
                    new EntityDTO($"{_id++}", "First", new PointDTO(_d, _d, _d++)),
                    new EntityDTO($"{_id++}", "Second", new PointDTO(_d, _d, _d++)),
                    new EntityDTO($"{_id++}", "Third", new PointDTO(_d, _d, _d++)),
            }
            .ToArray();

        return res;
    }
}
