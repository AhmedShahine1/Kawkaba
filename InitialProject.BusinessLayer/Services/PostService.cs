using AutoMapper;
using Kawkaba.BusinessLayer.Interfaces;
using Kawkaba.Core.DTO.AuthViewModel;
using Kawkaba.Core.DTO.AuthViewModel.PostsModel;
using Kawkaba.Core.Entity.Files;
using Kawkaba.Core.Entity.Posts;
using Kawkaba.RepositoryLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kawkaba.BusinessLayer.Services
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IFileHandling _fileHandling;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper, IFileHandling fileHandling)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileHandling = fileHandling;
        }

        public async Task<PostDTO> CreatePostAsync(PostDTO postDto, string companyId)
        {
            var post = _mapper.Map<Post>(postDto);
            post.CompanyId = companyId;

            // Handle file uploads and generate file URLs
            var images = new List<Images>();
            var fileUrls = new List<string>();
            var path = await _unitOfWork.PathsRepository.FindAsync(a => a.Name == "Posts");

            foreach (var file in postDto.Files)
            {
                var fileId = await _fileHandling.UploadFile(file, path);
                var image = await _unitOfWork.ImagesRepository.GetByIdAsync(fileId);
                images.Add(image);

                // Get file URL
                var fileUrl = await _fileHandling.GetFile(fileId);
                fileUrls.Add(fileUrl);
            }
            post.Files = images;

            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();

            // Return the PostDTO with file URLs
            var postResponse = _mapper.Map<PostDTO>(post);
            postResponse.FileUrls = fileUrls;

            return postResponse;
        }

        public async Task<IEnumerable<PostDTO>> GetCompanyPostsAsync(string companyId)
        {
            var posts = await _unitOfWork.PostRepository.FindAllAsync(p => p.CompanyId == companyId, include: q => q
                .Include(a => a.Files)
                .Include(a => a.Company));
            var postDtos = new List<PostDTO>();

            foreach (var post in posts)
            {
                var postDto = _mapper.Map<PostDTO>(post);

                // Get URLs for the files
                var fileUrls = new List<string>();
                foreach (var image in post.Files)
                {
                    var fileUrl = await _fileHandling.GetFile(image.Id);
                    fileUrls.Add(fileUrl);
                }
                postDto.FileUrls = fileUrls;
                postDto.Auth = _mapper.Map<AuthDTO>(post.Company);
                postDto.Auth.ProfileImage = await _fileHandling.GetFile(post.Company.ProfileId);
                postDto.Created = post.Date;
                postDtos.Add(postDto);
            }

            return postDtos;
        }

        public async Task<bool> DeletePostAsync(string postId)
        {
            try
            {
                var post = await _unitOfWork.PostRepository.FindAsync(a=>a.Id == postId,include:q=>q.Include(s=>s.Files));
                if (post == null)
                    return false;
                foreach(var fileUrl in post.Files)
                {
                    _unitOfWork.ImagesRepository.Delete(fileUrl);
                }
                _unitOfWork.PostRepository.Delete(post);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
