﻿using Business.Models.Request.Functional;
using Business.Models.Response;
using Business.Services.Interface;
using Business.Utilities.Security.Auth;
using Business.Utilities.Security.Auth.Interface;
using Core.Results;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService, IHashingHelper hashingHelper)
        {
            _authService = authService;

        }
        [HttpPost]

        public async Task<ActionResult<DataResult<Token>>> Login([FromBody] LoginDto loginDto)
            => await _authService.Login(loginDto);

        [HttpPost]
        public async Task<ActionResult<DataResult<Token>>> Register([FromBody] RegisterDto registerDto)
            => await _authService.Register(registerDto);

        [HttpGet]

        public async Task<ActionResult<DataResult<UserProfileDto>>> GetProfileInfo()
            => await _authService.GetUserProfileInfo();

        [HttpGet]
        public async Task<ActionResult<DataResult<Token>>> RefreshToken(string token)
            => await _authService.RefreshToken(token);
    }
}
