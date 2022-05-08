using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shortener.Service.DTO;
using Shortener.Service.Model;
using Shortener.Service.Services.Interface;
using System;
using System.Reflection;
using System.Text;
using IUrlHelper = Shortener.Service.Services.Interface.IUrlHelper;
using Newtonsoft.Json.Linq;

namespace Shortener.Service.Controllers.Api
{
    [ApiController]
    public class ShortenerController : ControllerBase
    {
        private readonly IDbContext _dbContext;
        private readonly IUrlHelper _urlHelper;
        private readonly IMapper _mapper;
        // FileStream Log
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        // Console log - Just to be more user friendly // Debug
        private readonly ILogger<ShortenerController> _logger;

        public ShortenerController(IDbContext urls, IUrlHelper urlHelper, IMapper mapper, ILogger<ShortenerController> logger)
        {
            _dbContext = urls;
            _urlHelper = urlHelper;
            _mapper = mapper;
            _logger = logger;
        }
        
        [HttpGet("{shortUrl}")]
        public IActionResult GetUrl(string shortUrl)
        {
            _logger.LogInformation("Get request => Start");
            if (String.IsNullOrEmpty(shortUrl))
                return BadRequest();
            try
            {
                // Check if it is the customized shortened URL
                var urlData = _dbContext.GetCustomizedUrl(shortUrl);
                // If not, it is the random URL, find the record
                if (urlData == null)
                {
                    var id = _urlHelper.GetId(shortUrl.Substring(shortUrl.Length-6));
                    _logger.LogInformation(id.ToString());
                    urlData = _dbContext.GetUrl(id);   
                    // If it is not found, return 404 not found.
                    if (urlData == null)
                    {
                        return NotFound();
                    }                  
                }
                _logger.LogInformation(urlData.Url.ToString());
                var urlDataDto = _mapper.Map<UrlDataDto>(urlData);                

                // If it is a browser request, redirect to the main URL.
                string userAgent = this.Request.Headers["User-Agent"].ToString();
                if (userAgent.Contains("Mozilla") || userAgent.Contains("AppleWebKit") || userAgent.Contains("Chrome") ||  userAgent.Contains("Safari") || userAgent.Contains("Edge"))
                    return RedirectPermanent(urlDataDto.Url);

                _logger.LogInformation("Get request => Finished");
                return Ok(urlDataDto);
            }
            catch (Exception ex)
            {
                log.Error("ShortenerApiError: ", ex);
                _logger.LogInformation(ex.ToString());
                _logger.LogInformation("Get request => Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost("shorten")]
        public IActionResult ShortenUrl([FromBody] UrlDataDto longUrl)
        {
            _logger.LogInformation("Post request => Start");
            if (longUrl == null)
                return BadRequest();

            if (!Uri.TryCreate(longUrl.Url, UriKind.Absolute, out Uri result))
                ModelState.AddModelError("Info", "Empty URL");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                // If URL is already shortened, return a 400 bad request and inform user about existing URL
                if(_dbContext.CheckIfUrlExists(longUrl.Url))
                {
                    var shortUrlId = _dbContext.FindExistingUrl(longUrl.Url);
                    var inputDataRecord = _dbContext.GetUrl(shortUrlId);
                    var shortUrl = $"{this.Request.Scheme}://{this.Request.Host}/{inputDataRecord.CustomUrl}";
                    if (inputDataRecord.CustomUrl == longUrl.CustomUrl)
                    {                                             
                        ModelState.AddModelError("Info", "Url is already shortened");
                        ModelState.AddModelError("shortUrl", shortUrl);
                    }
                    else
                    {   
                        if(longUrl.CustomUrl == "") // random URL
                        {
                            inputDataRecord.CustomUrl = _urlHelper.GetShortUrl(shortUrlId);
                        }
                        else // custom URL
                        {
                            inputDataRecord.CustomUrl = longUrl.CustomUrl;
                        }                      
                        _dbContext.UpdateUrlRecord(inputDataRecord);
                        shortUrl = $"{this.Request.Scheme}://{this.Request.Host}/{inputDataRecord.CustomUrl}";
                        ModelState.AddModelError("Info", "Url is already shortened before but here is the URL");
                        ModelState.AddModelError("shortUrl", shortUrl);
                    }
                }
                    
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Create and save new record to the database
                var newEntry = new UrlData
                {
                    Url = longUrl.Url,
                    CustomUrl = longUrl.CustomUrl                 
                };
                
                var id = _dbContext.AddUrl(newEntry);       
                UrlDataDto urlDataDto = new UrlDataDto() { Url = $"{this.Request.Scheme}://{this.Request.Host}/{_urlHelper.GetShortUrl(id)}" };
                newEntry.CustomUrl = _urlHelper.GetShortUrl(id);
                _dbContext.UpdateUrlRecord(newEntry);
                
                if(longUrl.CustomUrl != "")
                {
                    urlDataDto.Url = $"{this.Request.Scheme}://{this.Request.Host}/{longUrl.CustomUrl}";
                    newEntry.CustomUrl = longUrl.CustomUrl;
                    _dbContext.UpdateUrlRecord(newEntry);
                }

                _logger.LogInformation("Post request => Finished");
                return Created("shortUrl", urlDataDto);
            }
            catch (Exception ex)
            {
                log.Error("ShortenerApiError: ", ex);
                _logger.LogInformation("Get request => Error");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }  
    }

}