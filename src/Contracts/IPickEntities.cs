namespace Contracts
{
    public interface IPickEntities
    {
        /// <summary>
        /// Lets picking up entitites by mouse
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public EntityDTO[] Select(object? parameters);
    }
}
