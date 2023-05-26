using Blog.Entities;
using NLog;
using Repository;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Business.Core
{
    public class BlogManager : IBlogManager
    {   
        private readonly IBlogStore _blogStore;
        public static ILogger logger = LogManager.GetLogger("BlogManager");
        public BlogManager(IBlogStore blogStore) {
            _blogStore = blogStore;
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
            BlogInfoResult blogInfoResult =new BlogInfoResult();
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
            return blogInfoResult;
        }
        public BlogInfoResult UpdateBlogInfo(int id ,BlogInfo blogInfo)
        {
            BlogInfoResult blogInfoResult = new BlogInfoResult();
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
                blogInfo.ID = id;
                _blogStore.UpdateBlogInfo(blogInfo);
            }
            return blogInfoResult;
        }
        public BlogInfo DeleteBlogInfo(int id)
        {
            return _blogStore.DeleteBlogInfo(id);
        }
    }
}