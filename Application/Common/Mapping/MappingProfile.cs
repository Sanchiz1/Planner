﻿using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Membership, MembershipDto>();
        CreateMap<Workspace, WorkspaceDto>();
        CreateMap<Domain.Entities.Task, TaskDto>();
        CreateMap<Tag, TagDto>();
    }
}
