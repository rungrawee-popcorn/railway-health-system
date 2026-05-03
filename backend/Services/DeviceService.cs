using backend.Models;
using backend.Repositories;

namespace backend.Services
{
    public class DeviceService
    {
        private readonly DeviceRepository _repo;

        public DeviceService(DeviceRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Device>> GetAll()
        {
            return await _repo.GetAll();
        }

        public async Task Create(Device device)
        {
            await _repo.Create(device);
        }

        public async Task Update(Device device)
        {
            await _repo.Update(device);
        }

        public async Task Delete(int id)
        {
            await _repo.Delete(id);
        }
    }
}