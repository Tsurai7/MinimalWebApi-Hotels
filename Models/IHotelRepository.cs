public interface IHotelRepository : IDisposable
{
    Task<List<Hotel>> GetHotelsAsync();
    Task<List<Hotel>> GetHotelsAsync(string name);
    Task<List<Hotel>> GetHotelsAsync(Coordinate coordinate);
    Task<Hotel> GetHotelAsync(int id);
    Task AddHotelAsync(Hotel hotel);
    Task UpdateHotelAsync(Hotel hotel);
    Task DeleteHotelAsync(int id);
    Task SaveAsync();
}