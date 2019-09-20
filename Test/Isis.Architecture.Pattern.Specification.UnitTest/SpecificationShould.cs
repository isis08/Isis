using System;
using System.Linq;
using Isis.Architecture.Pattern.Specification.UnitTest.Seed;
using Xunit;

namespace Isis.Architecture.Pattern.Specification.UnitTest
{
    public class SpecificationShould
    {
        [Fact]
        public void UseSimpleExpression()
        {
            //-- Arrange
            var gRating = new MpaaRatingAtMostSpecification(MpaaRating.G);
            var repository = new MovieRepository();

            //-- Act
            var movies = repository.Find(gRating);

            //-- Assert
            Assert.Equal(2, movies.Count());

        }

        [Fact]
        public void SatisfyNotOneCriteria()
        {
            //-- Arrange
            var movie = new Movie
            {
                Name = "C\'est arrivé près de chez vous",
                ReleaseDate = new DateTime(1992, 11, 4),
                MpaaRating = MpaaRating.R,
                Genre = "drama comedy",
                Rating = 9
            };

            var pg13Rating = new MpaaRatingAtMostSpecification(MpaaRating.G);

            //-- Act
            bool isSatisfiedBy = pg13Rating.IsSatisfiedBy(movie);

            //-- Assert
            Assert.False(isSatisfiedBy);
        }

        [Fact]
        public void SatisfyOneCriteria()
        {
            //-- Arrange
            var movie = new Movie
            {
                Name = "Las Vegas parano",
                ReleaseDate = new DateTime(1998, 8, 19),
                MpaaRating = MpaaRating.G,
                Genre = "drama comedy",
                Rating = 10
            };

            var pg13Rating = new MpaaRatingAtMostSpecification(MpaaRating.G);

            //-- Act
            bool isSatisfiedBy = pg13Rating.IsSatisfiedBy(movie);

            //-- Assert
            Assert.True(isSatisfiedBy);
        }

        [Fact]
        public void UseAndComposition()
        {
            //-- Arrange
            var gRating = new MpaaRatingAtMostSpecification(MpaaRating.G);
            var goodMovie = new GoodMovieSpecification();
            var repository = new MovieRepository();

            //-- Act
            var movies = repository.Find(gRating.And(goodMovie));

            //-- Assert
            Assert.Single(movies);
        }

        [Fact]
        public void UseOrComposition()
        {
            //-- Arrange
            var gRating = new MpaaRatingAtMostSpecification(MpaaRating.G);
            var goodMovie = new GoodMovieSpecification();
            var repository = new MovieRepository();

            //-- Act
            var movies = repository.Find(gRating.Or(goodMovie));

            //-- Assert
            Assert.Equal(4, movies.Count());
        }

        [Fact]
        public void UseNotComposition()
        {
            //-- Arrange
            var goodMovie = new GoodMovieSpecification();
            var repository = new MovieRepository();

            //-- Act
            var movies = repository.Find(goodMovie.Not());

            //-- Assert
            Assert.Single(movies);
        }
    }
}
