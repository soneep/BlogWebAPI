using Blog.Entities;

namespace Blog.Business
{
    public interface IBlogManager
    {
        public List<BlogInfo> GetBlogInfo();
        public BlogInfo GetBlogInfo(int id);
        public BlogInfoResult AddBlogInfo(BlogInfo blogInfo);
        public BlogInfoResult UpdateBlogInfo(int id ,BlogInfo blogInfo);
        public BlogInfo DeleteBlogInfo(int id);
    }
}