using Mapster;
using Domain.Entities;
using Application.Contexts.Comments.Dtos;

namespace Application.Mappings;
public class CommentMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Comment, CommentDto>()
            .Map(dest => dest.UserName, src => src.User!.UserName);
    }
}
