using DocumentAdministration.API.Core.Interfaces.Database;
using DocumentAdministration.API.Data;
using DocumentAdministration.API.Data.Entity;
using DocumentAdministration.API.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DocumentAdministration.API.Test.Helpers
{
    public class DbContextHelper
    {
        private readonly DocumentAdministrationDbContext documentAdministrationDbContext;
        public DbContextHelper()
        {
            var builder = new DbContextOptionsBuilder<DocumentAdministrationDbContext>();
            builder.UseInMemoryDatabase(databaseName: $"DocumentAdministrationDbInMemory{Guid.NewGuid()}");

            var dbContextOptions = builder.Options;
            documentAdministrationDbContext = new DocumentAdministrationDbContext(dbContextOptions);

            documentAdministrationDbContext.Database.EnsureDeleted();
            documentAdministrationDbContext.Database.EnsureCreated();

            var mockDocumentData =
                new List<Document>
                {
                    new Document
                    {
                        DocumentId = new Guid("dea1d4cfd13f4fa2994b2207078ae0bf"),
                        Name ="Laptop"
                    },
                    new Document
                    {
                        DocumentId =  new System.Guid("091c390763eb49c899434f65194b6f2d"),
                        Name ="Mouse"
                    }
                    
                };

            var mockKeywordData = new List<DocumentKeywordDetail>
            {
                new DocumentKeywordDetail()
                {
                    DocumentId = new System.Guid("091c390763eb49c899434f65194b6f2d"),
                    Keyword ="DELLNEW50",
                    KeywordId = new Guid("b5786f3e6f4041939358d85f05c602bb")
                },
                 new DocumentKeywordDetail()
                {
                    DocumentId = new System.Guid("dea1d4cfd13f4fa2994b2207078ae0bf"),
                    Keyword ="DELLNEW50",
                    KeywordId = new Guid("cc45c08d995740ebb83169c1ba852f9f")
                }
            };
            documentAdministrationDbContext.AddRange(mockDocumentData);
            documentAdministrationDbContext.AddRange(mockKeywordData);
            documentAdministrationDbContext.SaveChanges();


        }

        public IDocumentRepository GetInMemoryDocumentRepository()
        {
            return new DocumentRepository(documentAdministrationDbContext);
        }

        public IDocumentKeywordDetailsRepository GetInMemoryDocumentKeywordDetailsRepository()
        {
            return new DocumentKeywordDetailsRepository(documentAdministrationDbContext);
        }
    }

    //public class InMemoryTest : TestRegistration
    //{
    //    public InMemoryTest()
    //        : base(new DbContextOptionsBuilder<DocumentAdministrationDbContext>()
    //                .UseInMemoryDatabase("TestDatabase")
    //                .Options)
    //    {
    //    }
    //}


    public abstract class TestRegistration
    {
        #region Seeding
        public TestRegistration(DbContextOptions<DocumentAdministrationDbContext> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }

        protected DbContextOptions<DocumentAdministrationDbContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new DocumentAdministrationDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                //var one = new Register()
                //{
                //    Name = "Test One",
                //    Age = 40
                //};

                //var two = new Register()
                //{
                //    Name = "Test Two",
                //    Age = 50
                //};

                //var three = new Register()
                //{
                //    Name = "Test Three",
                //    Age = 60
                //};
                //context.AddRange(one, two, three);
                //context.SaveChanges();
            }
        }
        #endregion
    }

}
