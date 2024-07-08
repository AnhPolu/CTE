using CTEMS.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTEMS.Infrastructure
{
    public interface ICurrentUserService
    {
        int CurrentUserId { get; set; }
    }
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService()
        {
        }

        public int CurrentUserId { get ; set ; }
    }
}
