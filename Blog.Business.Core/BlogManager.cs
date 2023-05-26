using Blog.Entities;
using Microsoft.Extensions.Logging;
using Repository;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Business.Core
{
    public class BlogManager : IBlogManager
    {   
        private readonly IBlogStore _blogStore;
        private readonly ILogger _logger;
        public BlogManager(IBlogStore blogStore, ILoggerFactory loggerFactory) {
            _blogStore = blogStore;
            _logger = loggerFactory.CreateLogger<BlogManager>(); ;
        }
        public List<BlogInfo> GetBlogInfo()
        {
            return _blogStore.GetBlogInfo();
        }
        public BlogInfo GetBlogInfo(int id)
        {
            return _blogStore.GetBlogInfo(id);
        }
        public BlogInfoResult AddBlogInfo(BlogInfo blogInfo)
        {
            BlogInfoResult blogInfoResult = new BlogInfoResult();
            try
            {   
                if (blogInfo.Category.ToLower() != "technology" && blogInfo.Category.ToLower() != "travel" && blogInfo.Category != "food")
                {
                    blogInfoResult.ErrorCode=1;
                    blogInfoResult.ErrorMessage = "Blog posts can only be created in specific categories: \"Technology,\" \"Travel,\" and \"Food.\"";
                }
                else
                {
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(blogInfo.Title));
                        Guid result = new Guid(hash);
                        blogInfo.BlogID = result;
                    }
                    _blogStore.AddBlogInfo(blogInfo);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogManager => AddBlogInfo method. Exception details : "+ex.Message);
            }
            return blogInfoResult;
        }
        public BlogInfoResult UpdateBlogInfo(int id ,BlogInfo blogInfo)
        {
            BlogInfoResult blogInfoResult = new BlogInfoResult();
            try
            {
                if(id <= 0)
                {
                    blogInfoResult.ErrorCode=1;
                    blogInfoResult.ErrorMessage = "Incorrect ID value.";
                }
                else if (blogInfo.Category.ToLower() != "technology" && blogInfo.Category.ToLower() != "travel" && blogInfo.Category != "food")
                {
                    blogInfoResult.ErrorCode=1;
                    blogInfoResult.ErrorMessage = "Blog posts can only be created in specific categories: \"Technology,\" \"Travel,\" and \"Food.\"";
                }
                else
                {
                    using (MD5 md5 = MD5.Create())
                    {
                        byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(blogInfo.Title));
                        Guid result = new Guid(hash);
                        blogInfo.BlogID = result;
                    }
                    blogInfo.ID = id;
                    _blogStore.UpdateBlogInfo(blogInfo);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occuured in BlogManager => UpdateBlogInfo method. Exception details : "+ ex.Message);
            }
            return blogInfoResult;
        }
        public BlogInfo DeleteBlogInfo(int id)
        {
            BlogInfo blogInfo = new BlogInfo();
            try
            {
                blogInfo= _blogStore.DeleteBlogInfo(id);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogManager => DeleteBlogInfo method. Exception details : "+ ex.Message);
            }
            return blogInfo;
        }
    }
}