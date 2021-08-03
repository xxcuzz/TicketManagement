using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementApi.Services.Interfaces
{
    public interface IService<T>
    {
        /// <summary>
        /// Validates and creates <paramref name="item"/>.
        /// </summary>
        /// <returns>True if <paramref name="item"/> is valid and added to database.</returns>
        Task<bool> CreateAsync(T item);

        /// <summary>
        /// Updates <paramref name="item"/>.
        /// </summary>
        /// <returns>True if <paramref name="item"/> updated in database.</returns>
        Task<bool> UpdateAsync(T item);

        /// <summary>
        /// Removes <paramref name="item"/>.
        /// </summary>
        /// <returns>True if <paramref name="item"/> removed from database.</returns>
        Task<bool> DeleteAsync(T item);

        /// <summary>
        /// Removes item.
        /// </summary>
        /// <returns>True if item removed from database.</returns>
        Task<bool> DeleteAsync(int id);

        /// <summary>
        /// Gets all elements from database.
        /// </summary>
        /// <returns>IEnumerable list of elements.</returns>
        IEnumerable<T> GetAll();

        Task<T> GetByIdAsync(int id);
    }
}
