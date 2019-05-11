using AutoMapper;
using KnowledgeGraph.Application.Request;
using KnowledgeGraph.Web.Features.KnowledgeAuthor.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeCategory.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeConcept.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeContent.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeSource.ViewModels;
using KnowledgeGraph.Web.Features.KnowledgeSourceType.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeGraph.Web.Mapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            configureCategories();
            configureSources();
            configureSourceTypes();
            configureConcepts();
            configureContent();
            configureAuthors();
        }

        private void configureAuthors()
        {
            CreateMap<KnowledgeAuthorSimpleDto, KnowledgeAuthorForSourceViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}" ));

            CreateMap<KnowledgeAuthorDto, KnowledgeAuthorViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Sources, opt => opt.MapFrom(src => src.Sources))
            .ForMember(dest => dest.SourceCount, opt => opt.MapFrom(src => src.Sources.Count()));

            CreateMap<KnowledgeAuthorDto, DetailsKnowledgeAuthorViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Sources, opt => opt.MapFrom(src => src.Sources))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationTime))
            .ForMember(dest => dest.LastModificationDate, opt => opt.MapFrom(src => src.LastModificationTime))
            .ForMember(dest => dest.SourceCount, opt => opt.MapFrom(src => src.Sources.Count()));
        }

        private void configureContent()
        {

            CreateMap<KnowledgeContentDto, KnowledgeContentViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
            .ForMember(dest => dest.CreationDate, opt => opt.MapFrom(src => src.CreationDate))
            .ForMember(dest => dest.LastModificationDate, opt => opt.MapFrom(src => src.LastModificationDate));

            CreateMap<KnowledgeContentDto, EditKnowledgeContentViewModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
            .ForMember(dest => dest.SourceId, opt => opt.MapFrom(src => src.Source.Id));

            CreateMap<KnowledgeSourceWithContentDto, IndexKnowledgeContentViewModel>()
                .ForMember(dest => dest.Source, opt => opt.MapFrom(src => src.Source))
                .ForMember(dest => dest.KnowledgeContentViewModels, opt => opt.MapFrom(src => src.Contents));

        }

        private void configureConcepts()
        {
            CreateMap<KnowledgeConceptSimpleDto, KnowledgeConceptForDetailsKnowledgeCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<KnowledgeConceptDto, EditKnowledgeConceptViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Category.Id));

            CreateMap<KnowledgeConceptSimpleDto, KnowledgeConceptSimpleViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ContentCount, opt => opt.MapFrom(src => src.ContentCount));

            CreateMap<KnowledgeConceptDto, DetailsKnowledgeConceptViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
                .ForMember(dest => dest.Contents, opt => opt.MapFrom(src => src.Contents.OrderByDescending(c=>c.LastModificationDate)));
        }

        private void configureSourceTypes()
        {
            CreateMap<KnowledgeSourceTypeDto, ListKnowledgeSourceTypeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.UsageCount, opt => opt.MapFrom(src => src.Sources.Count()));

            CreateMap<KnowledgeSourceTypeDto, DetailsKnowledgeSourceTypeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
                .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => src.LastModificationTime))
                .ForMember(dest => dest.Sources, opt => opt.MapFrom(src => src.Sources));
        }

        private void configureSources()
        {
            CreateMap<KnowledgeSourceDto, ListKnowledgeSourceViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.SourceType, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.AuthorList, opt => opt.MapFrom(src => src.Authors));

            CreateMap<KnowledgeSourceDto, DetailsKnowledgeSourceViewModel>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
             .ForMember(dest => dest.SourceType, opt => opt.MapFrom(src => src.Type))
             .ForMember(dest => dest.CreationTime, opt => opt.MapFrom(src => src.CreationTime))
             .ForMember(dest => dest.LastModificationTime, opt => opt.MapFrom(src => src.LastModificationTime));

            CreateMap<KnowledgeSourceDto, EditKnowledgeSourceViewModel>()
              .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
              .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
              .ForMember(dest => dest.SourceTypeId, opt => opt.MapFrom(src => src.Type.Id))
              .ForMember(dest => dest.AuthorIds, opt => opt.MapFrom(src => src.Authors.Select(a=>a.Id).ToList()));
        }

        private void configureCategories()
        {
            CreateMap<KnowledgeCategoryDto, KnowledgeCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<KnowledgeCategoryWithConceptsDto, KnowledgeCategoryWithConceptsViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dest => dest.KnowledgeConcepts, opt => opt.MapFrom(src => src.KnowledgeConcepts));

            CreateMap<KnowledgeCategoryWithConceptsDto, ListKnowledgeCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.UsageCount, opt => opt.MapFrom(src => src.KnowledgeConcepts.Count()));

            CreateMap<KnowledgeCategoryWithConceptsDto, DetailsKnowledgeCategoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Category.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.KnowledgeConcepts, opt => opt.MapFrom(src => src.KnowledgeConcepts));
        }
    }
}
