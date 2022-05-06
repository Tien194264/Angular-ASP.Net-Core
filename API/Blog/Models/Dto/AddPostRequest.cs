using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Models.Dto
{
    public class AddPostRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public string UrlHandle { get; set; }
        public string FeatureImageUrl { get; set; }
        public bool Visible { get; set; }

        public string Author { get; set; }
        public DateTime PushlishDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}