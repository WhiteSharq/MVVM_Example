using Contracts;
using System.Diagnostics;

public class ZoomEntityService : IZoomEntity
{
    public void Zoom(EntityDTO enitity)
    {
        Debug.WriteLine(
            $"Zoomed to element {enitity.Id}");
    }
}