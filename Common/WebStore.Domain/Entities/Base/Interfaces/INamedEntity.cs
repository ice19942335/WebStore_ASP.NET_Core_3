namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>Named entity</summary>
    public interface INamedEntity : IBaseEntity
    {
        /// <summary>Name</summary>
        string Name { get; set; }
    }
}