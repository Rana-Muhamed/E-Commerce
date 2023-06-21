using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.Core.Entities.Identity;
using talabat.Core.Services;
using Talabat.DTOs;
using Talabat.Errors;
using Talabat.Extensions;

namespace Talabat.Controllers
{
 
    public class AccountsController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }


        [HttpPost("login")] // POST: /api/Accounts/login

        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
        }



        [HttpPost("register")] // POST: /api/Accounts/Register

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(CheckEmailEixsts(registerDto.Email).Result.Value)
                return BadRequest(new ApiValidationErrorResponse() { Errors= new string[] {"this email is already used"} });
            var user = new AppUser()
            {

                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email, // ahmed.nasr@linkdev.com
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Email.Split("@")[0]//ahmed.nasr
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));

            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
    }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName= user.DisplayName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
            });
            
        }
        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {

            var user = await _userManager.FindUserWithAddressAsync(User);
            var address = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
            
        }
        [Authorize]
        [HttpPut("address")]
        public async Task<ActionResult<AddressDto>> UpadteUserAddress(AddressDto updatedAddress)
        {
            var address =  _mapper.Map<AddressDto, Address>(updatedAddress);
            var user = await _userManager.FindUserWithAddressAsync(User);
            address.Id= user.Address.Id;
            user.Address = address;
            var result = await _userManager.UpdateAsync(user);
            if(!result.Succeeded) return BadRequest(new ApiResponse(400));
            return Ok(updatedAddress);

        }
        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailEixsts(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null; 

        }
    }
}
