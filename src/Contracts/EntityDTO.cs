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

        public string Id { get; }
        public string Name { get; }
        public PointDTO? Location { get; }

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
            return Equals(obj as EntityDTO);
        }

        public override int GetHashCode() => Id.GetHashCode();
    }
}