using WorkingWithWebApi2.Repositories.Managers;

namespace WorkingWithWebApi2.BusinessServices.Services.Abstract
{
    /// <summary>
    /// Base class for business service objects
    /// </summary>
    public class ServiceBase
    {

        #region Properties & Constructor

        protected RepositoryManager repositoryManager;

        public ServiceBase()
        {
            repositoryManager = new RepositoryManager();
        }

        #endregion  
    }
}
