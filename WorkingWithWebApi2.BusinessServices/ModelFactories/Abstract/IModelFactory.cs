using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithWebApi2.BusinessServices.ModelFactories.Abstract
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">The 'T'ype of domain entity to be converted</typeparam>
    /// <typeparam name="E">The 'E'dit view model type</typeparam>
    /// <typeparam name="L">The 'L'ist view model type</typeparam>
    /// <typeparam name="D">The 'D'rop list view model type</typeparam>
    public interface IModelFactory <T, E, L, D>
    {
        /// <summary>
        /// Converts a view model object into a domain entity object prior to persisting to data storage
        /// </summary>
        /// <param name="viewModel">The view model object to be converted</param>
        /// <returns>The converted domain entity object</returns>
        T CreateDomainEntity(E viewModel);

        /// <summary>
        /// Converts a domain entity object to a view model used in drop lists
        /// </summary>
        /// <param name="entity">The domain entity object to be converted</param>
        /// <returns>The converted view model</returns>
        D CreateDropListVM(T entity);

        /// <summary>
        /// Converts a domain entity object to a view model used for insert and update forms
        /// </summary>
        /// <param name="entity">The domain entity object to be converted</param>
        /// <returns>The converted view model</returns>
        E CreateEditVM(T entity);

        /// <summary>
        /// Converts a domain entity object to a view model used in main lists
        /// </summary>
        /// <param name="entity">The domain entity object to be converted</param>
        /// <returns>The converted view model</returns>
        L CreateListVM(T entity);
    }
}
