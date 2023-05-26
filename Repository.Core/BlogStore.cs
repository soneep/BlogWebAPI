using Blog.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Repository.Core
{
    public class BlogStore : IBlogStore
    {
        private readonly ILogger _logger;
        readonly ApplicationDbContext _dbContext = new();

        public BlogStore(ApplicationDbContext dbContext, ILoggerFactory loggerFactory)
        {
            _dbContext = dbContext;
            _logger = loggerFactory.CreateLogger<BlogStore>();
        }

        /// <summary>
        /// This method is used to list all available blog information from database
        /// </summary>
        /// <returns></returns>
        public List<BlogInfo> GetBlogInfo()
        {
            List<BlogInfo> blogInfo = new List<BlogInfo>();
            try
            {
                blogInfo = _dbContext.BlogInfos.ToList();
            }
            catch(Exception ex) 
            {
                _logger.LogError("Error occuured in BlogStore => GetBlogInfo method. Exception details : "+ex.Message);
            }
            return blogInfo;
        }

        /// <summary>
        /// This method is used find blog information by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BlogInfo GetBlogInfo(int id)
        {
            BlogInfo blogInfo =new BlogInfo();
            try
            {
                blogInfo = _dbContext.BlogInfos.Find(id);
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogStore => GetBlogInfoById method. Exception details : "+ex.Message);
            }
            return blogInfo;
        }

        public void AddBlogInfo(BlogInfo BlogInfo)
        {
            try
            {
                _dbContext.BlogInfos.Add(BlogInfo);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogStore => AddBlogInfo method. Exception details : "+ex.Message);
            }
        }

        public void UpdateBlogInfo(BlogInfo BlogInfo)
        {
            try
            {
                _dbContext.Entry(BlogInfo).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogStore => UpdateBlogInfo method. Exception details : "+ex.Message);
            }
        }

        public BlogInfo DeleteBlogInfo(int id)
        {   
            BlogInfo blogInfo = GetBlogInfo(id);
            try
            {
                if (blogInfo != null)
                {
                    _dbContext.BlogInfos.Remove(blogInfo);
                    _dbContext.SaveChanges();                    
                }
            }
            catch(Exception ex)
            {
                _logger.LogError("Error occuured in BlogStore => DeleteBlogInfo method. Exception details : "+ex.Message);
            }
            return blogInfo;
        }
    }
}