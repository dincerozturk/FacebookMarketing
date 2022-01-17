using System;

namespace FM.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? Uid { get; }
    }
}