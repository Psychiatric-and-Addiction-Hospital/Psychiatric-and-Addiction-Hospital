using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Profile;
using Application.Common.Interfaces.UpLoad;
using Application.Common.Responses;
using Application.DTOS.Request.Profile;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Profile
{
    public class ChangeProfileImageService : IChangeProfileImage
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IFileStorage _fileStorage;

        public ChangeProfileImageService(AddIdentityDbContext context, ICurrentUser currentUser, IFileStorage fileStorage)
        {
            _context = context;
            _currentUser = currentUser;
            _fileStorage = fileStorage;
        }
        public async Task<BaseResponse<string>> ChangeAsync(ChangeProfileImageRequest request, CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<string>("User is not authenticated.");

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == _currentUser.UserId, ct);

            if (user == null)
                return ResponseFactory.Fail<string>("User not found.");

            if (!_fileStorage.IsValidImage(request.ImageUrl))
                return ResponseFactory.Fail<string>("Invalid image.");

            if (!string.IsNullOrWhiteSpace(user.ImageUrl))
                await _fileStorage.DeleteFileAsync(user.ImageUrl);


            var image = await _fileStorage.SaveFileAsync(request.ImageUrl, "ProfileImages", ct);

            user.ImageUrl = image;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(image!, "Profile image updated successfully.");
        }
    }
}
