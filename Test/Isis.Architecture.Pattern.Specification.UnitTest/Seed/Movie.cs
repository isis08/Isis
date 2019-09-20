using System;
using System.Collections.Generic;
using System.Linq;
using Isis.Architecture.Core.Domain.Specification;

namespace Isis.Architecture.Pattern.Specification.UnitTest.Seed
{
    internal class Movie
    {
        public virtual string Name { get; set; }

        public virtual DateTime ReleaseDate { get; set; }

        public virtual MpaaRating MpaaRating { get; set; }

        public virtual string Genre { get; set; }

        public virtual double Rating { get; set; }

        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }

    internal enum MpaaRating
    {
        G = 1,
        PG13 = 2,
        R = 3
    }

    internal class MovieRepository
    {
        private List<Movie> _movieTech;


        internal MovieRepository()
        {
            _movieTech = new List<Movie>
            {
                new Movie
                {
                    Name = "Las Vegas parano",
                    ReleaseDate = new DateTime(1998, 8, 19),
                    MpaaRating = MpaaRating.G,
                    Genre = "drama comedy",
                    Rating = 10
                },
                new Movie
                {
                    Name = "C\'est arrivé près de chez vous",
                    ReleaseDate = new DateTime(1992, 11, 4),
                    MpaaRating = MpaaRating.R,
                    Genre = "drama comedy",
                    Rating = 9
                },
                new Movie
                {
                    Name = "Slumdog Millionaire",
                    ReleaseDate = new DateTime(2009, 1, 14),
                    MpaaRating = MpaaRating.PG13,
                    Genre = "drama",
                    Rating = 8
                },
                new Movie
                {
                    Name = "Les clefs de bagnole",
                    ReleaseDate = new DateTime(2003, 12, 10),
                    MpaaRating = MpaaRating.G,
                    Genre = "comedy",
                    Rating = 1
                }
            };
        }

        public IEnumerable<Movie> Find(IRootSpecification<Movie> specification)
        {
            var movies = _movieTech.AsQueryable();
            return movies.Where(specification.ToExpression());
        }
    }
    
}
