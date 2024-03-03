namespace Contracts
{
    using System;

    public class EntityDTO : IEquatable<EntityDTO> 
    {
        public EntityDTO(string id, string name, PointDTO? location)
        {
            Id = id;
            Name = name;
            Location = location;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public PointDTO? Location { get; set; }

        public bool Equals(EntityDTO? other)
        {
            var res = other is not null &&
                Id.Equals(
                    other.Id,
                    StringComparison.InvariantCultureIgnoreCase);

            return res;
        }

        public override bool Equals(object? obj)
        {
            return
                obj is EntityDTO other
                && Equals(other);
        }

        public override int GetHashCode() => 0;
    }
}