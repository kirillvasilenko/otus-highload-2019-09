using System;
using System.Collections.Generic;
using System.Reflection;
using Dapper;
using SocialNetwork.Model;

namespace SocialNetwork.Repo.MySql
{
    public static class RefreshTokenMapper
    {
        public static SqlMapper.ITypeMap GetMapper()
        {
            var columnMaps = new Dictionary<string, string>
            {
                {"id", nameof(RefreshToken.Id)},
                {"user_id", nameof(RefreshToken.UserId)},
                {"token", nameof(RefreshToken.Token)},
                {"expiration_time", nameof(RefreshToken.ExpirationTime)}
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
            
            return new CustomPropertyTypeMap(typeof(RefreshToken), map) ;
        }
    }
}