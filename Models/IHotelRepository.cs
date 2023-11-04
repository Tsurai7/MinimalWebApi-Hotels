public interface IHotelRepository : IDisposable
{
    Task<List<Hotel>> GetHotelsAsync();
    Task<Hotel> GetHotelAsync(int id);
    Task AddHotelAsync(Hotel hotel);
    Task UpdateHotelAsync(Hotel hotel);
    Task DeleteHotelAsync(int id);
    Task SaveAsync();
}