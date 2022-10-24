using TvShowTracker.Interfaces;
using TvShowTracker.Model;
using TvShowTracker.Data;
using AutoMapper;
namespace TvShowTracker.Services
{
    public class UserServices : IUserServices
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public UserServices(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserGetDTO> CreateUser(UserPostDTO user)
        {
            try
            {
                var newUser = _mapper.Map<User>(user);
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();
                return _mapper.Map<UserGetDTO>(newUser);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        async Task<UserGetDTO> IUserServices.GetUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                return _mapper.Map<UserGetDTO>(user);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}