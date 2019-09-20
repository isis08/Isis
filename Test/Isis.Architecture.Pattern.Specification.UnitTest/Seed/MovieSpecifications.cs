using System;
using System.Linq.Expressions;

namespace Isis.Architecture.Pattern.Specification.UnitTest.Seed
{
    internal class MpaaRatingAtMostSpecification : RootSpecification<Movie>
    {
        private readonly MpaaRating _rating;

        public MpaaRatingAtMostSpecification(MpaaRating rating)
        {
            _rating = rating;
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.MpaaRating <= _rating;
        }
    }

    internal class GoodMovieSpecification : RootSpecification<Movie>
    {
        private const double Threshold = 7;
        
        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Rating >= Threshold;
        }
    }


}
