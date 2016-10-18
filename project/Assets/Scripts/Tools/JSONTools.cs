using UnityEngine;
using System.Collections;
using System.Text;
using TinyJSON;

public static class JSONTools {

    public static T toEnum<T>(this Variant self, T def = default(T))
    {
		var type = typeof(T);
        T ret = def;
		if (type.BaseType == typeof(System.Enum)) {
            try {
                if (self is ProxyNumber) {
					var intVal = (int)self;
					if (System.Enum.IsDefined(type, intVal)) {
						ret = (T)System.Enum.ToObject(type, intVal);
					}
                } else if (self is ProxyString) {
					var value = (string)self;
					if (!string.IsNullOrEmpty(value)) {
						ret = (T)System.Enum.Parse(typeof(T), value, true);
					}
                }
            } catch (System.Exception e) {
                LogMgr.E(string.Format("JSON->Enum({0}) Fail: {1}", typeof(T).FullName, e.Message));
            }
        }

        return ret;
	}

    public static T toValue<T>(this Variant self, string key, T defautValue = default(T)) where T : System.IConvertible
    {
        Variant ret = self[key];
        if (ret == null) return defautValue;

        if (typeof(T).BaseType == typeof(System.Enum)) {
            return ret.toEnum<T>();
        } else {
            var typeCode = defautValue != null ? defautValue.GetTypeCode() : System.TypeCode.String;
            System.IConvertible convertible = ret;
            switch (typeCode) {
                case System.TypeCode.Boolean: convertible = (System.Boolean)ret; break;
                case System.TypeCode.Single: convertible = (System.Single)ret; break;
                case System.TypeCode.Double: convertible = (System.Double)ret; break;
                case System.TypeCode.UInt16: convertible = (System.UInt16)ret; break;
                case System.TypeCode.Int16: convertible = (System.Int16)ret; break;
                case System.TypeCode.Int32: convertible = (System.Int32)ret; break;
                case System.TypeCode.UInt32: convertible = (System.UInt32)ret; break;
                case System.TypeCode.UInt64: convertible = (System.UInt64)ret; break;
                case System.TypeCode.Int64: convertible = (System.Int64)ret; break;
                case System.TypeCode.Decimal: convertible = (System.Decimal)ret; break;
                case System.TypeCode.String: convertible = (System.String)ret; break;
                default: return defautValue;
            }
            return (T)convertible;
        }
    }

    public static Variant ToVariant(this System.Object self)
    {
        return JSON.Load(JSON.Dump(self));
    }
}
