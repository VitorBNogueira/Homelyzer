using Application.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    internal static class FilterAndOrder
    {
        public static IQueryable<T> Order<T>(IQueryable<T> queriable, SortDTO sort)
        {
            if (sort == null || string.IsNullOrWhiteSpace(sort.OrderBy))
                return queriable;

            var propertyName = char.ToUpper(sort.OrderBy[0]) + sort.OrderBy[1..];

            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExpression;

            if (sort.Direction == EDirection.Asc)
                resultExpression = Expression.Call(typeof(Queryable), "OrderBy", new Type[] { type, property.PropertyType }, queriable.Expression, Expression.Quote(orderByExp));
            else
                resultExpression = Expression.Call(typeof(Queryable), "OrderByDescending", new Type[] { type, property.PropertyType }, queriable.Expression, Expression.Quote(orderByExp));

            return queriable.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
