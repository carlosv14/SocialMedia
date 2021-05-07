using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<Post> _postRepository;

        public CommentService(IRepository<Comment> commentRepository, IRepository<Post> postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }
        public ServiceResult<IReadOnlyList<Comment>> GetComments()
        {
            var comments = _commentRepository.Get();
            return ServiceResult<IReadOnlyList<Comment>>.SuccessResult(comments);
        }

        public ServiceResult<Comment> GetCommentsById(int id)
        {
            var comment = _commentRepository.Get(id);
            if (comment == null)
            {
                return ServiceResult<Comment>.NotFoundResult($"No se encontró un comentario con el id {id}");
            }

            return ServiceResult<Comment>.SuccessResult(comment);
        }

        public ServiceResult<Comment> CreateComment(Comment entity)
        {
            var post = _postRepository.Get(entity.PostId);
            if (post == null)
            {
                return ServiceResult<Comment>.NotFoundResult($"No se encontró un post con el id {entity.PostId}");
            }

            var comment = _commentRepository.Get(entity.Id);
            if (comment == null)
            {
                return ServiceResult<Comment>.ErrorResult($"Ya existe un comentario con el id {entity.Id}");
            }

            return ServiceResult<Comment>.SuccessResult(comment);
        }

        public ServiceResult<IReadOnlyList<Comment>> FilterCommentsByPostId(int postId)
        {
            var post = _postRepository.Get(postId);
            if (post == null)
            {
                return ServiceResult<IReadOnlyList<Comment>>.NotFoundResult($"No se encontró un post con el id {postId}");
            }
            //var comments = _commentRepository.Filter(x => x.PostId == postId);
            if (!post.Comments.Any())
            {
                return ServiceResult<IReadOnlyList<Comment>>.NotFoundResult($"No hay comentarios para el post {postId}");
            }

            return ServiceResult<IReadOnlyList<Comment>>.SuccessResult(post.Comments.ToList());
        }

    }
}
