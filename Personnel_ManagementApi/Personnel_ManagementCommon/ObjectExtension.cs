using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traffic.Utility
{
    /// <summary>
    /// Description: 通用类型扩展方法类
    /// Version: 1.0
    /// Created: 2014/12/17
    /// Author:  zlf
    /// Company: 北京博奥中成信息科技有限责任公司
    /// 
    /// ModifyEditTime: 修改时间
    /// ModifyContent:  修改内容
    /// ModifyPerson :  修改人
    /// </summary>
    public static class ObjectExtension
    {
        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回该类型默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <returns> 转化后的指定类型的对象，转化失败返回类型的默认值 </returns>
        public static T CastTo<T>(this object value)
        {
            object result;
            Type type = typeof(T);
            try
            {
                if (type.IsEnum)
                {
                    result = System.Enum.Parse(type, value.ToString());
                }
                else if (type == typeof(Guid))
                {
                    result = Guid.Parse(value.ToString());
                }
                else
                {
                    result = Convert.ChangeType(value, type);
                }
            }
            catch
            {
                result = default(T);
            }

            return (T)result;
        }

        /// <summary>
        /// 把对象类型转化为指定类型，转化失败时返回指定的默认值
        /// </summary>
        /// <typeparam name="T"> 动态类型 </typeparam>
        /// <param name="value"> 要转化的源对象 </param>
        /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
        /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
        public static T CastTo<T>(this object value, T defaultValue)
        {
            object result;
            Type type = typeof(T);
            try
            {
                result = type.IsEnum ? System.Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
            }
            catch
            {
                result = defaultValue;
            }
            return (T)result;
        }
    }
}
