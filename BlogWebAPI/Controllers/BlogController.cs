using Blog.Business;
using Blog.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogManager _blogManager;
        private readonly ILogger _logger;
        public BlogController(IBlogManager blogManager, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<BlogController>(); ;
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
            return await Task.FromResult(_blogManager.GetBlogInfo());
        }

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
                _logger.LogError("Error occuured in BlogController => GetBlogInfo API. Exception details : " +ex.Message);
                return new BlogInfo();
            }
            
        }

        /// <summary>
        /// Creates new blog information
        /// </summary>
        /// <param name="blogInfo"></param>
        /// <returns></returns>
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
                _logger.LogError("Error occuured in BlogController => Create API. Exception details : "+ex.Message);
            }
            return response;
        }

        // PUT api/employee/5
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
                _logger.LogError("Error occuured in BlogController => UpdateBlogInfo API. Exception details : "+ex.Message);
            }
             await Task.FromResult(blogInfo);

            return response;
        }

        // DELETE api/employee/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BlogInfo>> Delete(int id)
        {
            var blogInfo = _blogManager.DeleteBlogInfo(id);
            return await Task.FromResult(blogInfo);
        }
    }
}
