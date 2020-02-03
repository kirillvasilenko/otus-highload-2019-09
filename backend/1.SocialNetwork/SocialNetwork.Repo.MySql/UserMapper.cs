using System;
using System.Collections.Generic;
using System.Reflection;
using Dapper;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public static class UserMapper
    {
        public static SqlMapper.ITypeMap GetMapper()
        {
            var columnMaps = new Dictionary<string, string>()
            {
                {"id", nameof(User.Id)},
                {"email", nameof(User.Email)},
                {"email_verified", nameof(User.EmailVerified)},
                {"password", nameof(User.Password)},
                {"given_name", nameof(User.GivenName)},
                {"family_name", nameof(User.FamilyName)},
                {"age", nameof(User.Age)},
                {"city", nameof(User.City)},
                {"interests", nameof(User.Interests)},
                {"is_active", nameof(User.IsActive)}
            };

            var map = new Func<Type, string, PropertyInfo>((type, columnName) =>
            {
                if (columnMaps.ContainsKey(columnName))
                {
                    return type.GetProperty(columnMaps[columnName]);
                }
                else
                {
                    return type.GetProperty(columnName);
                }
            });
            
            return new CustomPropertyTypeMap(typeof(User), map) ;
        }
    }
}