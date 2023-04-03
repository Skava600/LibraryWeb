using LibraryWeb.Contracts.Data.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Core.Handlers.Commands
{
    public class LogoutUserCommand: IRequest<int>
    { 
    }
    public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, int>
    {
        private readonly SignInManager<User> signInManager;
        public LogoutUserCommandHandler(SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
        }

        public async Task<int> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            await signInManager.SignOutAsync();

            return 0;
        }
    }
}
