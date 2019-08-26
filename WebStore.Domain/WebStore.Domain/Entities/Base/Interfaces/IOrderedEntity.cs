namespace WebStore.Domain.Entities.Base.Interfaces
{
    public interface IOrderedEntity
    {
        /// <summary>Order/Порядок</summary>
        int Order { get; set; }
    }
}