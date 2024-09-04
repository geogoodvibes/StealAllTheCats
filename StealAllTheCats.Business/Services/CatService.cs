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
        public async Task<PaginatedResult<GetCatApiResponseDto>> GetCatsAsync(string tag, int page, int pageSize)
        {
            try
            {
                IQueryable<Cat> catsQueryable = _context.Cats.AsNoTracking().Include(x => x.Tags);

                var cats = await catsQueryable.AsNoTracking().Paginate(pageSize, page)
                    .ConfigureAwait(false);

                var finalResults = _mapper.Map<PaginatedResult<GetCatApiResponseDto>>(cats);

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
        public async Task<GetCatApiResponseDto> GetCatAsync(int catId)
        {
            try
            {
                IQueryable<Cat> catQueryable = _context.Cats
                    .Include(x => x.Tags)
                    .AsNoTracking();

                var catResult = await catQueryable.FirstOrDefaultAsync(d => d.Id == catId)
                    .ConfigureAwait(false);

                var cat = _mapper.Map<GetCatApiResponseDto>(catResult);

                return cat;
            }
            catch (Exception e)
            {
                throw new Exception(nameof(GetCatApiResponseDto), e.InnerException);
            }
        }

        /// <summary>
        /// Download
        /// </summary>
        /// <param name="catId"></param>
        /// <returns></returns>
        public async Task DownloadImageFileById(int catId)
        {
            try
            {
                var cat = _context.Cats.FirstOrDefault(x => x.Id == catId);

                if (cat.Image != null)
                {
                    var content = new System.IO.MemoryStream(cat.Image);
                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "FileDownloaded",
                        cat.ImageFilepath);

                    //await CopyStream(content, path);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<int> AddCatAsync(List<AddCatRequestDto> addCatListDto)
        {
            if (addCatListDto == null)
            {
                throw new ArgumentNullException(nameof(addCatListDto), "Supplied add Cat list was null");
            }

            //var existingCat = await _context.Cats
            //    .AsNoTracking()
            //    .FirstOrDefaultAsync(x => x.FirstName == addCatDto.FirstName &&
            //                              x.LastName == addCatDto.LastName &&
            //                              x.Mobile == addCatDto.Mobile)
            //    .ConfigureAwait(false);

            //if (existingCat != null)
            //{
            //    throw new Exception($"A Cat with first name {existingCat.FirstName} and last name {existingCat.LastName} already exists!");
            //}

            //var cat = _mapper.Map<Cat>(addCatDto);

            //cat.Tags = (addCatDto.TagsId > 1)
            //    ? _context.Tagss.FirstOrDefault(x => x.Id == addCatDto.TagsId)
            //    : null;

            //await _context.Cats.AddAsync(cat).ConfigureAwait(false);
            //await _context.SaveChangesAsync().ConfigureAwait(false);

            //return cat.Id;
            return 0;
        }
    }
}
