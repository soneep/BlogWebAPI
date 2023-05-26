using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repository.Core
{
    public class BlogStore : IBlogStore
    {
        readonly ApplicationDbContext _dbContext = new();

        public BlogStore(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BlogInfo> GetBlogInfo()
        {
            try
            {
                return _dbContext.BlogInfos.ToList();
            }
            catch
            {
                throw;
            }
        }

        public BlogInfo GetBlogInfo(int id)
        {
            try
            {
                BlogInfo? BlogInfo = _dbContext.BlogInfos.Find(id);
                if (BlogInfo != null)
                {
                    return BlogInfo;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public void AddBlogInfo(BlogInfo BlogInfo)
        {
            try
            {
                _dbContext.BlogInfos.Add(BlogInfo);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateBlogInfo(BlogInfo BlogInfo)
        {
            try
            {
                _dbContext.Entry(BlogInfo).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public BlogInfo DeleteBlogInfo(int id)
        {
            try
            {
                BlogInfo? BlogInfo = _dbContext.BlogInfos.Find(id);

                if (BlogInfo != null)
                {
                    _dbContext.BlogInfos.Remove(BlogInfo);
                    _dbContext.SaveChanges();
                    return BlogInfo;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}