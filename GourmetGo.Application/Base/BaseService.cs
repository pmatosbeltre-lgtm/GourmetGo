
namespace GourmetGo.Application.Base
{
    public abstract class BaseService
    {
        protected DateTime GetCurrentDate() 
        {  
            return DateTime.Now; 
        }
    }
}
