using AutoMapper;
using WebTemplate.Models;
using WebTemplate.MVC.ViewModels.TestModel;

namespace WebTemplate.MVC
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<TestModelProfile>();
            });
        }
    }

    public class TestModelProfile : Profile
    {
        public TestModelProfile()
        {
            CreateMap<TestModel, TestModelListViewModel>();
            CreateMap<TestModel, TestModelDetailsViewModel>();

            CreateMap<TestModelCreateViewModel, TestModel>();
            CreateMap<TestModelUpdateViewModel, TestModel>().ReverseMap();
            CreateMap<TestModelDetailsViewModel, TestModel>();
        }
    }
}