using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Auth.Commands
{
    public class TokenCommand : IRequest<BaseResponse<TokenDto>>
    {


    }
}
