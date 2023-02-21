using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using edk.Fusc.Contracts;
using edk.Fusc.Core;
using edk.Fusc.Core.Validators;
using edk.Kchef.Application.Common;
using edk.Kchef.Domain.Common;
using edk.Kchef.Domain.Contracts.Repositories;
using edk.Kchef.Domain.Contracts.Services;
using edk.Kchef.Domain.Entities.Users;
using edk.Tools;

namespace edk.Kchef.Application.Features.Users.Create;

public class CreateUserUseCase : UseCase<CreateUserInput, UserOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    protected override string NameUseCase => "CreateUserUseCase";

    public CreateUserUseCase(IUnitOfWork unitOfWork
        , IUserRepository userRepository
        , IPasswordService passwordService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    protected override async Task<bool> OnActionBeforeStartAsync(CreateUserInput input, IUser user)
    {
        VerifyPasswordStrength(input);

        await CheckLoginAvailability(input);

        return Notifications.NoErrors();
       
    }


    public override async Task<UserOutput> OnExecuteAsync(CreateUserInput input, CancellationToken cancellationToken)
    {

        var userNew = input.ToDomain();

        var password = _passwordService.GenerateHash(userNew, input.Password);

        userNew.SetPassword(password, CurrentDate.Value);

        await _userRepository.AddAsync(userNew).ConfigureAwait(false);

        (await _unitOfWork.CommitAsync())
            .WhenFalse(() => SetNotification(Notification.Error("Não foi possível cadastrar o usuário.")));

        return userNew.ToOutput();

    }

    private void VerifyPasswordStrength(CreateUserInput input)
    {
        if (_passwordService.CheckForce(input.Password).IsFalse())
        {
            SetNotification(Notification.Error("Senha fora dos padrões de segurança."));
        }
    }

    private async Task CheckLoginAvailability(CreateUserInput input)
    {
        Expression<Func<User, bool>> condition = (u) => u.Login.Equals(input.Login);
        var userOption = await _userRepository.FirstOrDefaultAsync(condition);

        if (userOption.NotNull)
        {
            SetNotification(Notification.Error("Login indisponível."));
        }
    }
}