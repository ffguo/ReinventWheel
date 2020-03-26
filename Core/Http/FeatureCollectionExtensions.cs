using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Http
{
    public static class FeatureCollectionExtensions
    {
        public static IFeatureCollection Set<T>(this IFeatureCollection featureCollection, T feature)
        {
            featureCollection[typeof(T)] = feature;
            return featureCollection;
        }

        public static T Get<T>(this IFeatureCollection featureCollection)
        {
            return (T)featureCollection[typeof(T)];
        }
    }
}
