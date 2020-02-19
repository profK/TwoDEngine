using System;
using ServiceRegistry;

namespace TDEGui
{
    public interface IGUIService:IService
    {
        event Action<IGUIService> OnGUInit;
    }
}