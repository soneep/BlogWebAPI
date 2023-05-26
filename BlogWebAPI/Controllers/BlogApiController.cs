using Azure.Core;
using Blog.Business;
using Blog.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BlogWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogApiController : ControllerBase
    {
        private readonly IBlogManager _blogManager;
        private readonly ILogger _logger;
        public BlogApiController(IBlogManager blogManager, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlogApiController>();
            _blogManager = blogManager;
        }

        // GET: api/blogInfo>
        /// <summary>
        /// Gets all Blog information
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getallblogs")]
        public async Task<ActionResult<IEnumerable<BlogInfo>>> Get()
        {
            List<BlogInfo> blogInfos = new List<BlogInfo>();
            try
            {
                var result = _blogManager.GetBlogInfo();
                if(result != null)
                    return await Task.FromResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occuured in BlogController => getallblogs API. Exception details : ", ex.Message);
            }
            return blogInfos;
        }

        /// <summary>
        /// gets blog information by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getallblogsbyid")]
        public async Task<ActionResult<BlogInfo>> GetBlogInfo(int id)
        {
            try
            {
                BlogInfo blogInfo = _blogManager.GetBlogInfo(id);
                return blogInfo;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogController => GetBlogInfo API. ID: {0} Exception details : {1} ", id.ToString() ,ex.Message);
                return new BlogInfo();
            }
            
        }

        /// <summary>
        /// Creates new blog information
        /// </summary>
        /// <param name="blogInfo"></param>
        /// <returns></returns>
        [Authorize(Role.Admin)]
        [HttpPost]
        [Route("createblog")]
        public async Task<ActionResult<BlogInfoResponse>> Create(BlogInfo blogInfo)
        {
            var response = new BlogInfoResponse();
            try
            {
               var result = _blogManager.AddBlogInfo(blogInfo);
                
                if(result != null && result.ErrorCode != 0) { 
                
                    response.ErrorCode = result.ErrorCode;
                    response.ErrorMessage = result.ErrorMessage;
                }
                await Task.FromResult(blogInfo);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogController => Create API.Request Details : {0}, Exception details : {1} ", blogInfo == null ? string.Empty : blogInfo.ToString(), ex.Message);                
            }
            return response;
        }

        /// <summary>
        /// Updates blog information
        /// </summary>
        /// <param name="id"></param>
        /// <param name="blogInfo"></param>
        /// <returns></returns>
        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<BlogInfoResponse>> UpdateBlogInfo(int id, BlogInfo blogInfo)
        {
            var response = new BlogInfoResponse();
            try
            {
               var result = _blogManager.UpdateBlogInfo(id,blogInfo);
                if (result != null && result.ErrorCode != 0)
                {

                    response.ErrorCode = result.ErrorCode;
                    response.ErrorMessage = result.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occuured in BlogController => UpdateBlogInfo API.Request Details : {0}, Exception details : {1} ", blogInfo == null ? string.Empty : blogInfo.ToString(), ex.Message);
            }
             await Task.FromResult(blogInfo);

            return response;
        }

        /// <summary>
        /// Deletes blog information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogInfo>> Delete(int id)
        {
            var blogInfo = _blogManager.DeleteBlogInfo(id);
            return await Task.FromResult(blogInfo);
        }
    }
}
