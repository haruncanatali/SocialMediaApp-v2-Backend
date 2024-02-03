using AutoMapper;
using SHFT.Application.Common.Mappings;
using SHFT.Domain.Entities;

namespace SHFT.Application.Posts.Queries.Dtos;

public class PostDto : IMapFrom<Post>
{
    public long Id { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public long UserId { get; set; }
    public string UserFullName { get; set; }
    public string UserPhoto { get; set; }
    public bool Fallowed { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Post, PostDto>()
            .ForMember(dest => dest.UserFullName, opt =>
                opt.MapFrom(c=>c.User.FullName))
            .ForMember(dest => dest.UserPhoto, opt =>
                opt.MapFrom(c=>c.User.ProfilePhoto));
    }
}