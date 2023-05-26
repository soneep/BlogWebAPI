using Blog.Entities;

namespace Repository
{
    public interface IBlogStore
    {
        public List<BlogInfo> GetBlogInfo();
        public BlogInfo GetBlogInfo(int id);
        public void AddBlogInfo(BlogInfo blogInfo);
        public void UpdateBlogInfo(BlogInfo blogInfo);
        public BlogInfo DeleteBlogInfo(int id);
    }
}