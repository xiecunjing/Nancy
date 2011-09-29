﻿namespace Nancy.Security
{
    using System;
    using System.Linq;

    /// <summary>
    /// Represents a Csrf protection token
    /// </summary>
    [Serializable]
    public sealed class CsrfToken
    {
        /// <summary>
        /// The default key for the csrf cookie/form value/querystring value
        /// </summary>
        public const string DEFAULT_CSRF_KEY = "NCSRF";

        /// <summary>
        /// Randomly generated bytes
        /// </summary>
        public byte[] RandomBytes { get; set; }
            
        /// <summary>
        /// Date and time the token was created
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Tamper prevention hmac
        /// </summary>
        public byte[] Hmac { get; set; }

        public bool Equals(CsrfToken other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return this.RandomBytes.SequenceEqual(other.RandomBytes) 
                && other.CreatedDate.Equals(this.CreatedDate) 
                && this.Hmac.SequenceEqual(other.Hmac);
        }

        /// <summary>
        /// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
        /// </summary>
        /// <returns>
        /// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
        /// </returns>
        /// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(CsrfToken)) return false;
            return Equals((CsrfToken)obj);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                int result = (this.RandomBytes != null ? this.RandomBytes.GetHashCode() : 0);
                result = (result*397) ^ this.CreatedDate.GetHashCode();
                result = (result*397) ^ (this.Hmac != null ? this.Hmac.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(CsrfToken left, CsrfToken right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CsrfToken left, CsrfToken right)
        {
            return !Equals(left, right);
        }
    }
}