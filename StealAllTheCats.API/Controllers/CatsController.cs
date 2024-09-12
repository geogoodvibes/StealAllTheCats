using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StealAllTheCats.Business.Interfaces;
using StealAllTheCats.Dto.Cats;
using StealAllTheCats.Utilities.Helpers;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StealAllTheCats.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        /// <summary>
        /// The <see cref="ICatRepository"/>.
        /// </summary>
        private readonly ICatRepository _catService;

        /// <summary>
        /// The <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper _mapper;

        public CatsController(ICatRepository catService, IMapper mapper)
        {
            _catService = catService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fetches cats from CaaS API and adds them to db.
        /// </summary>
        /// <param name="catCount">Sets how many cats we want to fetch</param>
        /// <returns>Status 200 if everything added to the db.</returns>
        [HttpPost("fetch", Name = nameof(FetchCats))]
        [SwaggerResponse(StatusCodes.Status201Created, "Cats added successfully!")]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status409Conflict)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FetchCats(
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than or equal to {1}")] int catCount = 25)
        {
            try
            {
                List<GetCatApiResponseDto> data = await FetchCatsFromApiAsync(catCount);

                var addCatList = _mapper.Map<List<AddCatRequestDto>>(data);

                await GetImageFilesFromApiAsync(addCatList).ConfigureAwait(false);

                var result = await _catService.AddCatAsync(addCatList)
                     .ConfigureAwait(true);

                if (result > 0)
                {
                    return Ok(addCatList);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets cat by Id.
        /// </summary>
        /// <param name="catId">The cat identifier parameter.</param>
        /// <returns>Task&lt;GetCatResponseDto&gt;&gt;.</returns>
        [HttpGet("{catId:int}", Name = nameof(GetCat))]
        [SwaggerResponse(StatusCodes.Status200OK, "Cat fetched successfully!", typeof(GetCatResponseDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCat(
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than or equal to {1}")] int catId)
        {
            try
            {
                var cat = await _catService.GetCatAsync(catId).ConfigureAwait(true);
                return Ok(cat);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get a paged list of saved cats.
        /// </summary>
        /// <param name="tag">The tag parameter.</param>
        /// <param name="page">The page parameter.</param>
        /// <param name="pageSize">The page size parameter. Default value is 10.</param>
        /// <returns></returns>
        [HttpGet("tag", Name = nameof(GetCatsByTag))]
        [SwaggerResponse(StatusCodes.Status200OK, "Cat fetched successfully!", typeof(List<GetCatApiResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCatsByTag(string? tag = null,
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than or equal to {1}")] int page = 1,
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than or equal to {1}")] int pageSize = 10)
        {
            try
            {
                var cats = await _catService.GetCatsAsync(tag, page, pageSize).ConfigureAwait(true);

                return Ok(cats);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get a paged list of saved cats filtered by tag.
        /// </summary>
        /// <param name="tag">The tag parameter.</param>
        /// <param name="page">The page parameter.</param>
        /// <param name="pageSize">The page size parameter. Default value is 10.</param>
        /// <returns></returns>
        [HttpGet("", Name = nameof(GetCats))]
        [SwaggerResponse(StatusCodes.Status200OK, "Cat fetched successfully!", typeof(List<GetCatApiResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCats(
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than or equal to {1}")] int page = 1,
        [Range(1, int.MaxValue, ErrorMessage = "{0} must be greater than or equal to {1}")] int pageSize = 10)
        {
            try
            {
                var cats = await _catService.GetCatsAsync(page, pageSize).ConfigureAwait(true);

                return Ok(cats);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Downloads the cat image.
        /// </summary>
        /// <param name="catId">The cat identifier parameter.</param>
        /// <returns></returns>
        [HttpGet("downloadFile/{catId:int}", Name = nameof(DownloadFileAsync))]
        [SwaggerResponse(StatusCodes.Status200OK, "Cat fetched successfully!", typeof(List<GetCatApiResponseDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status404NotFound)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DownloadFileAsync(int catId)
        {
            try
            {
                var cat = await _catService.GetCatAsync(catId).ConfigureAwait(true);

                if (System.IO.File.Exists(cat.ImagePath))
                {
                    return File(System.IO.File.OpenRead(cat.ImagePath), MimeTypes.Png, Path.GetFileName(cat.ImagePath));
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Gets and serializes cats from CaaS API.
        /// </summary>
        /// <param name="catCount">How many cats to fetch.</param>
        /// <returns></returns>
        static async Task<List<GetCatApiResponseDto>> FetchCatsFromApiAsync(int catCount)
        {
            var data = new List<GetCatApiResponseDto>();

            while (data.Count < catCount)
            {
                //This limit is set conditionally, in order in last iteration to get only remaining number of cats.
                var limit = catCount;
                if (limit > 10)
                {
                    limit = (data.Any() && data.Count > 10 && data.Count < catCount) ? 10 - (catCount - data.Count) : 10;
                }

                var url = $"https://api.thecatapi.com/v1/images/search?limit={limit}&api_key=live_MVgybJvA7wRmh81FWs3fLgD1BudHkEvMI79gPbSWOtJHanQfOdPhARvxjEnBwJJZ";
                using var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await httpClient.SendAsync(request);
                using var reader = new StreamReader(response.Content.ReadAsStream());
                var responseBody = reader.ReadToEnd();

                var options = new JsonSerializerOptions()
                {
                    NumberHandling = JsonNumberHandling.AllowReadingFromString |
                    JsonNumberHandling.WriteAsString
                };

                data.AddRange(JsonSerializer.Deserialize<List<GetCatApiResponseDto>>(responseBody, options));
                data = data.DistinctBy(x => x.Id).ToList();
            }

            return data;
        }

        /// <summary>
        /// Gets the images from CaaS API and store it to file location.
        /// </summary>
        /// <param name="catCount">How many cats to fetch.</param>
        /// <returns></returns>
        private static async Task GetImageFilesFromApiAsync(List<AddCatRequestDto> addCatList)
        {
            foreach (var item in addCatList)
            {
                using (var client = new HttpClient())
                {
                    using (var response = await client.GetAsync(item.Url))
                    {
                        byte[] imageBytes =
                            await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                        item.Image = imageBytes;
                    }
                }
            }
        }
    }

}
