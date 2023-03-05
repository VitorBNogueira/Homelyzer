using API;
using Application.Interfaces;
using AutoMapper;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Test.Fixtures
{
    public class InMemoryDBConfig : IDisposable
    {
        protected HomelyzerDBContext _context;

        private AdvertRepository? _advertRepository;
        private OwnerRepository? _ownerRepository;
        private PictureRepository? _pictureRepository;

        public InMemoryDBConfig()
        {
            var dbContextOptions = new DbContextOptionsBuilder<HomelyzerDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new HomelyzerDBContext(dbContextOptions);

            _context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        protected CancellationToken CltToken => CancellationToken.None;

        protected IMapper Mapper
        {
            get
            {
                var mapper = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(typeof(Mappings));
                });

                return mapper.CreateMapper();
            }
        }

        protected IAdvertRepository AdvertRepositoryFake
        {
            get
            {
                if (_advertRepository is null)
                    _advertRepository = new AdvertRepository(_context);

                return _advertRepository;
            }
        }

        protected IOwnerRepository OwnerRepositoryFake
        {
            get
            {
                if (_ownerRepository is null)
                    _ownerRepository = new OwnerRepository(_context);

                return _ownerRepository;
            }
        }

        protected IPictureRepository PictureRepositoryFake
        {
            get
            {
                if (_pictureRepository is null)
                    _pictureRepository = new PictureRepository(_context);

                return _pictureRepository;
            }
        }
    }
}
