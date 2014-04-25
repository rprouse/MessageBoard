using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace MessageBoard.Data
{
    public class MessageBoardMigrationsConfiguration : DbMigrationsConfiguration<MessageBoardContext>
    {
        public MessageBoardMigrationsConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }

        #region Overrides of DbMigrationsConfiguration<MessageBoardContext>

        /// <summary>
        /// Runs after upgrading to the latest migration to allow seed data to be updated.
        /// </summary>
        /// <remarks>
        /// Note that the database may already contain seed data when this method runs. This means that
        ///             implementations of this method must check whether or not seed data is present and/or up-to-date
        ///             and then only make changes if necessary and in a non-destructive way. The 
        ///             <see cref="M:System.Data.Entity.Migrations.DbSetMigrationsExtensions.AddOrUpdate``1(System.Data.Entity.IDbSet{``0},``0[])"/>
        ///             can be used to help with this, but for seeding large amounts of data it may be necessary to do less
        ///             granular checks if performance is an issue.
        ///             If the <see cref="T:System.Data.Entity.MigrateDatabaseToLatestVersion`2"/> database 
        ///             initializer is being used, then this method will be called each time that the initializer runs.
        ///             If one of the <see cref="T:System.Data.Entity.DropCreateDatabaseAlways`1"/>, <see cref="T:System.Data.Entity.DropCreateDatabaseIfModelChanges`1"/>,
        ///             or <see cref="T:System.Data.Entity.CreateDatabaseIfNotExists`1"/> initializers is being used, then this method will not be
        ///             called and the Seed method defined in the initializer should be used instead.
        /// </remarks>
        /// <param name="context">Context to be used for updating seed data. </param>
        protected override void Seed( MessageBoardContext context )
        {
            base.Seed( context );

#if DEBUG
            if ( !context.Topics.Any() )
            {
                var topic = new Topic
                {
                    Title = "I like C#",
                    Body = "It is the best language evar!!!",
                    Created = DateTime.Now,
                    Replies = new List<Reply>
                    {
                        new Reply
                        {
                            Body = "I love it too!",
                            Created = DateTime.Now
                        },
                        new Reply
                        {
                            Body = "Me too",
                            Created = DateTime.Now
                        },
                        new Reply
                        {
                            Body = "It sucks",
                            Created = DateTime.Now
                        },
                    }
                };
                context.Topics.Add(topic);
                
                topic = new Topic
                {
                    Title = "I prefer Ruby",
                    Body = "Rails rocks...",
                    Created = DateTime.Now,
                    Replies = new List<Reply>
                    {
                        new Reply
                        {
                            Body = "I love it too!",
                            Created = DateTime.Now
                        },
                        new Reply
                        {
                            Body = "Me too",
                            Created = DateTime.Now
                        },
                        new Reply
                        {
                            Body = "It sucks",
                            Created = DateTime.Now
                        },
                    }
                };
                context.Topics.Add(topic);
                try
                {
                    context.SaveChanges( );
                }
                catch ( Exception exception )
                {
                    Console.WriteLine( exception );
                }
            }
#endif
        }

        #endregion
    }
}