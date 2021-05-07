using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia.Core.Entities;

namespace SocialMedia.Core.Interfaces
{
    public interface IPostService
    {
        ServiceResult<IReadOnlyList<Post>> GetPosts();

        ServiceResult<Post> GetPostsById(int id);

        ServiceResult<Post> CreatePost(Post entity);
    }
}
