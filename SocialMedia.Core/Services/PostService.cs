using System;
using System.Collections.Generic;
using System.Text;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> _postRepository;
        public PostService(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }
        public ServiceResult<IReadOnlyList<Post>> GetPosts()
        {
            var posts = _postRepository.Get();
            return ServiceResult<IReadOnlyList<Post>>.SuccessResult(posts);
        }

        public ServiceResult<Post> GetPostsById(int id)
        {
            var post = _postRepository.Get(id);
            if (post == null)
            {
                return ServiceResult<Post>.NotFoundResult($"No se encontró un post con el id {id}");
            }

            return ServiceResult<Post>.SuccessResult(post);
        }

        public ServiceResult<Post> CreatePost(Post entity)
        {
            var post = _postRepository.Get(entity.Id);
            if (post == null)
            {
                return ServiceResult<Post>.ErrorResult($"Ya existe un post con el id {entity.Id}");
            }

            return ServiceResult<Post>.SuccessResult(post);
        }
    }
}
