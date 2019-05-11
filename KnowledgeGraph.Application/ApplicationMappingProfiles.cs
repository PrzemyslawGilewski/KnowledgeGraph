using AutoMapper;
using KnowledgeGraph.Data.Model;
using System.Linq;

namespace KnowledgeGraph.Application
{
    public class ApplicationMappingProfiles : Profile
    {
        public ApplicationMappingProfiles()
        {
            configureConcepts();
            configureCategories();
            configureContents();
            configureSources();
            configureSourceTypes();
            configureAuthors();
        }

        private void configureAuthors()
        {
            CreateMap<KnowledgeAuthor, Request.KnowledgeAuthorDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
            .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => src.LastModificationTime))
            .ForMember(dest => dest.Sources, opt => opt.MapFrom(src => src.AuthorSource.Select(autSour => autSour.Source)));
        }

        private void configureContents()
        {
            CreateMap<KnowledgeContent, Command.KnowledgeContentDto>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
              .ForMember(dest => dest.Concept, opt => opt.MapFrom(src => src.Concept));

            CreateMap<KnowledgeContent, Request.KnowledgeContentInConceptDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
        }

        private void configureSourceTypes()
        {
            CreateMap<KnowledgeSourceType, Request.KnowledgeSourceTypeDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Sources, opt => opt.MapFrom(src => src.Sources));
        }

        private void configureSources()
        {
            CreateMap<KnowledgeSource, Request.KnowledgeSourceDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
               .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.AuthorSource.Select(autSour => autSour.Author)))
               .ForMember(dest => dest.UsageCount, opt => opt.MapFrom(src => src.Contents.Count));

            CreateMap<KnowledgeSource, Command.KnowledgeSourceDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));
        }

        private void configureCategories()
        {
            CreateMap<KnowledgeCategory, Request.KnowledgeCategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment));

            CreateMap<KnowledgeCategory, Request.KnowledgeCategoryWithConceptsDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src))
                .ForMember(dest => dest.KnowledgeConcepts, opt => opt.MapFrom(src => src.KnowledgeConcepts));


        }

        private void configureConcepts()
        {
            CreateMap<KnowledgeConcept, Request.KnowledgeConceptDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents));

            CreateMap<KnowledgeConcept, Request.KnowledgeConceptSimpleDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ContentCount, opt => opt.MapFrom(src => src.Contents == null ? 0 : src.Contents.Count));
        }
    }
}
