using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;
using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<IApplicationUser, UserDto>();
        CreateMap<Membership, MembershipDto>();
        CreateMap<UserMembership, UserMembershipDto>();
        CreateMap<Workspace, WorkspaceDto>();
        CreateMap<Domain.Entities.Task, TaskDto>();
        CreateMap<Tag, TagDto>();
    }
}
