using AutoMapper;
using Dictionary.Application.Abstractions.Repositories;
using Dictionary.Common.Features.Users.Queries;
using Dictionary.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Application.Features.Users.Queries
{
    public sealed class GetUserDetailQueryRequest:IRequest<GetUserDetailQueryResponse>
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
    }

    public sealed class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQueryRequest, GetUserDetailQueryResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserDetailQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<GetUserDetailQueryResponse> Handle(GetUserDetailQueryRequest request, CancellationToken cancellationToken)
        {
            User user=null;
            if (request.UserId==Guid.Empty)
            {
                user = await _userRepository.GetByIdAsync(request.UserId);
            }
            else if (!string.IsNullOrEmpty(request.UserName))
            {
                user = await _userRepository.GetSingleAsync(e=>e.UserName == request.UserName);
            }
            if (user == null) throw new Exception("User is null");

            return _mapper.Map<GetUserDetailQueryResponse>(user);
        }
    }

    public sealed class GetUserDetailQueryResponse:UserDetailViewModel
    {
    }
}
