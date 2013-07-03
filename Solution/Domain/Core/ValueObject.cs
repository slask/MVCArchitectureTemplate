using System;
using System.Linq;
using System.Reflection;

namespace Domain.Core
{
    /// <summary>
    /// Base class for value objects in domain.
    /// Value
    /// </summary>
    /// <typeparam name="TValueObject">The type of this value object</typeparam>
    public class ValueObject<TValueObject> : IEquatable<TValueObject> where TValueObject : ValueObject<TValueObject>
    {
        #region IEquatable and Override Equals operators

        /// <summary>
        /// <see>
        ///     <cref>M:System.Object.IEquatable{TValueObject}</cref>
        /// </see>
        /// </summary>
        /// <param name="other"><see>
        ///                         <cref>M:System.Object.IEquatable{TValueObject}</cref>
        ///                     </see>
        /// </param>
        /// <returns><see>
        ///              <cref>M:System.Object.IEquatable{TValueObject}</cref>
        ///          </see>
        /// </returns>
        public bool Equals(TValueObject other)
        {
            if ((object) other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            //compare all public properties
            PropertyInfo[] publicProperties = GetType().GetProperties();

            if (publicProperties.Any())
            {
                return publicProperties.All(p =>
                    {
                        var left = p.GetValue(this, null);
                        var right = p.GetValue(other, null);


                        if (typeof (TValueObject).IsAssignableFrom(left.GetType()))
                        {
                            //check not self-references...
                            return ReferenceEquals(left, right);
                        }
                        return left.Equals(right);
                    });
            }
            return true;
        }

        /// <summary>
        /// <see>
        ///     <cref>M:System.Object.Equals</cref>
        /// </see>
        /// </summary>
        /// <param name="obj"><see>
        ///                       <cref>M:System.Object.Equals</cref>
        ///                   </see>
        /// </param>
        /// <returns><see>
        ///              <cref>M:System.Object.Equals</cref>
        ///          </see>
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            var item = obj as ValueObject<TValueObject>;

            if ((object) item != null)
                return Equals((TValueObject) item);

            return false;
        }

        /// <summary>
        /// <see cref="M:System.Object.GetHashCode"/>
        /// </summary>
        /// <returns><see cref="M:System.Object.GetHashCode"/></returns>
        public override int GetHashCode()
        {
            int hashCode = 31;
            bool changeMultiplier = false;
            const int index = 1;

            //compare all public properties
            PropertyInfo[] publicProperties = GetType().GetProperties();


            if (publicProperties.Any())
            {
                foreach (var item in publicProperties)
                {
                    object value = item.GetValue(this, null);

                    if (value != null)
                    {
                        hashCode = hashCode*((changeMultiplier) ? 59 : 114) + value.GetHashCode();
                        changeMultiplier = !changeMultiplier;
                    }
                    else
                        hashCode = hashCode ^ (index*13); //only for support {"a",null,null,"a"} <> {null,"a","a",null}
                }
            }

            return hashCode;
        }

        public static bool operator ==(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            if (Equals(left, null))
                return (Equals(right, null));
            return left.Equals(right);
        }

        public static bool operator !=(ValueObject<TValueObject> left, ValueObject<TValueObject> right)
        {
            return !(left == right);
        }

        #endregion
    }
}
