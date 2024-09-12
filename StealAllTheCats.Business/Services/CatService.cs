using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StealAllTheCats.Business.Interfaces;
using StealAllTheCats.Data.Context;
using StealAllTheCats.Data.Entities;
using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Utilities;

namespace StealAllTheCats.Business.Services
{
    /// <summary>
    /// Cat Service Class.
    /// Implements the <see cref="ICatRepository"/> <see langword="interface"/>
    /// </summary>
    public class CatService : ICatRepository
    {
        /// <summary>
        /// Db Context
        /// </summary>
        protected StealAllTheCatsContext _context { get; }

        /// <summary>
        /// The <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CatService" /> class.
        /// </summary>
        public CatService(StealAllTheCatsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets a paged list of all cats matching the specified tag (if any).
        /// </summary>
        /// <param name="tag">The tag parameter.</param>
        /// <param name="page">The page parameter.</param>
        /// <param name="pageSize">The page size parameter. Default value is 10.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PaginatedResult<GetCatResponseDto>> GetCatsAsync(int page, int pageSize)
        {
            try
            {

                var catsQueryable = _context.Cats.AsNoTracking().Include(x => x.Tags);

                var cats = await catsQueryable.AsNoTracking().Paginate(pageSize, page)
                    .ConfigureAwait(false);

                var finalResults = _mapper.Map<PaginatedResult<GetCatResponseDto>>(cats);

                return finalResults;
            }
            catch (Exception e)
            {
                throw new Exception(nameof(GetCatApiResponseDto), e.InnerException);
            }
        }

        /// <summary>
        /// Gets a paged list of all cats matching the specified tag (if any).
        /// </summary>
        /// <param name="tag">The tag parameter.</param>
        /// <param name="page">The page parameter.</param>
        /// <param name="pageSize">The page size parameter. Default value is 10.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<PaginatedResult<GetCatResponseDto>> GetCatsAsync(string tag, int page, int pageSize)
        {
            try
            {
                var catsQueryable = _context
                    .Cats.AsNoTracking()
                    .Include(x => x.Tags)
                    .Where(cats => cats.Tags.Any(t => t.Name == tag));

                var cats = await catsQueryable.AsNoTracking().Paginate(pageSize, page)
                    .ConfigureAwait(false);

                var finalResults = _mapper.Map<PaginatedResult<GetCatResponseDto>>(cats);

                return finalResults;
            }
            catch (Exception e)
            {
                throw new Exception(nameof(GetCatApiResponseDto), e.InnerException);
            }
        }

        /// <summary>
        /// Gets the cat with the specified identifier.
        /// </summary>
        /// <param name="catId">The cat identifier.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<GetCatResponseDto> GetCatAsync(int catId)
        {
            try
            {
                IQueryable<Cat> catQueryable = _context.Cats
                    .Include(x => x.Tags)
                    .AsNoTracking();

                var catResult = await catQueryable.FirstOrDefaultAsync(d => d.Id == catId)
                    .ConfigureAwait(false);

                var cat = _mapper.Map<GetCatResponseDto>(catResult);

                return cat;
            }
            catch (Exception e)
            {
                throw new Exception(nameof(GetCatResponseDto), e.InnerException);
            }
        }

        /// <summary>
        /// Adds cats from a cat list.
        /// </summary>
        /// <param name="addCatListDto"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<int> AddCatAsync(List<AddCatRequestDto> addCatListDto)
        {
            if (addCatListDto == null)
            {
                throw new ArgumentNullException(nameof(addCatListDto), "Supplied add Cat list was null");
            }

            foreach (var addCat in addCatListDto)
            {
                var existingCat = await _context.Cats
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CatApiId == addCat.CatApiId)
                    .ConfigureAwait(false);

                if (existingCat != null)
                {
                    continue;
                }

                //Cat
                var cat = _mapper.Map<Cat>(addCat);
                cat.Created = DateTime.Now;

                //Breeds
                AddBreeds(addCat, cat);

                //Image File
                AddImageFile(addCat, cat);

                await _context.Cats.AddAsync(cat).ConfigureAwait(false);
            }
            return await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Add Breeds.
        /// </summary>
        /// <param name="addCat"></param>
        /// <param name="cat"></param>
        private void AddBreeds(AddCatRequestDto addCat, Cat cat)
        {
            if (addCat.Breeds?.Length > 0)
            {
                foreach (var breed in addCat.Breeds)
                {
                    var tags = breed.Temperament?.Replace(" ", "").Split(',');
                    foreach (var tag in tags)
                    {
                        cat.Tags.Add(new Tag { Name = tag, Created = DateTime.Now });
                    }
                }
            }
        }

        /// <summary>
        /// Add Image File.
        /// </summary>
        /// <param name="addCat"></param>
        /// <param name="cat"></param>
        private void AddImageFile(AddCatRequestDto addCat, Cat cat)
        {
            if (addCat.Image.Length > 0)
            {
                using (var ms = new MemoryStream(addCat.Image))
                {
                    string ext = System.IO.Path.GetExtension(addCat.Url);

                    var folder = Path.Combine(Directory.GetCurrentDirectory(), "Cat Photos");

                    if (!Directory.Exists(folder))
                        System.IO.Directory.CreateDirectory(folder);

                    var path = Path.Combine(folder, $"{addCat.CatApiId}{ext}");

                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        ms.WriteTo(fs);
                        cat.ImagePath = path;
                    }
                }
            }
        }
    }
}
