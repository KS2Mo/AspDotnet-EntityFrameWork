using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PGMKTStock
{
    public static class Data
    {
        public static T Clone<T>(this T source) where T : DBStockMarketEntities
        {
            var ser = new DataContractSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                ser.WriteObject(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)ser.ReadObject(stream);
            }
        }
        //public static T Clone<T>(this T source)
        //{
        //    var dcs = new DataContractSerializer(typeof(T));
        //    using (var ms = new System.IO.MemoryStream())
        //    {
        //        dcs.WriteObject(ms, source);
        //        ms.Seek(0, System.IO.SeekOrigin.Begin);
        //        return (T)dcs.ReadObject(ms);
        //    }
        //}
    }


}