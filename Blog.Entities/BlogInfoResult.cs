using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Entities
{
    public class BlogInfoResult
    {
        public int ErrorCode { get; set; }
        /// <summary>
        /// Error description. Optional.
        /// </summary>
        public string ErrorMessage { get; set; }


        public BlogInfoResult()
        {
            //These are defaults, but lets write it out for emphasis.
            this.ErrorCode = 0;
            this.ErrorMessage = "Blog Created Successfully";
        }
    }
}