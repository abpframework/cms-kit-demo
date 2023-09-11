using AutoMapper;
using CmsKitDemo.Entities;
using CmsKitDemo.Services.Dtos;

namespace CmsKitDemo.ObjectMapping;

public class CmsKitDemoAutoMapperProfile : Profile
{
    public CmsKitDemoAutoMapperProfile()
    {
        /* Create your AutoMapper object mappings here */

        CreateMap<CreateUpdateGalleryImageDto, GalleryImage>().ReverseMap();

        CreateMap<GalleryImage, GalleryImageDto>().ReverseMap();

    }
}
