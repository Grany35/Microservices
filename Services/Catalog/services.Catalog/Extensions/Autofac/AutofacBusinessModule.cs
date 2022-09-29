using Autofac;
using AutoMapper;
using services.Catalog.Extensions.Mapper;
using services.Catalog.Services;
using services.Catalog.Settings;

namespace services.Catalog.Extensions.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CategoryManager>().As<ICategoryService>();
            builder.RegisterType<CourseManager>().As<ICourseService>();

        }
    }
}
