using System.Linq;
using System.Threading.Tasks;

namespace TicketManagement.DataAccess.InterfacesRepository
{
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Adds an <paramref name="item"/> to the database.
        /// </summary>
        /// <returns> True if <paramref name="item"/> added successfully.</returns>
        Task<bool> AddAsync(T item);

        /// <summary>
        /// Removes an <paramref name="item"/> from the database.
        /// </summary>
        /// <returns> True if <paramref name="item"/> removed successfully.</returns>
        Task<bool> DeleteAsync(T item);

        /// <summary>
        /// Updates an <paramref name="item"/> in the database.
        /// </summary>
        /// <returns> True if <paramref name="item"/> updated successfully.</returns>
        Task<bool> UpdateAsync(T item);

        /// <summary>
        /// Gets all elements from database.
        /// </summary>
        /// <returns>IEnumerable list of elements.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Gets all elements from database by id.
        /// </summary>
        Task<T> GetByIdAsync(int id);
    }
}
