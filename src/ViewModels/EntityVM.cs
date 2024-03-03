namespace WS.ViewModels
{
    using Contracts;
    using System;

    public class EntityVM : IComparable<EntityVM>, IEquatable<EntityVM>
    {
        public EntityVM(EntityDTO entity)
        {
            DTO = entity;

            var s = $"{entity.Id} : {entity.Name}";

            if (entity.Location is PointDTO p)
            {
                s += $" ({Format(p)})";
            }

            AsString = s;
        }

        public string AsString { get; }
        public EntityDTO DTO { get; }

        public int CompareTo(EntityVM other)
        {
            return AsString.CompareTo(other.AsString);
        }

        public bool Equals(EntityVM other)
        {
            return DTO.Equals(other.DTO);
        }

        public override bool Equals(object? obj)
        {
            return obj is EntityVM other && Equals(other);
        }

        private string Format(PointDTO d)
        {
            var asString =
                $"{Round(d.X)};" +
                $"{Round(d.Y)};" +
                $"{Round(d.Z)}";

            return asString;
        }

        private string Round(double d) => d.ToString("0.000");
    }
}