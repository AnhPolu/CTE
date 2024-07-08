using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CTEMS.Lib.Model
{
    public interface IBaseEntity
    {
        long Id { get; set; }
    }
    public interface IAuditEntity
    {
        DateTime CreatedDate { get; set; }
        long CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        long? ModifiedBy { get; set; }
    }
    public interface IDeleteEntity
    { 
        bool IsDeleted { get; set; }
    }

    public class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
    }

    public class DeleteEntity : BaseEntity, IDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
    public class AuditEntity : BaseEntity, IAuditEntity
    {
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
