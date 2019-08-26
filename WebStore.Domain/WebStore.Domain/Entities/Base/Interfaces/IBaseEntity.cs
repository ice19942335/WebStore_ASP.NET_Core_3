using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.Entities.Base.Interfaces
{
    /// <summary>Entity</summary>
    public interface IBaseEntity
    {
        /// <summary>Identifier</summary>
        int Id { get; set; }
    }
}
