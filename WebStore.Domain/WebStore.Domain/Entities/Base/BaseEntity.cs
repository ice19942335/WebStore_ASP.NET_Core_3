using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebStore.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebStore.Domain.Entities.Base
{
    /// <summary>Base entity</summary>
    public abstract class BaseEntity : IBaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}
