using AutoMapper;
using FluentValidation;
using IdentityServer4;
using IdentityServer4.Events;
using LibraryWeb.Contracts.Data.Entities;
using LibraryWeb.Contracts.DTO;
using LibraryWeb.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Core.Handlers.Commands
{
    public class LoginUserCommand: IRequest<UserDTO>
    {
        public LoginUserCommand(LoginDTO model)
        {
            Model = model;
        }

        public LoginDTO Model { get; }
    }

    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, UserDTO>
    {
        private readonly UserManager<User> userManager;
        private readonly IValidator<LoginDTO> validator;
        private readonly SignInManager<User> signInManager;
        private readonly IMapper mapper;
        public LoginUserCommandHandler(UserManager<User> userManager, IMapper mapper, IValidator<LoginDTO> validator, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.validator = validator;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }
        public async Task<UserDTO> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var result = validator.Validate(model);

            if (!result.IsValid)
            {
                throw new InvalidRequestBodyException
                {
                    Errors = result.Errors.Select(x => x.ErrorMessage).ToArray()
                };
            }

            var entityUser = await userManager.FindByEmailAsync(model.Email);
            if (entityUser != null
            && (await signInManager.CheckPasswordSignInAsync(entityUser, model.Password, true)) == SignInResult.Success)
            {
                var isuser = new IdentityServerUser(entityUser.Id)
                {
                    DisplayName = entityUser.UserName
                };

                await signInManager.SignInAsync(entityUser, true);
            }
            else
            {
                throw new InvalidRequestBodyException
                {
                    Errors = new string[] {"Invalid login or password"}
                };
            }

            var userDTO = mapper.Map<UserDTO>(entityUser);

            return userDTO;
        }
    }

}
